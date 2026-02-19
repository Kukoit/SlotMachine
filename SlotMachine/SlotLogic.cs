namespace SlotMachine;

public enum Symbol
{
    Cherry,
    Lemon,
    Orange,
    Plum,
    Bell,
    Bar,
    Seven
}

public enum BetType
{
    CenterLine = 1,
    AllHorizontal = 2,
    AllVertical = 3,
    Diagonals = 4,
    AllLines = 5
}

public static class SlotLogic
{
    private static readonly Random _random = new();

    private static readonly Dictionary<Symbol, int> payouts = new()
    {
        { Symbol.Cherry, Constants.PAYOUT_CHERRY },
        { Symbol.Lemon, Constants.PAYOUT_LEMON },
        { Symbol.Orange, Constants.PAYOUT_ORANGE },
        { Symbol.Plum, Constants.PAYOUT_PLUM },
        { Symbol.Bell, Constants.PAYOUT_BELL },
        { Symbol.Bar, Constants.PAYOUT_BAR },
        { Symbol.Seven, Constants.PAYOUT_SEVEN }
    };

    private static readonly Symbol[] allSymbols = Enum.GetValues<Symbol>();

    public static Symbol[,] Spin()
    {
        var grid = new Symbol[Constants.GRID_ROWS, Constants.GRID_COLS];
        for (int r = 0; r < Constants.GRID_ROWS; r++)
            for (int c = 0; c < Constants.GRID_COLS; c++)
                grid[r, c] = allSymbols[_random.Next(allSymbols.Length)];
        return grid;
    }

    public static int CheckHorizontalLine(Symbol[,] grid, int row)
    {
        if (grid[row, Constants.COL_LEFT] == grid[row, Constants.COL_CENTER]
            && grid[row, Constants.COL_CENTER] == grid[row, Constants.COL_RIGHT])
            return payouts[grid[row, Constants.COL_LEFT]];
        return Constants.NO_WIN;
    }

    public static int CheckVerticalLine(Symbol[,] grid, int col)
    {
        if (grid[Constants.ROW_TOP, col] == grid[Constants.ROW_CENTER, col]
            && grid[Constants.ROW_CENTER, col] == grid[Constants.ROW_BOTTOM, col])
            return payouts[grid[Constants.ROW_TOP, col]];
        return Constants.NO_WIN;
    }

    public static int CheckDiagonal(Symbol[,] grid, bool topLeftToBottomRight)
    {
        if (topLeftToBottomRight)
        {
            if (grid[Constants.ROW_TOP, Constants.COL_LEFT] == grid[Constants.ROW_CENTER, Constants.COL_CENTER]
                && grid[Constants.ROW_CENTER, Constants.COL_CENTER] == grid[Constants.ROW_BOTTOM, Constants.COL_RIGHT])
                return payouts[grid[Constants.ROW_TOP, Constants.COL_LEFT]];
        }
        else
        {
            if (grid[Constants.ROW_TOP, Constants.COL_RIGHT] == grid[Constants.ROW_CENTER, Constants.COL_CENTER]
                && grid[Constants.ROW_CENTER, Constants.COL_CENTER] == grid[Constants.ROW_BOTTOM, Constants.COL_LEFT])
                return payouts[grid[Constants.ROW_TOP, Constants.COL_RIGHT]];
        }
        return Constants.NO_WIN;
    }

    public static int GetLineCost(BetType betType) => betType switch
    {
        BetType.CenterLine => Constants.COST_CENTER_LINE,
        BetType.AllHorizontal => Constants.COST_ALL_HORIZONTAL,
        BetType.AllVertical => Constants.COST_ALL_VERTICAL,
        BetType.Diagonals => Constants.COST_DIAGONALS,
        BetType.AllLines => Constants.COST_ALL_LINES,
        _ => Constants.NO_WIN
    };

    public static (int totalWinnings, List<string> winningLines) CalculateWinnings(Symbol[,] grid, BetType betType)
    {
        int total = Constants.NO_WIN;
        var winners = new List<string>();

        // Center horizontal line
        if (betType == BetType.CenterLine || betType == BetType.AllHorizontal || betType == BetType.AllLines)
        {
            int p = CheckHorizontalLine(grid, Constants.ROW_CENTER);
            if (p > Constants.NO_WIN) { total += p; winners.Add($"Center row: {Constants.MATCH_COUNT}x {grid[Constants.ROW_CENTER, Constants.COL_LEFT]} - pays ${p}"); }
        }

        // Top and bottom horizontal lines
        if (betType == BetType.AllHorizontal || betType == BetType.AllLines)
        {
            int p = CheckHorizontalLine(grid, Constants.ROW_TOP);
            if (p > Constants.NO_WIN) { total += p; winners.Add($"Top row: {Constants.MATCH_COUNT}x {grid[Constants.ROW_TOP, Constants.COL_LEFT]} - pays ${p}"); }

            p = CheckHorizontalLine(grid, Constants.ROW_BOTTOM);
            if (p > Constants.NO_WIN) { total += p; winners.Add($"Bottom row: {Constants.MATCH_COUNT}x {grid[Constants.ROW_BOTTOM, Constants.COL_LEFT]} - pays ${p}"); }
        }

        // All vertical lines
        if (betType == BetType.AllVertical || betType == BetType.AllLines)
        {
            for (int c = 0; c < Constants.GRID_COLS; c++)
            {
                int p = CheckVerticalLine(grid, c);
                if (p > Constants.NO_WIN)
                {
                    string name = c == Constants.COL_LEFT ? "Left col" : c == Constants.COL_CENTER ? "Center col" : "Right col";
                    total += p;
                    winners.Add($"{name}: {Constants.MATCH_COUNT}x {grid[Constants.ROW_TOP, c]} - pays ${p}");
                }
            }
        }

        // Diagonals
        if (betType == BetType.Diagonals || betType == BetType.AllLines)
        {
            int p = CheckDiagonal(grid, true);
            if (p > Constants.NO_WIN) { total += p; winners.Add($"Diagonal \\: {Constants.MATCH_COUNT}x {grid[Constants.ROW_TOP, Constants.COL_LEFT]} - pays ${p}"); }

            p = CheckDiagonal(grid, false);
            if (p > Constants.NO_WIN) { total += p; winners.Add($"Diagonal /: {Constants.MATCH_COUNT}x {grid[Constants.ROW_TOP, Constants.COL_RIGHT]} - pays ${p}"); }
        }

        return (total, winners);
    }

    public static string GetSymbolDisplay(Symbol s) => s switch
    {
        Symbol.Cherry => Constants.DISPLAY_CHERRY,
        Symbol.Lemon => Constants.DISPLAY_LEMON,
        Symbol.Orange => Constants.DISPLAY_ORANGE,
        Symbol.Plum => Constants.DISPLAY_PLUM,
        Symbol.Bell => Constants.DISPLAY_BELL,
        Symbol.Bar => Constants.DISPLAY_BAR,
        Symbol.Seven => Constants.DISPLAY_SEVEN,
        _ => Constants.DISPLAY_UNKNOWN
    };
}

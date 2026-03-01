namespace SlotMachine;

public static class SlotUI
{
    public static void DisplayWelcome()
    {
        Console.Clear();
        Console.WriteLine("══════════════════════════════════════");
        Console.WriteLine("          SLOT MACHINE");
        Console.WriteLine("        Match 3 to Win!");
        Console.WriteLine("══════════════════════════════════════");
        Console.WriteLine();
    }

    public static void DisplayBalance(int balance)
    {
        Console.WriteLine($"  Balance: ${balance}");
        Console.WriteLine();
    }

    public static void DisplayPayTable()
    {
        Console.WriteLine("  --- Pay Table (per line) ---");
        Console.WriteLine($"  {Constants.DISPLAY_CHERRY} x{Constants.MATCH_COUNT} = ${Constants.PAYOUT_CHERRY}   {Constants.DISPLAY_LEMON} x{Constants.MATCH_COUNT} = ${Constants.PAYOUT_LEMON}");
        Console.WriteLine($"  {Constants.DISPLAY_ORANGE} x{Constants.MATCH_COUNT} = ${Constants.PAYOUT_ORANGE}   {Constants.DISPLAY_PLUM} x{Constants.MATCH_COUNT} = ${Constants.PAYOUT_PLUM}");
        Console.WriteLine($"  {Constants.DISPLAY_BELL} x{Constants.MATCH_COUNT} = ${Constants.PAYOUT_BELL}  {Constants.DISPLAY_BAR} x{Constants.MATCH_COUNT} = ${Constants.PAYOUT_BAR}");
        Console.WriteLine($"  {Constants.DISPLAY_SEVEN} x{Constants.MATCH_COUNT} = ${Constants.PAYOUT_SEVEN}");
        Console.WriteLine();
    }

    public static BetType GetBetType()
    {
        Console.WriteLine("  How do you want to play?");
        Console.WriteLine($"  1) Center line only        (${Constants.COST_CENTER_LINE})");
        Console.WriteLine($"  2) All {Constants.GRID_ROWS} horizontal lines  (${Constants.COST_ALL_HORIZONTAL})");
        Console.WriteLine($"  3) All {Constants.GRID_COLS} vertical lines    (${Constants.COST_ALL_VERTICAL})");
        Console.WriteLine($"  4) Both diagonals          (${Constants.COST_DIAGONALS})");
        Console.WriteLine($"  5) All {Constants.COST_ALL_LINES} lines             (${Constants.COST_ALL_LINES})");
        Console.WriteLine("  0) Quit");
        Console.Write($"  Enter choice ({Constants.MENU_MIN}-{Constants.MENU_MAX}): ");

        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= Constants.MENU_MIN && choice <= Constants.MENU_MAX)
                return (BetType)choice;
            Console.Write($"  Invalid choice. Enter {Constants.MENU_MIN}-{Constants.MENU_MAX}: ");
        }
    }

    public static bool ConfirmWager(int cost, int balance)
    {
        if (cost > balance)
        {
            Console.WriteLine($"\n  Not enough funds! You need ${cost} but only have ${balance}.");
            Console.WriteLine("  Choose a cheaper bet or quit.\n");
            return false;
        }

        Console.WriteLine($"\n  This will cost ${cost} from your ${balance} balance.");
        Console.Write("  Proceed? (y/n): ");
        string? input = Console.ReadLine()?.Trim().ToLower();
        return input == "y" || input == "yes";
    }

    public static void DisplaySpinning()
    {
        Console.WriteLine("\n  Spinning...\n");
    }

    public static void DisplayGrid(Symbol[,] grid)
    {
        Console.WriteLine("  +------+------+------+");
        for (int r = 0; r < Constants.GRID_ROWS; r++)
        {
            Console.Write("  |");
            for (int c = 0; c < Constants.GRID_COLS; c++)
            {
                string symbol = GetSymbolDisplay(grid[r, c]);
                Console.Write($" {symbol} |");
            }
            Console.WriteLine();
            if (r < Constants.GRID_ROWS - 1)
                Console.WriteLine("  +------+------+------+");
        }
        Console.WriteLine("  +------+------+------+");
    }

    private static string GetSymbolDisplay(Symbol s) => s switch
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

    public static void DisplayResult(int cost, int winnings, List<string> winningLines, int newBalance)
    {
        Console.WriteLine();

        if (winnings > Constants.NO_WIN)
        {
            Console.WriteLine("  *** WINNER! ***");
            foreach (string line in winningLines)
                Console.WriteLine($"    {line}");

            int net = winnings - cost;
            if (net > Constants.NO_WIN)
                Console.WriteLine($"\n  Won ${winnings} (net +${net})");
            else if (net == Constants.NO_WIN)
                Console.WriteLine($"\n  Won ${winnings} (broke even)");
            else
                Console.WriteLine($"\n  Won ${winnings} but spent ${cost} (net -${Math.Abs(net)})");
        }
        else
        {
            Console.WriteLine($"  No winning lines. Lost ${cost}.");
        }

        Console.WriteLine($"  New balance: ${newBalance}");
        Console.WriteLine();
    }

    public static void DisplayGameOver(int balance)
    {
        Console.WriteLine("══════════════════════════════════════");
        Console.WriteLine("            GAME OVER");
        Console.WriteLine($"      Final Balance: ${balance}");
        Console.WriteLine("══════════════════════════════════════");
    }

    public static void DisplayBroke()
    {
        Console.WriteLine("  You're out of money! Game over.");
        Console.WriteLine();
    }
}

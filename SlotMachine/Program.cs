using SlotMachine;

int balance = Constants.INITIAL_BALANCE;

SlotUI.DisplayWelcome();
SlotUI.DisplayPayTable();

while (balance > Constants.NO_WIN)
{
    SlotUI.DisplayBalance(balance);

    BetType betType = SlotUI.GetBetType();

    // Quit option
    if (betType == 0)
        break;

    int cost = SlotLogic.GetLineCost(betType);

    if (!SlotUI.ConfirmWager(cost, balance))
        continue;

    balance -= cost;

    SlotUI.DisplaySpinning();
    Symbol[,] grid = SlotLogic.Spin();
    SlotUI.DisplayGrid(grid);

    var (winnings, winningLines) = SlotLogic.CalculateWinnings(grid, betType);
    balance += winnings;

    SlotUI.DisplayResult(cost, winnings, winningLines, balance);

    if (balance <= Constants.NO_WIN)
    {
        SlotUI.DisplayBroke();
        break;
    }
}

SlotUI.DisplayGameOver(balance);

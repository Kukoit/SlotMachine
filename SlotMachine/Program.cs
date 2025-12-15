namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Game configuration constants
            const double INITIAL_BALANCE = 100.00;
            const double BET_COST_PER_LINE = 1.00;
            const int MIN_GRID_SIZE = 2;
            const int MAX_GRID_SIZE = 10;
            const int NUM_DIAGONALS_SQUARE = 2;
            const string DEFAULT_INPUT = "0";
            const char MIN_DIGIT = '0';
            const char MAX_DIGIT = '9';
            const int FIRST_SYMBOL_INDEX = 0;
            const int SECOND_POSITION = 1;
            const int GRID_CENTER_DIVISOR = 2;
            const double WIN_LINE_PAYOUT = 1.0;

            // Game symbols
            string[] symbols = { "CHERRY", "LEMON", "ORANGE", "GRAPE", "DIAMOND", "SEVEN" };

            // Initialize player money
            double playerMoney = INITIAL_BALANCE;

            // Get grid size from user
            int gridRows = 0;
            int gridCols = 0;

            Console.WriteLine("SLOT MACHINE GAME");
            Console.WriteLine();

            // Get number of rows
            Console.Write($"Enter number of rows ({MIN_GRID_SIZE}-{MAX_GRID_SIZE}): ");
            gridRows = int.Parse(Console.ReadLine() ?? DEFAULT_INPUT);
            // Get number of columns
            Console.Write($"Enter number of rows ({MIN_GRID_SIZE}-{MAX_GRID_SIZE}): ");
            gridCols = int.Parse(Console.ReadLine() ?? DEFAULT_INPUT);

            // Calculate number of diagonals (only if square grid)
            int numDiagonals = 0;
            if (gridRows == gridCols)
            {
                numDiagonals = NUM_DIAGONALS_SQUARE;
            }

            // Game loop
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine($"    SLOT MACHINE GAME ({gridRows}x{gridCols})");
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine($"Current Balance: ${playerMoney:F2}");
                Console.WriteLine();

                if (playerMoney <= 0)
                {
                    Console.WriteLine("You're out of money! Game Over!");
                    break;
                }

                // Calculate bet amounts
                double centerLineCost = BET_COST_PER_LINE;
                double allHorizontalCost = gridRows * BET_COST_PER_LINE;
                double allVerticalCost = gridCols * BET_COST_PER_LINE;
                double allDiagonalCost = numDiagonals * BET_COST_PER_LINE;
                double allLinesCost = allHorizontalCost + allVerticalCost + allDiagonalCost;

                // Display betting options
                Console.WriteLine("Betting Options:");
                Console.WriteLine($"1. Center Horizontal Line (${centerLineCost:F2})");
                Console.WriteLine($"2. All {gridRows} Horizontal Lines (${allHorizontalCost:F2})");
                Console.WriteLine($"3. All {gridCols} Vertical Lines (${allVerticalCost:F2})");

                if (numDiagonals > 0)
                {
                    Console.WriteLine($"4. Both Diagonals (${allDiagonalCost:F2})");
                    Console.WriteLine($"5. All Lines ({gridRows}H + {gridCols}V + {numDiagonals}D = ${allLinesCost:F2})");
                }
                else
                {
                    Console.WriteLine("4. Diagonals (Not available - grid must be square)");
                    Console.WriteLine($"5. All Lines ({gridRows}H + {gridCols}V = ${allLinesCost:F2})");
                }

                Console.WriteLine("0. Quit Game");
                Console.WriteLine();
                Console.Write("Select your bet: ");

                string input = Console.ReadLine();
                int betChoice = 0;
                double betAmount = 0;

                // Parse input
                if (input != null && input.Length > 0)
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] >= MIN_DIGIT && input[i] <= MAX_DIGIT)
                        {
                            betChoice = input[i] - MIN_DIGIT;
                            break;
                        }
                    }
                }

                // Determine bet amount
                const int BET_CENTER_LINE = 1;
                const int BET_ALL_HORIZONTAL = 2;
                const int BET_ALL_VERTICAL = 3;
                const int BET_DIAGONALS = 4;
                const int BET_ALL_LINES = 5;
                const int BET_QUIT = 0;

                if (betChoice == BET_CENTER_LINE)
                    betAmount = centerLineCost;
                else if (betChoice == BET_ALL_HORIZONTAL)
                    betAmount = allHorizontalCost;
                else if (betChoice == BET_ALL_VERTICAL)
                    betAmount = allVerticalCost;
                else if (betChoice == BET_DIAGONALS)
                {
                    if (numDiagonals > 0)
                        betAmount = allDiagonalCost;
                    else
                    {
                        Console.WriteLine("Diagonals not available for non-square grids! Press any key...");
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (betChoice == BET_ALL_LINES)
                    betAmount = allLinesCost;
                else if (betChoice == BET_QUIT)
                {
                    playAgain = false;
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid choice! Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                // check if player has enough money
                if (betAmount > playerMoney)
                {
                    Console.WriteLine("Insufficient funds! Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                // deduct bet
                playerMoney -= betAmount;

                // create dynamic grid
                int[,] grid = new int[gridRows, gridCols];
                Random rand = new Random();

                // spin the reels
                Console.WriteLine("\nSpinning...\n");
                for (int i = 0; i < gridRows; i++)
                {
                    for (int j = 0; j < gridCols; j++)
                    {
                        grid[i, j] = rand.Next(FIRST_SYMBOL_INDEX, symbols.Length);
                    }
                }

                // display grid
                Console.WriteLine("═══════════════════════════════════");
                for (int i = 0; i < gridRows; i++)
                {
                    Console.Write("     ");
                    for (int j = 0; j < gridCols; j++)
                    {
                        Console.Write(symbols[grid[i, j]] + "  ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine();

                // track winnings
                double totalWinnings = 0;
                int winCount = 0;

                // check horizontal lines
                bool checkHorizontal = (betChoice == BET_CENTER_LINE || betChoice == BET_ALL_HORIZONTAL || betChoice == BET_ALL_LINES);
                if (checkHorizontal)
                {
                    // check center line if betChoice is 1
                    if (betChoice == BET_CENTER_LINE)
                    {
                        int centerRow = gridRows / GRID_CENTER_DIVISOR;
                        bool isWin = true;
                        int firstSymbol = grid[centerRow, FIRST_SYMBOL_INDEX];

                        for (int j = SECOND_POSITION; j < gridCols; j++)
                        {
                            if (grid[centerRow, j] != firstSymbol)
                            {
                                isWin = false;
                                break;
                            }
                        }

                        if (isWin)
                        {
                            totalWinnings += WIN_LINE_PAYOUT;
                            winCount++;
                            Console.WriteLine($"✓ CENTER HORIZONTAL LINE (Row {centerRow + 1}) WINS! +{BET_COST_PER_LINE:F2}");
                        }
                    }
                    // check all horizontal lines if betChoice is 2 or 5
                    else
                    {
                        for (int i = 0; i < gridRows; i++)
                        {
                            bool isWin = true;
                            int firstSymbol = grid[i, FIRST_SYMBOL_INDEX];

                            for (int j = SECOND_POSITION; j < gridCols; j++)
                            {
                                if (grid[i, j] != firstSymbol)
                                {
                                    isWin = false;
                                    break;
                                }
                            }

                            if (isWin)
                            {
                                totalWinnings += WIN_LINE_PAYOUT;
                                winCount++;
                                Console.WriteLine($"✓ HORIZONTAL LINE (Row {i + 1}) WINS! +{BET_COST_PER_LINE:F2}");
                            }
                        }
                    }
                }

                // check vertical lines
                bool checkVertical = (betChoice == BET_ALL_VERTICAL || betChoice == BET_ALL_LINES);
                if (checkVertical)
                {
                    for (int j = 0; j < gridCols; j++)
                    {
                        bool isWin = true;
                        int firstSymbol = grid[FIRST_SYMBOL_INDEX, j];

                        for (int i = SECOND_POSITION; i < gridRows; i++)
                        {
                            if (grid[i, j] != firstSymbol)
                            {
                                isWin = false;
                                break;
                            }
                        }

                        if (isWin)
                        {
                            totalWinnings += WIN_LINE_PAYOUT;
                            winCount++;
                            Console.WriteLine($"✓ VERTICAL LINE (Column {j + 1}) WINS! +$1.00");
                        }
                    }
                }

                // check diagonal lines (only for square grids)
                bool checkDiagonal = (betChoice == BET_DIAGONALS || betChoice == BET_ALL_LINES);
                if (checkDiagonal && numDiagonals > 0)
                {
                    // top-left to bottom-right diagonal
                    bool isWin = true;
                    int firstSymbol = grid[FIRST_SYMBOL_INDEX, FIRST_SYMBOL_INDEX];

                    for (int i = SECOND_POSITION; i < gridRows; i++)
                    {
                        if (grid[i, i] != firstSymbol)
                        {
                            isWin = false;
                            break;
                        }
                    }

                    if (isWin)
                    {
                        totalWinnings += WIN_LINE_PAYOUT;
                        winCount++;
                        Console.WriteLine(" DIAGONAL (\\) WINS! +$1.00");
                    }

                    // top-right to bottom-left diagonal
                    isWin = true;
                    firstSymbol = grid[FIRST_SYMBOL_INDEX, gridCols - SECOND_POSITION];

                    for (int i = SECOND_POSITION; i < gridRows; i++)
                    {
                        if (grid[i, gridCols - SECOND_POSITION - i] != firstSymbol)
                        {
                            isWin = false;
                            break;
                        }
                    }

                    if (isWin)
                    {
                        totalWinnings += WIN_LINE_PAYOUT;
                        winCount++;
                        Console.WriteLine("✓ DIAGONAL (/) WINS! +$1.00");
                    }
                }

                // add winnings to player money
                playerMoney += totalWinnings;

                // display results
                Console.WriteLine();
                if (winCount > 0)
                {
                    Console.WriteLine($"YOU WON ${totalWinnings:F2}!");
                }
                else
                {
                    Console.WriteLine("No winning lines this time.");
                }

                Console.WriteLine($"New Balance: ${playerMoney:F2}");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            Console.WriteLine();
            Console.WriteLine($"Thanks for playing! Final Balance: ${playerMoney:F2}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
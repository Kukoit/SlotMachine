namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {

                // Game symbols
                string[] symbols = { "CHERRY", "LEMON", "ORANGE", "GRAPE", "DIAMOND", "SEVEN" };

                // Initialize player money
                double playerMoney = 100.00;

                // Get grid size from user
                int gridRows = 0;
                int gridCols = 0;

                Console.WriteLine("SLOT MACHINE GAME");
                Console.WriteLine();

                // Get number of rows
                Console.Write("Enter number of rows (2-10): ");
                gridRows = int.Parse(Console.ReadLine() ?? "0");
               // Get number of columns
            Console.Write("Enter number of columns (2-10): ");
                gridCols = int.Parse(Console.ReadLine() ?? "0");

            // Calculate number of diagonals (only if square grid)
            int numDiagonals = 0;
                if (gridRows == gridCols)
                {
                    numDiagonals = 2;
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
                    double centerLineCost = 1.00;
                    double allHorizontalCost = gridRows * 1.00;
                    double allVerticalCost = gridCols * 1.00;
                    double allDiagonalCost = numDiagonals * 1.00;
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
                            if (input[i] >= '0' && input[i] <= '9')
                            {
                                betChoice = input[i] - '0';
                                break;
                            }
                        }
                    }

                    // Determine bet amount
                    if (betChoice == 1)
                        betAmount = centerLineCost;
                    else if (betChoice == 2)
                        betAmount = allHorizontalCost;
                    else if (betChoice == 3)
                        betAmount = allVerticalCost;
                    else if (betChoice == 4)
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
                    else if (betChoice == 5)
                        betAmount = allLinesCost;
                    else if (betChoice == 0)
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
                            grid[i, j] = rand.Next(0, symbols.Length);
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
                    bool checkHorizontal = (betChoice == 1 || betChoice == 2 || betChoice == 5);
                    if (checkHorizontal)
                    {
                        // check center line if betChoice is 1
                        if (betChoice == 1)
                        {
                            int centerRow = gridRows / 2;
                            bool isWin = true;
                            int firstSymbol = grid[centerRow, 0];

                            for (int j = 1; j < gridCols; j++)
                            {
                                if (grid[centerRow, j] != firstSymbol)
                                {
                                    isWin = false;
                                    break;
                                }
                            }

                            if (isWin)
                            {
                                totalWinnings += 1;
                                winCount++;
                                Console.WriteLine($"✓ CENTER HORIZONTAL LINE (Row {centerRow + 1}) WINS! +$1.00");
                            }
                        }
                        // check all horizontal lines if betChoice is 2 or 5
                        else
                        {
                            for (int i = 0; i < gridRows; i++)
                            {
                                bool isWin = true;
                                int firstSymbol = grid[i, 0];

                                for (int j = 1; j < gridCols; j++)
                                {
                                    if (grid[i, j] != firstSymbol)
                                    {
                                        isWin = false;
                                        break;
                                    }
                                }

                                if (isWin)
                                {
                                    totalWinnings += 1;
                                    winCount++;
                                    Console.WriteLine($"✓ HORIZONTAL LINE (Row {i + 1}) WINS! +$1.00");
                                }
                            }
                        }
                    }

                    // check vertical lines
                    bool checkVertical = (betChoice == 3 || betChoice == 5);
                    if (checkVertical)
                    {
                        for (int j = 0; j < gridCols; j++)
                        {
                            bool isWin = true;
                            int firstSymbol = grid[0, j];

                            for (int i = 1; i < gridRows; i++)
                            {
                                if (grid[i, j] != firstSymbol)
                                {
                                    isWin = false;
                                    break;
                                }
                            }

                            if (isWin)
                            {
                                totalWinnings += 1;
                                winCount++;
                                Console.WriteLine($"✓ VERTICAL LINE (Column {j + 1}) WINS! +$1.00");
                            }
                        }
                    }

                    // check diagonal lines (only for square grids)
                    bool checkDiagonal = (betChoice == 4 || betChoice == 5);
                    if (checkDiagonal && numDiagonals > 0)
                    {
                        // top-left to bottom-right diagonal
                        bool isWin = true;
                        int firstSymbol = grid[0, 0];

                        for (int i = 1; i < gridRows; i++)
                        {
                            if (grid[i, i] != firstSymbol)
                            {
                                isWin = false;
                                break;
                            }
                        }

                        if (isWin)
                        {
                            totalWinnings += 1;
                            winCount++;
                            Console.WriteLine(" DIAGONAL (\\) WINS! +$1.00");
                        }

                        // top-right to bottom-left diagonal
                        isWin = true;
                        firstSymbol = grid[0, gridCols - 1];

                        for (int i = 1; i < gridRows; i++)
                        {
                            if (grid[i, gridCols - 1 - i] != firstSymbol)
                            {
                                isWin = false;
                                break;
                            }
                        }

                        if (isWin)
                        {
                            totalWinnings += 1;
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
    


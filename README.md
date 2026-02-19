# Slot Machine

A console-based slot machine game built with C# and .NET 8.

## How to Play

You start with a $100 balance and spin a 3x3 grid of symbols. Choose which lines to bet on and win payouts when 3 matching symbols line up.

### Bet Options

| Choice | Lines Played | Cost |
|--------|-------------|------|
| 1 | Center horizontal line | $1 |
| 2 | All 3 horizontal lines | $3 |
| 3 | All 3 vertical lines | $3 |
| 4 | Both diagonals | $2 |
| 5 | All 8 lines | $8 |

### Pay Table

| Symbol | Payout |
|--------|--------|
| CHRY | $2 |
| LEMN | $3 |
| ORNG | $4 |
| PLUM | $5 |
| BELL | $10 |
| BAR  | $20 |
| 7777 | $50 |

## Project Structure

```
SlotMachine/
├── SlotMachine.sln
└── SlotMachine/
    ├── SlotMachine.csproj
    ├── Program.cs          # Entry point and game loop
    ├── Constants.cs         # All game constants (SNAKE_CASE)
    ├── SlotLogic.cs         # Game logic (spin, line checks, payouts)
    └── SlotUI.cs            # Console display and user input
```

UI and logic are separated into distinct files with no cross-contamination — `SlotLogic.cs` contains no `Console` references, and `SlotUI.cs` contains no game logic.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Run

```bash
cd SlotMachine/SlotMachine
dotnet run
```

Or open `SlotMachine.sln` in Visual Studio and press F5.

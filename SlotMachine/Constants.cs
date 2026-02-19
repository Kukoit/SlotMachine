namespace SlotMachine;

public static class Constants
{
    // Grid dimensions
    public const int GRID_ROWS = 3;
    public const int GRID_COLS = 3;

    // Grid index positions
    public const int ROW_TOP = 0;
    public const int ROW_CENTER = 1;
    public const int ROW_BOTTOM = 2;
    public const int COL_LEFT = 0;
    public const int COL_CENTER = 1;
    public const int COL_RIGHT = 2;

    // Player
    public const int INITIAL_BALANCE = 100;

    // Line costs per bet type
    public const int COST_CENTER_LINE = 1;
    public const int COST_ALL_HORIZONTAL = 3;
    public const int COST_ALL_VERTICAL = 3;
    public const int COST_DIAGONALS = 2;
    public const int COST_ALL_LINES = 8;

    // Payout multipliers (per matching line)
    public const int PAYOUT_CHERRY = 2;
    public const int PAYOUT_LEMON = 3;
    public const int PAYOUT_ORANGE = 4;
    public const int PAYOUT_PLUM = 5;
    public const int PAYOUT_BELL = 10;
    public const int PAYOUT_BAR = 20;
    public const int PAYOUT_SEVEN = 50;

    // Win threshold
    public const int NO_WIN = 0;

    // Menu input range
    public const int MENU_MIN = 0;
    public const int MENU_MAX = 5;

    // Symbols display
    public const string DISPLAY_CHERRY = "CHRY";
    public const string DISPLAY_LEMON = "LEMN";
    public const string DISPLAY_ORANGE = "ORNG";
    public const string DISPLAY_PLUM = "PLUM";
    public const string DISPLAY_BELL = "BELL";
    public const string DISPLAY_BAR = "BAR ";
    public const string DISPLAY_SEVEN = "7777";
    public const string DISPLAY_UNKNOWN = "????";

    // Match count for a winning line
    public const int MATCH_COUNT = 3;
}

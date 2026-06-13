
public static class HexSettings
{
    public const float GapMultiplier = 1.02f;

    public const float HorizontalSpacingMultiplier = 1.2f * GapMultiplier;
    public const float VerticalSpacingMultiplier = 0.866f * GapMultiplier;

    public static float CellWidth { get; private set; }
    public static float CellHeight { get; private set; }

    public static float HorizontalSpacing => CellWidth * HorizontalSpacingMultiplier;
    public static float DiagonalX => HorizontalSpacing * 0.5f;
    public static float DiagonalY => CellHeight * VerticalSpacingMultiplier;

    public static void Initialize(float cellWidth, float cellHeight)
    {
        CellWidth = cellWidth;
        CellHeight = cellHeight;
    }
}
namespace Engine.Core.Manager.SpatialGridSystem;

public record struct Cell
{
    public readonly int X;
    public readonly int Y;
    private const int CellSize = 250;

    public static Cell Create(float xPos, float yPos)
    {
        var x = (int)Math.Floor(xPos / CellSize);
        var y = (int)Math.Floor(yPos / CellSize);

        return new Cell(x, y);
    }

    private Cell(int x, int y)
    {
        X = x;
        Y = y;
    }
}
internal class Cell
{
    public int TopY { get; }
    public int LeftX { get; }
    public int Row { get; }
    public int Column { get; }
    public int Width { get; }

    public Cell(int leftX, int topY, int cellX, int cellY, int width)
    {
        TopY = topY;
        LeftX = leftX;
        Row = cellX;
        Column = cellY;
        Width = width;
    }
}

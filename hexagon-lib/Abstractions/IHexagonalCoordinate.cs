namespace HexagonLib.Abstractions;

public interface IHexagonalCoordinate
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public string AsString();
}

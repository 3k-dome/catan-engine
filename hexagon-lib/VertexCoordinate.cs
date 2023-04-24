using HexagonLib.Abstractions;
using HexagonLib.Enums;

namespace HexagonLib
{
    public class VertexCoordinate : HexagonalCoordinate
    {
        public VertexType VertexType { get; }

        public VertexCoordinate(int x, int y, int z) : base(x, y, z)
        {
            int sum = X + Y + Z;

            VertexType = sum switch
            {
                0 => throw new ArgumentException("Tile coordniate was given."),
                1 => VertexType.CaretUp,
                2 => VertexType.CaretDown,
                _ => throw new ArgumentException("Coordinate seems to be on a different plane."),
            };
        }

        public VertexCoordinate Add((int X, int Y, int Z) coordinate)
        {

            return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
        }

        public VertexCoordinate Add(IHexagonalCoordinate coordinate)
        {
            return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
        }
    }
}

using HexagonLib.Abstractions;
using HexagonLib.Enums;

namespace HexagonLib
{
    public class TileCoordinate : HexagonalCoordinate
    {
        public TileCoordinate(int x, int y, int z) : base(x, y, z)
        {
            int sum = X + Y + Z;

            if (sum != 0)
            {
                throw new ArgumentException("Not a valid coordinate, tile coordinates must follow '0 == x + y + z'.");
            }
        }

        public TileCoordinate Add((int X, int Y, int Z) coordinate)
        {

            return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
        }

        public TileCoordinate Add(IHexagonalCoordinate coordinate)
        {
            return new(X + coordinate.X, Y + coordinate.Y, Z + coordinate.Z);
        }

        public TileCoordinate Scale(int scale)
        {
            return new(X * scale, Y * scale, Z * scale);
        }

        public TileCoordinate this[TileNeighbor neighbor] => Add(TileNeighborCoordinates.Offsets[neighbor]);

        public IEnumerable<TileCoordinate> Neighbors()
        {
            foreach (TileNeighbor direction in Enum.GetValues<TileNeighbor>())
            {
                yield return this[direction];
            }
        }

        public VertexCoordinate this[TileVertex neighbor]
        {
            get
            {
                (int X, int Y, int Z) offset = TileVertexCoordinates.Offsets[neighbor];
                return new(X + offset.X, Y + offset.Y, Z + offset.Z);
            }
        }

        public IEnumerable<VertexCoordinate> Vertices()
        {
            foreach (TileVertex direction in Enum.GetValues<TileVertex>())
            {
                yield return this[direction];
            }
        }

    }
}

using HexagonLib.Abstractions;

namespace HexagonLib
{
    public class TileCoordinate : HexagonalCoordinate
    {
        public TileCoordinate(int x, int y, int z) : base(x, y, z)
        {
            int sum = X + Y + Z;

            if (sum != 0 && Math.Abs(sum) % 3 == 0)
            {   
                throw new ArgumentException("Tiles only support the plane at which '0 == x + y + z' is valid.");
            }

            if (sum != 0)
            {
                throw new ArgumentException("Not a valid coordinate, tile coordinates must follow '0 == x + y + z'.");
            }
        }
    }
}

using HexagonLib.Abstractions;

namespace HexagonLib
{
    public class VertexCoordinate : HexagonalCoordinate
    {
        public VertexCoordinate(int x, int y, int z) : base(x, y, z)
        {
            int sum = X + Y + Z;

            if (sum == 0 || Math.Abs(sum) % 3 == 0)
            {
                throw new ArgumentException("Tile coordniate was given.");
            }
        }
    }
}

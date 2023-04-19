namespace HexagonLib.Abstractions
{
    public abstract class HexagonalCoordinate : IHexagonalCoordinate
    {
        public int X { get; protected set; }

        public int Y { get; protected set; }

        public int Z { get; protected set; }

        public HexagonalCoordinate(int x, int y, int z)
        {
            (X, Y, Z) = (x, y, z);
        }

        public override bool Equals(object? other)
        {
            return other != null && other is HexagonalCoordinate && GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}

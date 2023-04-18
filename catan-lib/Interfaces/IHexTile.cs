namespace CatanLib.Interfaces
{
    public interface IHexTile : IVectorizable
    {
        IHexCoordinate Coordinate { get; }
    }
}

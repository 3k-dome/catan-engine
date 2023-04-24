using HexagonLib;

namespace CatanLib.Interfaces
{
    public interface IHexTile : IVectorizable
    {
        TileCoordinate Coordinate { get; set; }
    }
}

using HexagonLib;

namespace CatanLib.Interfaces
{
    public interface IHexTile : IVectorizableComponent
    {
        TileCoordinate Coordinate { get; set; }
    }
}

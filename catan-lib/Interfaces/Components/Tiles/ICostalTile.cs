using CatanLib.Enums;
using HexagonLib;
using HexagonLib.Enums;

namespace CatanLib.Interfaces.Components.Tiles;

public interface ICostalTile : IHexagonalTile
{
    new TileCoordinate Coordinate { get; set; }
    IEnumerable<TileVertexDirection>? AccessibleFrom { get; }
    Resource? TradesResource { get; }
    bool TradesAny { get; }
}

using CatanLib.Enums;
using CatanLib.Interfaces.Interaction.Vectorization;
using HexagonLib;
using HexagonLib.Enums;

namespace CatanLib.Interfaces.Components.Tiles;

public interface ICostalTile : IHexagonalTile, IVectorizableComponent
{
    new TileCoordinate Coordinate { get; set; }
    IEnumerable<TileVertexDirection>? AccessibleFrom { get; }
    Resource? TradesResource { get; }
    bool TradesAny { get; }
}

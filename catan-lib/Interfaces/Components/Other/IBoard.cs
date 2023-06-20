using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Tiles;
using CatanLib.Interfaces.Interaction.Other;
using CatanLib.Interfaces.Interaction.Vectorization;
using HexagonLib;

namespace CatanLib.Interfaces.Components.Other;

public interface IBoard : IVectorizableComponent, IVectorizableActions, IResetableBySeed
{
    Dictionary<TileCoordinate, IHexagonalTile> Tiles { get; }
    IHexagonalTile this[TileCoordinate tileCoordinate] { get; }

    Dictionary<VertexCoordinate, ISettlement> Settlements { get; }
    ISettlement this[VertexCoordinate vertexCoordinate] { get; }

    Dictionary<EdgeCoordinate, IRoad> Roads { get; }
    IRoad this[EdgeCoordinate edgeCoordinate] { get; }
}

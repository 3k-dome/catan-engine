using CatanLib.Enums;
using CatanLib.Interfaces.Interaction.Vectorization;

namespace CatanLib.Interfaces.Components.Tiles;

public interface ITerrainTile : IHexagonalTile, IVectorizableComponent
{
    Terrain? TerrainType { get; }
    INumberToken NumberToken { get; }
}

using CatanLib.Enums;

namespace CatanLib.Interfaces.Components.Tiles;

public interface ITerrainTile : IHexagonalTile
{
    Terrain? TerrainType { get; }
    INumberToken NumberToken { get; }
    bool HasRobber { get; }

    public void ToggleRobber();
}

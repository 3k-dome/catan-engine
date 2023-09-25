using CatanLib.Enums;
using CatanLib.Interfaces.Components.Tiles;
using HexagonLib;

namespace CatanLib.Components.Tiles;

public class TerrainTile : ITerrainTile
{
    public Terrain? TerrainType { get; private set; }
    public INumberToken NumberToken { get; private set; }
    public TileCoordinate Coordinate { get; private set; }
    public bool HasRobber { get; private set; }

    public void ToggleRobber()
    {
        HasRobber = !HasRobber;
    }

    public TerrainTile(Terrain? terrain, INumberToken token, TileCoordinate coordinate)
    {
        (TerrainType, NumberToken, Coordinate, HasRobber) = (terrain, token, coordinate, terrain is null);
    }
}

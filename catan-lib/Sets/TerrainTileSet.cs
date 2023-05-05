using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using CatanLib.Parts;

namespace CatanLib.Sets
{
    public static class TerrainTileSet
    {
        public static readonly IEnumerable<ITerrainTile> Tiles = new List<TerrainTile>()
        {
            new() { Terrain = TerrainType.Desert },

            new() { Terrain = TerrainType.Mountain },
            new() { Terrain = TerrainType.Mountain },
            new() { Terrain = TerrainType.Mountain },

            new() { Terrain = TerrainType.Hill },
            new() { Terrain = TerrainType.Hill },
            new() { Terrain = TerrainType.Hill },

            new() { Terrain = TerrainType.Field },
            new() { Terrain = TerrainType.Field },
            new() { Terrain = TerrainType.Field },
            new() { Terrain = TerrainType.Field },

            new() { Terrain = TerrainType.Forest },
            new() { Terrain = TerrainType.Forest },
            new() { Terrain = TerrainType.Forest },
            new() { Terrain = TerrainType.Forest },

            new() { Terrain = TerrainType.Pasture },
            new() { Terrain = TerrainType.Pasture },
            new() { Terrain = TerrainType.Pasture },
            new() { Terrain = TerrainType.Pasture },
        };
    }
}

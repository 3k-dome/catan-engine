using CatanLib.Enums;
using CatanLib.Interfaces;
using CatanLib.Parts;

namespace CatanLib.Sets
{
    public static class HexTileSet
    {
        public static readonly IEnumerable<IHexTile> Tiles = new List<HexTile>()
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

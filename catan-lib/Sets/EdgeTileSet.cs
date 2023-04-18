using CatanLib.Enums;
using CatanLib.Interfaces;
using CatanLib.Parts;

namespace CatanLib.Sets
{
    public static class EdgeTileSet
    {
        public static readonly IEnumerable<IEnumerable<IEdgeTile>> Tiles = new List<List<EdgeTile>>()
        {
            new() // border 1:2
            {
                new() { ResourceTrade = ResourceType.Sheep, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = true },
            },
            new() // border 2:3
            {
                new() { ResourceTrade = null, SeaTrade = false },
                new() { ResourceTrade = ResourceType.Ore, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = false },
            },
            new() // border 3:4
            {
                new() { ResourceTrade = ResourceType.Wheat, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = true },
            },
            new() // border 4:5
            {
                new() { ResourceTrade = null, SeaTrade = false },
                new() { ResourceTrade = ResourceType.Wood, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = false },
            },
            new() // border 5:6
            {
                new() { ResourceTrade = ResourceType.Brick, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = true },
            },
            new() // border 6:1
            {
                new() { ResourceTrade = null, SeaTrade = false },
                new() { ResourceTrade = null, SeaTrade = true },
                new() { ResourceTrade = null, SeaTrade = false },
            },
        };
    }
}

﻿namespace CatanLib.Enums
{
    public enum ResourceType
    {
        Sheep,
        Wheat,
        Ore,
        Brick,
        Wood,
        Road,
        Settlement,
        City,
    }

    public static class TerrainResources
    {
        public static readonly Dictionary<TerrainType, ResourceType> Resources = new()
        {
            { TerrainType.Pasture, ResourceType.Sheep },
            { TerrainType.Field, ResourceType.Wheat },
            { TerrainType.Mountain, ResourceType.Ore },
            { TerrainType.Hill, ResourceType.Brick },
            { TerrainType.Forest, ResourceType.Wood },
        };
    }
}

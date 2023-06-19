namespace CatanLib.Enums;

public enum Terrain
{
    Pasture,
    Field,
    Mountain,
    Hill,
    Forest,
}

public static class TerrainLookup
{
    public static IEnumerable<Terrain> TerrainTypes => Enum.GetValues<Terrain>();

    public static IEnumerable<Terrain?> TerrainSet { get; } = new List<Terrain?>()
    {
        null,
        Terrain.Mountain,
        Terrain.Mountain,
        Terrain.Mountain,
        Terrain.Hill,
        Terrain.Hill,
        Terrain.Hill,
        Terrain.Field,
        Terrain.Field,
        Terrain.Field,
        Terrain.Field,
        Terrain.Forest,
        Terrain.Forest,
        Terrain.Forest,
        Terrain.Forest,
        Terrain.Pasture,
        Terrain.Pasture,
        Terrain.Pasture,
        Terrain.Pasture,
    };
}
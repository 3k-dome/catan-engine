namespace CatanLib.Enums;

public enum Resource
{
    Sheep,
    Wheat,
    Ore,
    Brick,
    Wood
}

public static class ResourceLookup
{
    public static IEnumerable<Resource> Resources => Enum.GetValues<Resource>();

    public static readonly Dictionary<Terrain, Resource> Lookup = new()
    {
        { Terrain.Pasture, Resource.Sheep },
        { Terrain.Field, Resource.Wheat },
        { Terrain.Mountain, Resource.Ore },
        { Terrain.Hill, Resource.Brick },
        { Terrain.Forest, Resource.Wood },
    };

    public static Resource GetResource(Terrain terrain)
    {
        return Lookup[terrain];
    }
}

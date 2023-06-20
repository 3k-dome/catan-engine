using CatanLib.Enums;

namespace CatanLib.Encoders;

public static class TerrainEncoder
{
    /// <summary>
    /// Applies one-hot encoding for the given terrrain, wastes are left blank.
    /// </summary>
    public static IEnumerable<float> EncodeTerrain(Terrain? terrain)
    {
        float[] encoding = new float[TerrainLookup.TerrainTypes.Count()];
        if (terrain is not null)
        {
            encoding[(int)terrain] = 1f;
        }
        return encoding;
    }

    /// <summary>
    /// Provides the a description of the terrain encoding.
    /// </summary>
    public static IEnumerable<string> EncodeTerrainDescriptive()
    {
        return TerrainLookup.TerrainTypes.Select(terrain => $"Is {terrain}").ToArray();
    }
}

using CatanLib.Enums;

namespace CatanLib.Encoders;
public static class ResourceEncoder
{
    /// <summary>
    /// Applies one-hot encoding for the given resource, wastes are left blank.
    /// </summary>
    public static IEnumerable<float> EncodeResource(Resource? resource)
    {
        float[] encoding = new float[ResourceLookup.Resources.Count()];
        if (resource is not null)
        {
            encoding[(int)resource] = 1f;
        }
        return encoding;
    }

    /// <summary>
    /// Provides the a description of the resource encoding.
    /// </summary>
    public static IEnumerable<string> EncodeResourceDescriptive()
    {
        return ResourceLookup.Resources.Select(resource => $"Trades {resource} 2:1").ToArray();
    }
}

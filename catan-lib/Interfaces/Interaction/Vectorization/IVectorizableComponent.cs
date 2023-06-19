using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Interfaces.Interaction.Vectorization;

public interface IVectorizableComponent
{
    IEnumerable<float> ToVector(ICatan catan);
    IEnumerable<string> ToDescriptiveVector(ICatan catan);
}

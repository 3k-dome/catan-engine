using CatanLib.Enums;

namespace CatanLib.Interfaces;

public interface IPlayer : IVectorizableComponent
{
    PlayerNumber Number { get; }
    Dictionary<ResourceType, int> Resources { get; }

    void UseResource(ResourceType resource);
    void UseResources(IEnumerable<ResourceType> resources);
    void GainResource(ResourceType resource);
    void GainResources(IEnumerable<ResourceType> resources);
}

using CatanLib.Enums;

namespace CatanLib.Interfaces;

public interface IPlayer : IVectorizableComponent
{
    PlayerNumber Number { get; }
    Dictionary<ResourceType, int> Resources { get; }

    void UseResources(IEnumerable<ResourceType> resources);
    void GainResources(IEnumerable<ResourceType> resources);
}

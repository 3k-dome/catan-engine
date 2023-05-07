using CatanLib.Enums;

namespace CatanLib.Interfaces.Interaction
{
    public interface IResourceCollection
    {
        Dictionary<ResourceType, int> Resources { get; }

        void UseResource(ResourceType resource)
        {
            if (Resources[resource] <= 0)
            {
                throw new Exception("Player can't spend any more of this Resource!");
            }

            Resources[resource]--;
        }

        void UseResources(IEnumerable<ResourceType> resources)
        {
            foreach (ResourceType resource in resources)
            {
                UseResource(resource);
            }
        }

        void GainResource(ResourceType resource)
        {
            Resources[resource]++;
        }

        void GainResources(IEnumerable<ResourceType> resources)
        {
            foreach (ResourceType resource in resources)
            {
                GainResource(resource);
            }
        }

        bool HasResources(IEnumerable<ResourceType> resources)
        {
            return resources.GroupBy(resource => resource)
                 .All(group => Resources[group.Key] >= group.Count());
        }
    }
}

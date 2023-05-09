using CatanLib.Enums;
using CatanLib.Interfaces.Interaction;

namespace CatanLib.Interfaces.Components
{
    public interface ITrade : IActionPlay, IVectorizableActions
    {
        IEnumerable<ResourceType> ResourceGains { get; }
    }
}

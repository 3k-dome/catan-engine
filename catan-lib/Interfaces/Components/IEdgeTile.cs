using CatanLib.Enums;
using CatanLib.Interfaces.Interaction;

namespace CatanLib.Interfaces.Components
{
    public interface IEdgeTile : IHexTile, IVectorizableComponent
    {
        bool SeaTrade { get; }
        ResourceType? ResourceTrade { get; }
    }
}

using CatanLib.Enums;

namespace CatanLib.Interfaces
{
    public interface IEdgeTile : IHexTile, IVectorizableComponent
    {
        bool SeaTrade { get; }
        ResourceType? ResourceTrade { get; }
    }
}

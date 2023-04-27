using CatanLib.Enums;

namespace CatanLib.Interfaces
{
    public interface IEdgeTile : IHexTile
    {
        bool SeaTrade { get; }
        ResourceType? ResourceTrade { get; }
    }
}

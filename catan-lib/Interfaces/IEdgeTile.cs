using CatanLib.Enums;

namespace CatanLib.Interfaces
{
    public interface IEdgeTile
    {
        bool SeaTrade { get; }
        ResourceType ResourceTrade { get; }
    }
}

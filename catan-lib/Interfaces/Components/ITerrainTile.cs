using CatanLib.Enums;
using CatanLib.Interfaces.Interaction;

namespace CatanLib.Interfaces.Components
{
    public interface ITerrainTile : IHexTile, IVectorizableComponent
    {
        TerrainType Terrain { get; }
        IProductionCircle Production { get; set; }
    }
}

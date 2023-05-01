using CatanLib.Enums;

namespace CatanLib.Interfaces
{
    public interface ITerrainTile : IHexTile, IVectorizableComponent
    {
        TerrainType Terrain { get; }
        IProductionCircle Production { get; set; }
    }
}

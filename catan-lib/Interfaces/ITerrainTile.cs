using CatanLib.Enums;

namespace CatanLib.Interfaces
{
    public interface ITerrainTile : IHexTile
    {
        TerrainType Terrain { get; }
        IProductionCircle Production { get; set; }
    }
}

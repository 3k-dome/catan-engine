using CatanLib.Enums;

namespace CatanLib.Interfaces
{
    public interface IHexTile : IVectorizable
    {
        TerrainType Terrain { get; init; }
        float[] TerrainEncoding { get; }
        IProductionCircle Production { get; set; }
    }
}

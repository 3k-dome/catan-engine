using CatanLib.Enums;

namespace CatanLib.Interfaces
{
    public interface IHexTile
    {
        TerrainType Terrain { get; set; }
        float[] TerrainEncoding { get; }
    }
}

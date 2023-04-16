using CatanLib.Enums;
using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    internal class HexTile : IHexTile, IVectorizable
    {
        public TerrainType Terrain { get; set; }

        private float[]? terrainEncoding;
        public float[] TerrainEncoding
        {
            get
            {
                if (terrainEncoding != null)
                {
                    return terrainEncoding;
                }

                terrainEncoding = new float[Enum.GetValues(typeof(TerrainType)).Length];
                terrainEncoding[(int)Terrain] = 1;
                return terrainEncoding;
            }
        }

        public IEnumerable<float> ToVector()
        {
            IEnumerable<float> vector = Enumerable.Empty<float>();
            vector = vector.Concat(TerrainEncoding);
            return vector;
        }
    }
}

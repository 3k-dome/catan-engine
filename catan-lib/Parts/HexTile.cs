using CatanLib.Enums;
using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    public class HexTile : IHexTile
    {
        public TerrainType Terrain { get; init; }

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

        private IProductionCircle? production;
        public IProductionCircle Production
        {
            get => production ?? throw new NullReferenceException("Property is still unset.");
            set => production ??= value;
        }

        public IEnumerable<float> ToVector()
        {
            IEnumerable<float> vector = Enumerable.Empty<float>();
            vector = vector.Concat(TerrainEncoding);
            vector = vector.Concat(Production.ToVector());
            return vector;
        }
    }
}

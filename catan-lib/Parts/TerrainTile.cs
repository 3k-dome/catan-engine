using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using HexagonLib;

namespace CatanLib.Parts
{
    public class TerrainTile : ITerrainTile
    {
        public TerrainType Terrain { get; init; }

        private IProductionCircle? production;
        public IProductionCircle Production
        {
            get => production ?? throw new NullReferenceException();
            set => production ??= value;
        }

        private TileCoordinate? coordinate;
        public TileCoordinate Coordinate
        {
            get => coordinate ?? throw new NullReferenceException();
            set => coordinate ??= value;
        }

        private float[]? terrainEncoding;
        public float[] TerrainEncoding
        {
            get
            {
                if (terrainEncoding != null)
                {
                    return terrainEncoding;
                }

                terrainEncoding = new float[Enum.GetValues<TerrainType>().Length - 1];
                if (Terrain != TerrainType.Desert)
                {
                    terrainEncoding[(int)Terrain] = 1;
                }
                return terrainEncoding;
            }
        }

        public IEnumerable<float> ToVector(ICatan catan)
        {
            IEnumerable<float> vector = Enumerable.Empty<float>();
            vector = vector.Concat(TerrainEncoding);
            vector = vector.Concat(Production.ToVector(catan));
            return vector;
        }
    }
}

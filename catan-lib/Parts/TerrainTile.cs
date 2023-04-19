﻿using CatanLib.Enums;
using CatanLib.Interfaces;

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

        private IHexCoordinate? coordinate;
        public IHexCoordinate Coordinate
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

                terrainEncoding = new float[Enum.GetValues<TerrainType>().Length];
                terrainEncoding[(int)Terrain] = 1;
                return terrainEncoding;
            }
        }

        public IEnumerable<string> ToExplainedVector()
        {
            IEnumerable<string> vector = Enumerable.Empty<string>();

            // not backed by a lazy property since this is just a utility method
            int index = 0;
            string[] descriptions = new string[Enum.GetValues<TerrainType>().Length];
            foreach (TerrainType terrainType in Enum.GetValues<TerrainType>())
            {
                descriptions[index] = $"Is{Enum.GetName(terrainType)}";
                index++;
            }

            vector = vector.Concat(descriptions);
            vector = vector.Concat(Production.ToExplainedVector());
            return vector;
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
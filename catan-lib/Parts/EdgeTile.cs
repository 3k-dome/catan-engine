using CatanLib.Enums;
using CatanLib.Interfaces;

namespace CatanLib.Parts
{
    public class EdgeTile : IEdgeTile
    {
        public bool SeaTrade { get; init; }

        public ResourceType? ResourceTrade { get; init; }

        private IHexCoordinate? coordinate;
        public IHexCoordinate Coordinate
        {
            get => coordinate ?? throw new NullReferenceException();
            set => coordinate ??= value;
        }

        private float[]? tradeEncoding;
        public float[] TradeEncoding
        {
            get
            {
                if (tradeEncoding != null)
                {
                    return tradeEncoding;
                }

                tradeEncoding = new float[Enum.GetValues<ResourceType>().Length];
                if (ResourceTrade != null)
                {
                    tradeEncoding[(int)ResourceTrade] = 1;
                }
                return tradeEncoding;
            }
        }

        public IEnumerable<string> ToExplainedVector()
        {
            // not backed by a lazy property since this is just a utility method
            int index = 0;
            string[] descriptions = new string[Enum.GetValues<ResourceType>().Length];
            foreach (ResourceType terrainType in Enum.GetValues<ResourceType>())
            {
                descriptions[index] = $"Trades{Enum.GetName(terrainType)}";
                index++;
            }

            return descriptions;
        }

        public IEnumerable<float> ToVector()
        {
            return TradeEncoding;
        }
    }
}

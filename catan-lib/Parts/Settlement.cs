using CatanLib.Enums;
using CatanLib.Interfaces;
using HexagonLib;

namespace CatanLib.Parts
{
    public class Settlement : ISettlement
    {
        private VertexCoordinate? vertex;
        public VertexCoordinate Vertex {
            get => vertex ?? throw new NullReferenceException();
            set => vertex ??= value;
        }

        public bool IsSettlement { get; private set; }

        public bool IsCity { get; private set; }

        public PlayerNumber? Belongs { get; private set; }

        private readonly IEnumerable<ResourceType> costs = new[]
        {
            ResourceType.Wood,
            ResourceType.Brick,
            ResourceType.Wheat,
            ResourceType.Sheep
        };
        public IEnumerable<ResourceType> Costs => costs;

        private readonly IEnumerable<ResourceType> upgradeCosts = new[]
        {
            ResourceType.Wheat,
            ResourceType.Wheat,
            ResourceType.Ore,
            ResourceType.Ore,
            ResourceType.Ore
        };
        public IEnumerable<ResourceType> UpgradeCosts => upgradeCosts;

        public IEnumerable<float> ToVector()
        {

            float[] playerEncoding = new float[Enum.GetValues<PlayerNumber>().Length];
            if (Belongs != null)
            {
                playerEncoding[(int)Belongs] = 1;
            }

            float[] buildingEncoding = new float[] { IsSettlement ? 1 : 0, IsCity ? 1 : 0 };
            return Enumerable.Concat(playerEncoding, buildingEncoding);
        }

        public IEnumerable<string> ToExplainedVector()
        {
            throw new NotImplementedException();
        }
    }
}

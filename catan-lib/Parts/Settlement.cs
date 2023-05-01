using CatanLib.Enums;
using CatanLib.Interfaces;
using CatanLib.Interfaces.Components;
using HexagonLib;

namespace CatanLib.Parts
{
    public class Settlement : ISettlement
    {
        private VertexCoordinate? vertex;
        public VertexCoordinate Vertex
        {
            get => vertex ?? throw new NullReferenceException();
            set => vertex ??= value;
        }

        public bool IsSettlement { get; private set; }
        public bool IsCity { get; private set; }
        public IPlayer? Belongs { get; private set; }
        public IEnumerable<ResourceType> Costs { get; } = new[]
        {
            ResourceType.Wood,
            ResourceType.Brick,
            ResourceType.Wheat,
            ResourceType.Sheep
        };
        public IEnumerable<ResourceType> UpgradeCosts { get; } = new[]
        {
            ResourceType.Wheat,
            ResourceType.Wheat,
            ResourceType.Ore,
            ResourceType.Ore,
            ResourceType.Ore
        };

        public void Play<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            catan.CurrentPlayer.UseResources(Costs);
            Belongs = catan.CurrentPlayer;
            (IsSettlement, IsCity) = (true, false);
        }

        public bool CanPlay<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            bool hasOwner = Belongs != null;
            bool hasResources = catan.CurrentPlayer.HasResources(Costs);
            bool placementRule = Vertex.Neighbors()
                .Select(vertex => catan.Board[vertex])
                .All(settlement => !settlement.IsSettlement && !settlement.IsCity);
            return !hasOwner && hasResources && placementRule && !IsSettlement && !IsCity;
        }

        public void Upgrade<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            catan.CurrentPlayer.UseResources(UpgradeCosts);
            (IsSettlement, IsCity) = (false, true);
        }

        public bool CanUpgrade<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            bool isOwner = Belongs == catan.CurrentPlayer;
            bool hasResources = catan.CurrentPlayer.HasResources(UpgradeCosts);
            return hasResources && isOwner && IsSettlement && !IsCity;
        }

        public IEnumerable<Action> GetActions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Func<bool>> CanExecuteActions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<float> ToVector()
        {
            throw new NotImplementedException();
        }
    }
}

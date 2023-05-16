using CatanLib.Enums;
using CatanLib.Helpers;
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

        public bool IsCity { get; private set; }
        public IPlayer? Belongs { get; private set; }
        public IEnumerable<ResourceType> ResourceCosts { get; } = new[]
        {
            ResourceType.Wood,
            ResourceType.Brick,
            ResourceType.Wheat,
            ResourceType.Sheep,
        };
        public PieceType RequiredPiece { get; } = PieceType.Settlement;

        public IEnumerable<ResourceType> UpgradeResourceCosts { get; } = new[]
        {
            ResourceType.Wheat,
            ResourceType.Wheat,
            ResourceType.Ore,
            ResourceType.Ore,
            ResourceType.Ore,
        };
        public PieceType RequiredUpgradePiece { get; } = PieceType.City;

        public void Play(ICatan catan)
        {
            catan.CurrentPlayer.UseResources(ResourceCosts);
            catan.CurrentPlayer.PlacePiece(RequiredPiece);
            Belongs = catan.CurrentPlayer;
        }

        public bool CanPlay(ICatan catan)
        {
            bool hasOwner = Belongs is not null;
            bool hasResources = catan.CurrentPlayer.HasResources(ResourceCosts);
            bool hasPiece = catan.CurrentPlayer.HasPiece(RequiredPiece);

            if (hasOwner || !hasResources || !hasPiece) { return false; }

            bool distanceRule = Vertex.Neighbors()
                .Select(vertex => catan.Board[vertex])
                .All(settlement => settlement.Belongs is null);

            bool placementRule = Vertex.Edges()
                .Select(edge => catan.Board[edge])
                .Any(road => road.Belongs is not null && road.Belongs.Number == catan.CurrentPlayer.Number);

            return distanceRule && placementRule;
        }

        public void Upgrade(ICatan catan)
        {
            catan.CurrentPlayer.UseResources(UpgradeResourceCosts);
            catan.CurrentPlayer.TakePiece(RequiredPiece);
            catan.CurrentPlayer.PlacePiece(RequiredUpgradePiece);
            IsCity = true;
        }

        public bool CanUpgrade(ICatan catan)
        {
            bool isUpgradable = !IsCity;
            bool isOwner = Belongs is not null && Belongs.Number == catan.CurrentPlayer.Number;
            bool hasResources = catan.CurrentPlayer.HasResources(UpgradeResourceCosts);
            bool hasPiece = catan.CurrentPlayer.HasPiece(RequiredUpgradePiece);

            return isUpgradable && isOwner && hasResources && hasPiece;
        }

        public IEnumerable<Action<ICatan>> GetActions()
        {
            yield return Play;
            yield return Upgrade;
        }

        public IEnumerable<Func<ICatan, bool>> CanExecuteActions()
        {
            yield return CanPlay;
            yield return CanUpgrade;
        }

        public IEnumerable<float> ToVector(ICatan catan)
        {
            IEnumerable<float> playerEndoding = PlayerEncoding.Encode(catan.CurrentPlayer, Belongs);
            return playerEndoding.Append(IsCity ? 0f : 1f);
        }
    }
}

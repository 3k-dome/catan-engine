using CatanLib.Enums;
using CatanLib.Helpers;
using CatanLib.Interfaces.Components;
using HexagonLib;

namespace CatanLib.Parts
{
    public class Road : IRoad
    {
        private EdgeCoordinate? edge;
        public EdgeCoordinate Edge
        {
            get => edge ?? throw new NullReferenceException();
            set => edge ??= value;
        }

        public IPlayer? Belongs { get; private set; }
        public IEnumerable<ResourceType> ResourceCosts { get; } = new[]
        {
            ResourceType.Wood,
            ResourceType.Brick,
        };

        public PieceType RequiredPiece { get; } = PieceType.Road;

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

            bool connectsToOwnSettlement = Edge.Vertices()
               .Select(vertex => catan.Board[vertex])
               .Any(settlement => settlement.Belongs is not null && settlement.Belongs.Number == catan.CurrentPlayer.Number);

            if (connectsToOwnSettlement) { return true; }

            IEnumerable<IRoad> incommingOwnRoads = Edge.Neighbors()
                .Select(edge => catan.Board[edge])
                .Where(road => road.Belongs is not null && road.Belongs.Number == catan.CurrentPlayer.Number);

            IEnumerable<ISettlement> sharedSettlements = incommingOwnRoads
                .Select(road => road.Edge)
                .Select(edge => Edge.Contains(edge.VertexA) ? edge.VertexA : edge.VertexB)
                .Distinct()
                .Select(vertex => catan.Board[vertex]);

            bool anyValidIncommingRoad = sharedSettlements
                .Any(settlement => settlement.Belongs is null || settlement.Belongs.Number == catan.CurrentPlayer.Number);

            return anyValidIncommingRoad;
        }

        public IEnumerable<Action<ICatan>> GetActions()
        {
            yield return Play;
        }

        public IEnumerable<Func<ICatan, bool>> CanExecuteActions()
        {
            yield return CanPlay;
        }

        public IEnumerable<float> ToVector(ICatan catan)
        {
            return PlayerEncoding.Encode(catan.CurrentPlayer, Belongs);
        }
    }
}

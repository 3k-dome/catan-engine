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

        public bool IsRoad { get; private set; }
        public IPlayer? Belongs { get; private set; }
        public IEnumerable<ResourceType> ResourceCosts { get; } = new[]
        {
            ResourceType.Wood,
            ResourceType.Brick,
        };

        public PieceType RequiredPiece { get; } = PieceType.Road;

        public void Play<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            catan.CurrentPlayer.UseResources(ResourceCosts);
            catan.CurrentPlayer.PlacePiece(RequiredPiece);
            Belongs = catan.CurrentPlayer;
            IsRoad = true;
        }

        public bool CanPlay<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            bool hasOwner = Belongs != null;
            bool hasResources = catan.CurrentPlayer.HasResources(ResourceCosts);
            bool hasPiece = catan.CurrentPlayer.HasPiece(RequiredPiece);

            bool connectsToOwnSettlement = Edge.Vertices()
               .Select(vertex => catan.Board[vertex])
               .Any(settlement => (settlement.IsSettlement || settlement.IsCity) && settlement.Belongs == catan.CurrentPlayer);

            IEnumerable<IRoad> incommingOwnRoads = Edge
                .Neighbors()
                .Select(edge => catan.Board[edge])
                .Where(road => road.IsRoad && Belongs == catan.CurrentPlayer);

            IEnumerable<VertexCoordinate> sharedSettlements = incommingOwnRoads
                .Select(road => road.Edge)
                .Select(edge => Edge.Contains(edge.VertexA) ? edge.VertexA : edge.VertexB)
                .Distinct();

            bool anyValidIncommingRoad = sharedSettlements
                .Select(vertex => catan.Board[vertex])
                .Any(settlement => settlement.Belongs == catan.CurrentPlayer || settlement.Belongs == null);

            return !hasOwner && hasResources && hasPiece && (connectsToOwnSettlement || anyValidIncommingRoad);
        }

        public IEnumerable<Action<Catan<TSettlement, TRoad, TDice>>> GetActions<TSettlement, TRoad, TDice>()
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            yield return Play;
        }

        public IEnumerable<Func<Catan<TSettlement, TRoad, TDice>, bool>> CanExecuteActions<TSettlement, TRoad, TDice>()
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            yield return CanPlay;
        }

        public IEnumerable<float> ToVector<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            IEnumerable<float> playerEndoding = PlayerEncoding.Encode(catan.CurrentPlayer, Belongs);

            float[] stateEncoding = new float[1];
            stateEncoding[0] = IsRoad ? 1 : 0;

            return playerEndoding.Concat(stateEncoding);
        }
    }
}

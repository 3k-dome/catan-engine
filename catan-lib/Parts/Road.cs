using CatanLib.Enums;
using CatanLib.Interfaces;
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
        public IEnumerable<ResourceType> Costs { get; } = new[] { ResourceType.Wood, ResourceType.Brick };

        public void Play<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            catan.CurrentPlayer.UseResources(Costs);
            Belongs = catan.CurrentPlayer;
            IsRoad = true;
        }

        public bool CanPlay<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            bool hasOwner = Belongs != null;
            bool hasResources = catan.CurrentPlayer.HasResources(Costs);

            bool connectsToOwnSettlement = Edge
               // at least one of the vertices of this road ...
               .Vertices()

               // ... musst be our own settlement or city
               .Select(vertex => catan.Board[vertex])
               .Any(settlement => (settlement.IsSettlement || settlement.IsCity) && settlement.Belongs == catan.CurrentPlayer);

            bool connectsToOwnRoad = Edge
                // at least one incomming road must be one our own
                .Neighbors()
                .Select(edge => catan.Board[edge])
                .Where(road => road.IsRoad && Belongs == catan.CurrentPlayer)

                // and the vertex of the incomming road that is shared with this road ...
                .Select(road => road.Edge)
                .Select(edge => Edge.Contains(edge.VertexA) ? edge.VertexA : edge.VertexB)
                .Distinct()

                // ... must either be our own settlement or no ones
                .Select(vertex => catan.Board[vertex])
                .Any(settlement => settlement.Belongs == catan.CurrentPlayer || settlement.Belongs == null);

            return (connectsToOwnSettlement || connectsToOwnRoad) && !hasOwner && hasResources && !IsRoad;
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

using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using HexagonLib;

namespace CatanLib.Parts.Trades
{
    public class ReducedHarborTrade : GenericTrade
    {
        public TileCoordinate Coordinate { get; private set; }

        private ReducedHarborTrade(TileCoordinate coordinate, IEnumerable<ResourceType> resourceGains, IEnumerable<ResourceType> resourceCosts) : base(resourceGains, resourceCosts)
        {
            Coordinate = coordinate;
        }

        public static IEnumerable<ITrade> TradeFactory(TileCoordinate coordinate, ResourceType givenResource)
        {
            List<ITrade> trades = new();
            foreach (ResourceType gainedResource in Enum.GetValues<ResourceType>())
            {
                trades.Add(new ReducedHarborTrade(
                    coordinate,
                    new ResourceType[] { gainedResource },
                    new ResourceType[] { givenResource, givenResource }
                ));
            }
            return trades;
        }

        public override bool CanPlay(ICatan catan)
        {
            if (!catan.CurrentPlayer.HasResources(ResourceCosts)) { return false; }

            bool accessesThisHarbor = Coordinate.Vertices()
                .Where(vertex => catan.Board.VertexStore.ContainsKey(vertex))
                .Select(vertex => catan.Board[vertex])
                .Where(settlement => settlement.Belongs is not null && settlement.Belongs.Number == catan.CurrentPlayer.Number)
                .Any();

            return accessesThisHarbor;
        }
    }
}

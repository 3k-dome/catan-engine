using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts.Trades
{
    public class GenericHarborTrade : GenericTrade
    {

        private GenericHarborTrade(IEnumerable<ResourceType> resourceGains, IEnumerable<ResourceType> resourceCosts) : base(resourceGains, resourceCosts) { }

        public static new IEnumerable<ITrade> TradeFactory()
        {
            List<ITrade> trades = new();
            foreach (ResourceType givenResource in Enum.GetValues<ResourceType>())
            {
                foreach (ResourceType gainedResource in Enum.GetValues<ResourceType>())
                {
                    trades.Add(new GenericHarborTrade(
                        new ResourceType[] { gainedResource },
                        new ResourceType[] { givenResource, givenResource, givenResource }
                    ));
                }
            }
            return trades;
        }

        public override bool CanPlay(ICatan catan)
        {
            if (!catan.CurrentPlayer.HasResources(ResourceCosts)) { return false; }

            bool accessesAnyHarbor = catan.Board.TileStore.Select(entry => entry.Value)
                .OfType<IEdgeTile>()
                .Where(tile => tile.SeaTrade)
                .SelectMany(tile => tile.Coordinate.Vertices()
                    .Where(vertex => catan.Board.VertexStore.ContainsKey(vertex))
                    .Select(vertex => catan.Board.VertexStore[vertex])
                    .Where(settlement => settlement.Belongs is not null && settlement.Belongs.Number == catan.CurrentPlayer.Number))
                .Any();

            return accessesAnyHarbor;
        }
    }
}

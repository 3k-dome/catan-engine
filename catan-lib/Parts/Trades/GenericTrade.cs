using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class GenericTrade : ITrade
    {

        public IEnumerable<ResourceType> ResourceGains { get; private set; }
        public IEnumerable<ResourceType> ResourceCosts { get; private set; }
        public PieceType RequiredPiece => PieceType.None;

        protected GenericTrade(IEnumerable<ResourceType> resourceGains, IEnumerable<ResourceType> resourceCosts)
        {
            ResourceGains = resourceGains;
            ResourceCosts = resourceCosts;
        }

        public static IEnumerable<ITrade> TradeFactory()
        {
            List<ITrade> trades = new();
            foreach (ResourceType givenResource in Enum.GetValues<ResourceType>())
            {
                foreach (ResourceType gainedResource in Enum.GetValues<ResourceType>())
                {
                    trades.Add(new GenericTrade(
                        new ResourceType[] { gainedResource },
                        new ResourceType[] { givenResource, givenResource, givenResource, givenResource }
                    ));
                }
            }
            return trades;
        }

        public void Play(ICatan catan)
        {
            catan.CurrentPlayer.GainResources(ResourceGains);
            catan.CurrentPlayer.UseResources(ResourceCosts);
        }

        public virtual bool CanPlay(ICatan catan)
        {
            return catan.CurrentPlayer.HasResources(ResourceCosts);
        }

        public IEnumerable<Action<ICatan>> GetActions()
        {
            yield return Play;
        }

        public IEnumerable<Func<ICatan, bool>> CanExecuteActions()
        {
            yield return CanPlay;
        }
    }
}

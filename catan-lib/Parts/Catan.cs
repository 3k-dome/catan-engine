using CatanLib.Enums;
using CatanLib.Helpers;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class Catan<TSettlement, TRoad, TDice> : ICatan
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new()
    {
        public IDice Dice { get; private set; }
        public IBank Bank { get; private set; }
        public Board Board { get; private set; }
        public IEnumerable<IPlayer> Players { get; private set; }
        public IPlayer CurrentPlayer => Players.First();

        public Catan(IEnumerable<IPlayer> players, int seed)
        {
            Dice = new TDice() { Random = new Random(seed) };
            Bank = new Bank();
            Board = Board.BoardFactory<TSettlement, TRoad>(Dice);
            Players = players.OrderBy(_ => Dice.RollTwice());

            // assign player order, relevant for our encoding
            foreach ((IPlayer player, PlayerNumber number) in Players.Zip(Enum.GetValues<PlayerNumber>()))
            {
                player.Number = number;
            }
        }

        private void NextPlayer()
        {
            Players = Players.Skip(1).Concat(Players.Take(1));
        }

        private void CounterClockwise()
        {
            Players = Players.Reverse();
        }

        private void SettlementPhase()
        {

        }

        private void ResourceProductionPhase()
        {

            IEnumerable<ITerrainTile> producingTiles = ProductionPhase.GetProducingTiles(Board.TileStore, Dice.RollTwice());
            IEnumerable<ResourceType> totalYield = ProductionPhase.GetTotalYield(Board.VertexStore, producingTiles);

            // all players gain resouces if the bank can provide the total yield
            if (Bank.HasResources(totalYield))
            {
                ProductionPhase.DistributeResources(Board.VertexStore, producingTiles, Bank);
            }

            // if only one player would gain resources he gains as much as possible
            else if (ProductionPhase.GetPlayerCount(Board.VertexStore, producingTiles) == 1)
            {
                ProductionPhase.TryDistributeResources(Board.VertexStore, producingTiles, Bank);
            }
        }

        private void TradingPhase()
        {

        }

        private void BuildingPhase()
        {

        }

    }
}

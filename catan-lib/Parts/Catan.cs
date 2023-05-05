using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class Catan<TSettlement, TRoad, TDice> : ICatan
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new()
    {
        public IDice Dice { get; private set; }
        public Board Board { get; private set; }
        public IEnumerable<IPlayer> Players { get; private set; }
        public IPlayer CurrentPlayer => Players.First();

        public Catan(IEnumerable<IPlayer> players, int seed)
        {
            Dice = new TDice() { Random = new Random(seed) };
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
            //// only allow placement of one settlement

            //Board.VertexStore.Select(entry => entry.Value)

            //VertexCoordinate coordinate;

            //// only allow placement of one road
            //_ = Board.EdgeStore.Select(entry => entry.Key)
            //    .Where(edge => edge.VertexA == coordinate || edge.VertexB == coordinate);
        }

        private void ResourceProductionPhase()
        {
            int roll = Dice.RollTwice();

            // find all tiles that match the roll
            IEnumerable<ITerrainTile> tiles = Board.TileStore
                 .Select(entry => entry.Value)
                 .OfType<ITerrainTile>()
                 .Where(tile => tile.Production.Roll == roll);

            foreach (ITerrainTile tile in tiles)
            {
                // find all settlements surrounding one of the matching tiles
                IEnumerable<ISettlement> settlements = tile.Coordinate.Vertices()
                    .Select(vertexCoordinate => Board[vertexCoordinate])
                    .Where(settlement => settlement.IsSettlement || settlement.IsCity);

                // add resouces to each settlement and city
                foreach (ISettlement settlement in settlements)
                {
                    if (settlement.IsSettlement)
                    {
                        settlement.Belongs?.GainResource(TerrainResources.Resources[tile.Terrain]);
                        continue;
                    }

                    if (settlement.IsCity)
                    {
                        settlement.Belongs?.GainResources(new[] { TerrainResources.Resources[tile.Terrain], TerrainResources.Resources[tile.Terrain] });
                        continue;
                    }
                }
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

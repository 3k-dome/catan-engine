using CatanLib.Enums;
using CatanLib.Interfaces;
using CatanLib.Interfaces.Components;

namespace CatanLib.Parts
{
    public class Catan<TSettlement, TRoad, TDice> : ICatan
    where TSettlement : ISettlement, new()
    where TRoad : IRoad, new()
    where TDice : IDice, new()
    {
        public Board Board { get; private set; }
        public IDice Dice { get; private set; }
        public IEnumerable<IPlayer> Players { get; private set; }
        public IPlayer CurrentPlayer => Players.First();

        public Catan(IEnumerable<IPlayer> players, int seed)
        {
            Players = players;
            Dice = new TDice() { Random = new Random(seed) };
            Board = Board.BoardFactory<TSettlement, TRoad>(Dice);
        }

        private void NextPlayer()
        {
            Players = Players.Skip(1).Concat(Players.Take(1));
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
                    .Select(vertexCoordinate => Board.VertexStore[vertexCoordinate])
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

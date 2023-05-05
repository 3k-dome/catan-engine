using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using CatanLib.Sets;
using HexagonLib;
using HexagonLib.Enums;
using HexagonLib.Utils;

namespace CatanLib.Parts
{
    public class Board
    {
        public Dictionary<TileCoordinate, IHexTile> TileStore = new();
        public Dictionary<VertexCoordinate, ISettlement> VertexStore = new();
        public Dictionary<EdgeCoordinate, IRoad> EdgeStore = new();

        public IDice Dice { get; init; }

        private Board(IDice dice)
        {
            Dice = dice;
        }

        public static Board BoardFactory<TSettlement, TRoad>(IDice dice)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        {
            Board board = new(dice);
            board.SetupBoard<TSettlement, TRoad>();
            return board;
        }

        private void SetupBoard<TSettlement, TRoad>()
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        {
            PlaceEdgeTiles(Dice.Random);
            PlaceTerrainTiles(Dice.Random);
            InitSettlementPlacement<TSettlement>();
            InitRoadPlacement<TRoad>();
        }

        private void InitRoadPlacement<TRoad>() where TRoad : IRoad, new()
        {
            foreach (VertexCoordinate coordinateA in VertexStore.Select(pair => pair.Key))
            {
                foreach ((int X, int Y, int Z) offset in VertexNeighborCoordinates.Offsets[coordinateA.VertexType].Select(pair => pair.Value))
                {
                    VertexCoordinate coordinateB = coordinateA.Add(offset);
                    if (VertexStore.ContainsKey(coordinateB))
                    {
                        EdgeCoordinate edge = new(coordinateA, coordinateB);
                        IRoad road = new TRoad() { Edge = edge };
                        _ = EdgeStore.TryAdd(edge, road);
                    }
                }
            }
        }

        private void InitSettlementPlacement<TSettlement>() where TSettlement : ISettlement, new()
        {
            foreach (ITerrainTile terrainTile in TileStore.Select(pair => pair.Value).OfType<ITerrainTile>())
            {
                foreach (VertexCoordinate coordinate in terrainTile.Coordinate.Vertices())
                {
                    ISettlement settlement = new TSettlement
                    {
                        Vertex = coordinate
                    };
                    _ = VertexStore.TryAdd(coordinate, settlement);
                }
            }
        }

        private void PlaceTerrainTiles(Random random)
        {
            IEnumerable<TileCoordinate> terrainTileCoordinates = SetupTerrainTileCoordinates(random);
            IEnumerator<IProductionCircle> circles = ProductionCircleSet.Circles.OrderBy(circle => circle.Order).GetEnumerator();
            IOrderedEnumerable<ITerrainTile> terrainTiles = TerrainTileSet.Tiles.OrderBy(_ => random.Next());
            foreach ((TileCoordinate coordinate, ITerrainTile tile) in terrainTileCoordinates.Zip(terrainTiles))
            {
                if (tile.Terrain != TerrainType.Desert)
                {
                    _ = circles.MoveNext();
                    tile.Production = circles.Current;
                }
                else
                {
                    tile.Production = ProductionCircleSet.DesertCircle;
                }

                tile.Coordinate = coordinate;
                TileStore.Add(coordinate, tile);
            }
        }

        private static IEnumerable<TileCoordinate> SetupTerrainTileCoordinates(Random random)
        {
            TileNeighbor startDirection = Enum.GetValues<TileNeighbor>().OrderBy(_ => random.Next()).First();

            IEnumerable<TileCoordinate> tileCoordinates = Enumerable.Empty<TileCoordinate>();
            TileCoordinate center = new(0, 0, 0);

            tileCoordinates = tileCoordinates.Concat(TileOperations.Circle(center, startDirection, 2));
            tileCoordinates = tileCoordinates.Concat(TileOperations.Circle(center, startDirection, 1));
            tileCoordinates = tileCoordinates.Concat(new[] { center });
            return tileCoordinates;
        }

        private void PlaceEdgeTiles(Random random)
        {
            IEnumerable<TileCoordinate> edgeTileCoordinates = SetupEdgeTileCoordinates();
            IEnumerable<IEdgeTile> edgeTiles = EdgeTileSet.Tiles.OrderBy(_ => random.Next()).SelectMany(set => set);
            foreach ((TileCoordinate coordinate, IEdgeTile tile) in edgeTileCoordinates.Zip(edgeTiles))
            {
                tile.Coordinate = coordinate;
                TileStore.Add(coordinate, tile);
            }
        }

        private static IEnumerable<TileCoordinate> SetupEdgeTileCoordinates()
        {
            IEnumerable<TileCoordinate> borderCoordinates = TileOperations.Circle(new TileCoordinate(0, 0, 0), TileNeighbor.NorthWest, 3);
            return borderCoordinates.Skip(1).Concat(borderCoordinates.Take(1)); ;
        }

        public IHexTile this[TileCoordinate tile] => TileStore[tile];
        public ISettlement this[VertexCoordinate vertex] => VertexStore[vertex];
        public IRoad this[EdgeCoordinate edge] => EdgeStore[edge];
    }
}
using CatanLib.Enums;
using CatanLib.Interfaces.Components;
using CatanLib.Interfaces.Interaction;
using CatanLib.Sets;
using HexagonLib;
using HexagonLib.Enums;
using HexagonLib.Utils;

namespace CatanLib.Parts
{
    public class Board : IVectorizableComponent, IVectorizableActions
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
            return SetupTerrainTileCoordinates(startDirection);
        }

        private static IEnumerable<TileCoordinate> SetupTerrainTileCoordinates(TileNeighbor direction)
        {
            IEnumerable<TileCoordinate> tileCoordinates = Enumerable.Empty<TileCoordinate>();
            TileCoordinate center = new(0, 0, 0);

            tileCoordinates = tileCoordinates.Concat(TileOperations.Circle(center, direction, 2));
            tileCoordinates = tileCoordinates.Concat(TileOperations.Circle(center, direction, 1));
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

        public IEnumerable<float> ToVector(ICatan catan)
        {
            // tiles need to be always serialized in the same order each game, otherwise
            // we would simply serialize a rotated (see starting direction) state of the game

            // ensure that terrain is always serialized in the same order by creating
            // a new coordinate spiral with a set direction, this should be cached
            IEnumerable<TileCoordinate> terrainTileOrder = SetupTerrainTileCoordinates(TileNeighbor.NorthWest);
            IEnumerable<float> terrainTileEncoding = terrainTileOrder.Select(coordinate => TileStore[coordinate])
                .Select(tile => tile)
                .OfType<ITerrainTile>()
                .SelectMany(tile => tile.ToVector(catan));

            // ensure that edge is always serialized in the same order, by reuseing
            // the edge placement spiral to index the tilestore, this should be cached
            IEnumerable<TileCoordinate> edgeTileOrder = SetupEdgeTileCoordinates();
            IEnumerable<float> edgeTileEncoding = edgeTileOrder.Select(coordinate => TileStore[coordinate])
                .Select(tile => tile)
                .Cast<IEdgeTile>()
                .SelectMany(tile => tile.ToVector(catan));

            // ensure that all vertices are always serialized in the same order

            // given that each tile is now ordered and the vertices of a tile are
            // visited in a set order, we can simple traverse them and remove duplicates,
            // this should be cached
            IEnumerable<VertexCoordinate> vertexOrder = terrainTileOrder.Select(coordinate => TileStore[coordinate])
                .Select(tile => tile)
                .OfType<ITerrainTile>()
                .SelectMany(tile => tile.Coordinate.Vertices())
                .Distinct();
            IEnumerable<float> vertexEncoding = vertexOrder.Select(vertex => VertexStore[vertex])
                .SelectMany(settlement => settlement.ToVector(catan));


            // ensure that all edges are always serialized in the same order

            // by now all vertices are orderd therefore we can simply travers those
            // and thier eges which are generated in a set order
            // since this will produce all edges we need to exclude edges that lead into the see
            IEnumerable<EdgeCoordinate> edgeOrder = vertexOrder.SelectMany(vertex => vertex.Edges())
                .Where(edge => EdgeStore.ContainsKey(edge))
                .Distinct();
            IEnumerable<float> edgeEncoding = edgeOrder.Select(edge => EdgeStore[edge])
                .SelectMany(road => road.ToVector(catan));


            return terrainTileEncoding.Concat(edgeTileEncoding).Concat(vertexEncoding).Concat(edgeEncoding);
        }

        public IEnumerable<Action<Catan<TSettlement, TRoad, TDice>>> GetActions<TSettlement, TRoad, TDice>()
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Func<Catan<TSettlement, TRoad, TDice>, bool>> CanExecuteActions<TSettlement, TRoad, TDice>()
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            throw new NotImplementedException();
        }
    }
}
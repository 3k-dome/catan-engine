using CatanLib.Enums;
using CatanLib.Helpers;
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
        public IHexTile this[TileCoordinate tile] => TileStore[tile];


        public Dictionary<VertexCoordinate, ISettlement> VertexStore = new();
        public ISettlement this[VertexCoordinate vertex] => VertexStore[vertex];


        public Dictionary<EdgeCoordinate, IRoad> EdgeStore = new();
        public IRoad this[EdgeCoordinate edge] => EdgeStore[edge];


        private Board() { }

        public static Board BoardFactory<TSettlement, TRoad>(Random random)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        {
            Board board = new();
            board.SetupBoard<TSettlement, TRoad>(random);
            return board;
        }


        private void SetupBoard<TSettlement, TRoad>(Random random)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        {
            PlaceEdgeTiles(random);
            PlaceTerrainTiles(random);
            InitSettlementPlacement<TSettlement>();
            InitRoadPlacement<TRoad>();
        }

        private void PlaceEdgeTiles(Random random)
        {
            IEnumerable<TileCoordinate> edgeTileCoordinates = TraverseOrder.EdgeTileOrder.Value;
            IEnumerable<IEdgeTile> edgeTiles = EdgeTileSet.Tiles.OrderBy(_ => random.Next()).SelectMany(set => set);
            foreach ((TileCoordinate coordinate, IEdgeTile tile) in edgeTileCoordinates.Zip(edgeTiles))
            {
                tile.Coordinate = coordinate;
                TileStore.Add(coordinate, tile);
            }
        }

        private static IEnumerable<TileCoordinate> GetTerrainTilePlacementOrder(Random random)
        {
            TileNeighbor startDirection = Enum.GetValues<TileNeighbor>().OrderBy(_ => random.Next()).First();
            return TileOperations.Spiral(new(0, 0, 0), startDirection, 2);
        }

        private void PlaceTerrainTiles(Random random)
        {
            IEnumerable<TileCoordinate> terrainTileCoordinates = GetTerrainTilePlacementOrder(random);
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

        private void InitSettlementPlacement<TSettlement>() where TSettlement : ISettlement, new()
        {
            foreach (ITerrainTile terrainTile in TileStore.Select(entry => entry.Value).OfType<ITerrainTile>())
            {
                foreach (VertexCoordinate coordinate in terrainTile.Coordinate.Vertices())
                {
                    ISettlement settlement = new TSettlement { Vertex = coordinate };
                    _ = VertexStore.TryAdd(coordinate, settlement);
                }
            }
        }

        private void InitRoadPlacement<TRoad>() where TRoad : IRoad, new()
        {
            foreach (VertexCoordinate coordinateA in VertexStore.Select(entry => entry.Key))
            {
                foreach (VertexCoordinate coordinateB in coordinateA.Neighbors().Where(vertex => VertexStore.ContainsKey(vertex)))
                {
                    EdgeCoordinate edge = new(coordinateA, coordinateB);
                    TRoad road = new() { Edge = edge };
                    _ = EdgeStore.TryAdd(edge, road);
                }

            }
        }

        public IEnumerable<float> ToVector(ICatan catan)
        {
            IEnumerable<float> terrainTileEncoding = TraverseOrder.VectorizeBoardTerrain(TileStore, catan);
            IEnumerable<float> edgeTileEncoding = TraverseOrder.VectorizeBoardEdge(TileStore, catan);
            IEnumerable<float> settlementEncoding = TraverseOrder.VectorizeSettlements(VertexStore, catan);
            IEnumerable<float> roadEncoding = TraverseOrder.VectorizeRoads(EdgeStore, catan);

            return terrainTileEncoding.Concat(edgeTileEncoding).Concat(settlementEncoding).Concat(roadEncoding);
        }

        public IEnumerable<Action<ICatan>> GetActions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Func<ICatan, bool>> CanExecuteActions()
        {
            throw new NotImplementedException();
        }
    }
}
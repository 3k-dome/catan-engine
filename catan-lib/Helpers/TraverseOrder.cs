using CatanLib.Interfaces.Components;
using HexagonLib;
using HexagonLib.Utils;

namespace CatanLib.Helpers
{
    public static class TraverseOrder
    {
        public static Lazy<IEnumerable<TileCoordinate>> TerrainTileOrder { get; } = new(() =>
        {
            return TileOperations.Spiral(new TileCoordinate(0, 0, 0), HexagonLib.Enums.TileNeighbor.NorthWest, 2).ToArray();
        });

        public static IEnumerable<float> VectorizeBoardTerrain(Dictionary<TileCoordinate, IHexTile> tileStore, ICatan catan)
        {
            return TerrainTileOrder.Value.Select(coordinate => tileStore[coordinate])
                .OfType<ITerrainTile>()
                .SelectMany(tile => tile.ToVector(catan));
        }

        public static Lazy<IEnumerable<TileCoordinate>> EdgeTileOrder { get; } = new(() =>
        {
            IEnumerable<TileCoordinate> coordinates = TileOperations.Circle(new TileCoordinate(0, 0, 0), HexagonLib.Enums.TileNeighbor.NorthWest, 3);
            return coordinates.Skip(1).Concat(coordinates.Take(1)).ToArray();
        });

        public static IEnumerable<float> VectorizeBoardEdge(Dictionary<TileCoordinate, IHexTile> tileStore, ICatan catan)
        {
            return EdgeTileOrder.Value.Select(coordinate => tileStore[coordinate])
                .OfType<IEdgeTile>()
                .SelectMany(tile => tile.ToVector(catan));
        }

        public static Lazy<IEnumerable<VertexCoordinate>> SettlementOrder { get; } = new(() =>
        {
            return TerrainTileOrder.Value.SelectMany(coordinate => coordinate.Vertices())
                .Distinct()
                .ToArray();
        });

        public static IEnumerable<float> VectorizeSettlements(Dictionary<VertexCoordinate, ISettlement> vertexStore, ICatan catan)
        {
            return SettlementOrder.Value.SelectMany(vertex => vertexStore[vertex].ToVector(catan));
        }

        public static Lazy<IEnumerable<EdgeCoordinate>> RoadOrder { get; } = new(() =>
        {
            return SettlementOrder.Value.SelectMany(vertex => vertex.Edges())
                .Distinct()
                .Where(edge => edge.Vertices().All(vertex => SettlementOrder.Value.Contains(vertex)))
                .ToArray();
        });

        public static IEnumerable<float> VectorizeRoads(Dictionary<EdgeCoordinate, IRoad> edgeStore, ICatan catan)
        {
            return RoadOrder.Value.SelectMany(edge => edgeStore[edge].ToVector(catan));
        }
    }
}

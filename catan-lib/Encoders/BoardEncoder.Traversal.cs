using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;
using CatanLib.Interfaces.Components.Tiles;
using HexagonLib;
using HexagonLib.Enums;
using HexagonLib.Operations;

namespace CatanLib.Encoders;

public static partial class BoardEncoder
{
    /// <summary>
    /// A static order of tile coordinates used to access coasts during any form of vectorization.
    /// </summary>
    private static readonly Lazy<IEnumerable<TileCoordinate>> OrderedCostalCoordinates = new(
        () => TileOperations.Circle(new(0, 0, 0), TileNeighborDirection.NorthWest, 3)
            .ToArray()
    );

    /// <summary>
    /// Iterate all costal tiles in a fixed order.
    /// </summary>
    private static IEnumerable<ICostalTile> GetOrderedCostalTiles(IBoard board)
    {
        return OrderedCostalCoordinates.Value.Select(coordinate => board[coordinate])
            .Cast<ICostalTile>();
    }

    /// <summary>
    /// A static order of tile coordinates used to access trerrain during any form of vectorization.
    /// </summary>
    private static readonly Lazy<IEnumerable<TileCoordinate>> OrderedTerrainCoordinates = new(
        () => TileOperations.Spiral(new(0, 0, 0), TileNeighborDirection.NorthEast, 2)
            .ToArray()
    );

    /// <summary>
    /// Iterate all terrain tiles in a fixed order.
    /// </summary>
    private static IEnumerable<ITerrainTile> GetOrderedTerrainTiles(IBoard board)
    {
        return OrderedTerrainCoordinates.Value.Select(coordinate => board[coordinate])
            .Cast<ITerrainTile>();
    }

    /// <summary>
    /// A static order of vertedx coordinates used to access those during any form of vectorization.
    /// </summary>
    private static readonly Lazy<IEnumerable<VertexCoordinate>> OrderedVertices = new(
        () => OrderedTerrainCoordinates.Value.SelectMany(coordinate => coordinate.Vertices())
            .Distinct()
            .ToArray()
    );

    /// <summary>
    /// Iterate all settlements in a fixed order.
    /// </summary>
    private static IEnumerable<ISettlement> GetOrderedSettlements(IBoard board)
    {
        return OrderedVertices.Value.Select(vertex => board[vertex]);
    }

    /// <summary>
    /// A static order of edge coordinates used to access those during any form of vectorization.
    /// </summary>
    private static readonly Lazy<IEnumerable<EdgeCoordinate>> OrderedEdges = new(
        () => OrderedVertices.Value.SelectMany(vertex => vertex.Edges())
            .Where(edge => OrderedVertices.Value.Contains(edge.VertexA) && OrderedVertices.Value.Contains(edge.VertexB))
            .Distinct()
            .ToArray()
    );

    /// <summary>
    /// Iterate all roads in a fixed order.
    /// </summary>
    private static IEnumerable<IRoad> GetOrderedRoads(IBoard board)
    {
        return OrderedEdges.Value.Select(edge => board[edge]);
    }
}

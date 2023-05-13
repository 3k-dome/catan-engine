using CatanLib.Interfaces.Components;
using CatanLib.Parts;
using HexagonLib;

namespace CatanLib.Helpers
{
    public static class TraverseRoads
    {
        public static bool IsValid(Board board, VertexCoordinate vertex, IPlayer player)
        {
            ISettlement settlement = board[vertex];
            return settlement.Belongs is null || settlement.Belongs.Number == player.Number;
        }

        public static bool IsValid(Board board, EdgeCoordinate edge, IPlayer player)
        {
            IRoad road = board[edge];
            return road.Belongs is not null && road.Belongs.Number == player.Number;
        }

        public static IEnumerable<EdgeCoordinate> RootRoads(Board board, IPlayer player)
        {
            IEnumerable<EdgeCoordinate> playersEdges = board.EdgeStore
                .Select(entry => entry.Key)
                .Where(edge => IsValid(board, edge, player));


            return playersEdges.Where(edge =>
            {
                IEnumerable<VertexCoordinate> sharedVertices = edge.Neighbors()
                    .SelectMany(neighbor => neighbor.Vertices())
                    .Distinct()
                    .Intersect(edge.Vertices());

                return sharedVertices.Count() <= 1;
            });
        }

        public static VertexCoordinate? FindOutgoing(Board board, EdgeCoordinate edge, IPlayer player)
        {
            IEnumerable<VertexCoordinate> outgoingVertices = edge.Vertices()
                .Where(vertex => IsValid(board, vertex, player))
                .Where(vertex => edge.Traverse(vertex)
                    .Where(outgoingEdge => IsValid(board, outgoingEdge, player))
                    .Any()
                );

            return outgoingVertices.Count() switch
            {
                0 => null,
                1 => outgoingVertices.First(),
                _ => throw new Exception("Given edge has more than one outgoing sides."),
            };
        }

        public static VertexCoordinate? FindOutgoing(Board board, EdgeCoordinate edge, VertexCoordinate incommingVertex, IPlayer player)
        {
            VertexCoordinate outgoingVertex = edge.Other(incommingVertex);

            if (!IsValid(board, outgoingVertex, player))
            {
                return null;
            }

            bool hasConnecting = edge.Traverse(outgoingVertex)
                .Where(outgoingEdge => IsValid(board, outgoingEdge, player))
                .Any();

            return hasConnecting ? outgoingVertex : null;
        }
    }
}

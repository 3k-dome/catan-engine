using CatanLib.Interfaces.Components;
using CatanLib.Parts;
using HexagonLib;

namespace CatanLib.Helpers
{
    public static class TraverseRoads
    {
        private static bool IsValid(Board board, VertexCoordinate vertex, IPlayer player)
        {
            ISettlement settlement = board[vertex];
            return settlement.Belongs is null || settlement.Belongs.Number == player.Number;
        }

        private static bool IsValid(Board board, EdgeCoordinate edge, IPlayer player)
        {
            IRoad road = board[edge];
            return road.Belongs is not null && road.Belongs.Number == player.Number;
        }

        private static IEnumerable<EdgeCoordinate> GetPlayersRoads(Board board, IPlayer player)
        {
            return board.EdgeStore
                .Select(entry => entry.Key)
                .Where(edge => IsValid(board, edge, player));
        }

        private static IEnumerable<EdgeCoordinate> GetRootRoads(IEnumerable<EdgeCoordinate> playersEdges, Board board, IPlayer player)
        {
            foreach (EdgeCoordinate edge in playersEdges)
            {
                try
                {
                    _ = FindOutgoing(board, edge, player);
                }

                // more than one outgoing side
                catch (Exception)
                {
                    continue;
                }

                // none or exactly one outgoing side
                yield return edge;
            }
        }

        private static VertexCoordinate? FindOutgoing(Board board, EdgeCoordinate edge, IPlayer player)
        {
            IEnumerable<VertexCoordinate> outgoingVertices = edge.Vertices()
                .Where(vertex => IsValid(board, vertex, player))
                .Where(vertex => TraverseValid(board, edge, vertex, player).Any());

            return outgoingVertices.Count() switch
            {
                0 => null,
                1 => outgoingVertices.First(),
                _ => throw new Exception("Given edge has more than one outgoing sides."),
            };
        }

        private static IEnumerable<EdgeCoordinate> TraverseValid(Board board, EdgeCoordinate currentEdge, VertexCoordinate outgoingVertex, IPlayer player)
        {
            return currentEdge.Traverse(outgoingVertex)
                .Where(outgoingEdge => IsValid(board, outgoingEdge, player));
        }

        private static int TraverseRootRoad(Board board, EdgeCoordinate root, IPlayer player, HashSet<EdgeCoordinate> totalRoads)
        {
            VertexCoordinate? outgoingVertex = FindOutgoing(board, root, player);

            int max = 1;
            if (outgoingVertex is null)
            {
                _ = totalRoads.Remove(root);
                return max;
            }

            TraverseRoad(board, root, outgoingVertex, player, max, ref max, new() { root.Other(outgoingVertex) }, totalRoads);
            return max;
        }

        private static int TraverseCicularRoad(Board board, EdgeCoordinate edge, IPlayer player, HashSet<EdgeCoordinate> totalRoads)
        {
            int max = 1;
            TraverseRoad(board, edge, edge.VertexA, player, max, ref max, new() { edge.VertexB }, totalRoads);
            return max;
        }

        private static void TraverseRoad(Board board, EdgeCoordinate currentEdge, VertexCoordinate outgoingVertex, IPlayer player, int depth, ref int max, HashSet<VertexCoordinate> seenVetices, HashSet<EdgeCoordinate> totalRoads)
        {
            _ = totalRoads.Remove(currentEdge);
            if (IsValid(board, outgoingVertex, player) && !seenVetices.Contains(outgoingVertex))
            {
                IEnumerable<EdgeCoordinate> validEdges = TraverseValid(board, currentEdge, outgoingVertex, player);
                if (validEdges.Any())
                {
                    depth++;
                    max = depth > max ? depth : max;

                    foreach (EdgeCoordinate validEdge in validEdges)
                    {
                        TraverseRoad(board, validEdge, validEdge.Other(outgoingVertex), player, depth, ref max, new(seenVetices) { outgoingVertex }, totalRoads);
                    }
                }
            }
        }

        public static void UpdateLongestRoad(ICatan catan)
        {
            Board board = catan.Board;
            foreach (IPlayer player in catan.Players)
            {
                HashSet<EdgeCoordinate> roads = GetPlayersRoads(board, player).ToHashSet();
                IEnumerable<EdgeCoordinate> roots = GetRootRoads(roads, board, player);

                // travers all valid roots and single roads
                IList<int> lengths = roots.Select(root => TraverseRootRoad(board, root, player, roads)).ToList();

                // if roads are still left they must belong to a circular network
                while (roads.Count > 0)
                {
                    lengths.Add(TraverseCicularRoad(board, roads.First(), player, roads));
                }

                player.LongestRoadLength = lengths.OrderByDescending(length => length).FirstOrDefault();
            }

            // secordary ordering by HasLongestRoad aligns players if tied for the longest road
            IOrderedEnumerable<IPlayer> orderedPlayers = catan.Players.OrderByDescending(player => player.LongestRoadLength)
                .ThenByDescending(player => player.HasLongestRoad);

            _ = orderedPlayers.Skip(1).Select(player => player.HasLongestRoad = false);
            orderedPlayers.First().HasLongestRoad = true;
        }
    }
}

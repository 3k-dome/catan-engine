using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Rules;

public static class SettlementPlacement
{
    /// <summary>
    /// A building spot is valid if all surrounding building spots are empty.
    /// </summary>
    public static bool DistanceRule(IBoard board, ISettlement settlement)
    {
        return settlement.Coordinate.Neighbors()
            .Where(vertex => board.Settlements.ContainsKey(vertex))
            .Select(vertex => board[vertex])
            .All(settlement => settlement.Owner is null);
    }

    /// <summary>
    /// A building spot is valid if it connects to atleast one road owned by the player.
    /// </summary>
    public static bool PlacementRule(IBoard board, IPlayer player, ISettlement settlement)
    {
        return settlement.Coordinate.Edges()
            .Where(edge => board.Roads.ContainsKey(edge))
            .Select(edge => board[edge])
            .Any(road => road.Owner == player);
    }

    /// <summary>
    /// During the settlement phase two settlements and roads are placed each in a specific order.
    /// A Settlement is placed first (no roads are placed yet) and again after the first road was
    /// placed (exactly one road is placed).
    /// </summary>
    public static bool SettlementPhaseConstraint(IBoard board, IPlayer player)
    {
        int noRoads = board.Roads.Select(pair => pair.Value)
            .Where(road => road.Owner == player)
            .Count();

        return noRoads is 0 or 1;
    }
}

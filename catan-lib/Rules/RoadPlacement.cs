using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanLib.Rules;

public static class RoadPlacement
{
    /// <summary>
    /// A building spot is valid if it connects to a settlement owned by the player.
    /// </summary>
    private static bool AdjacentSettlement(IBoard board, IPlayer player, IRoad road)
    {
        return road.Edge.Vertices()
            .Select(vertex => board[vertex])
            .Where(settlement => settlement.Owner == player)
            .Any();
    }

    /// <summary>
    /// A building spot is valid if it connects to a road owned by the player and
    /// the connection between those two is not iterrupted by another players settlement.
    /// </summary>
    private static bool AdjacentRoad(IBoard board, IPlayer player, IRoad road)
    {
        return road.Edge.Neighbors()
            // select adjacent roads owned by the player
            .Select(edge => board[edge])
            .Where(adjacentRoad => adjacentRoad.Owner == player)

            // select the settlement where the roads connect to each other
            .Select(adjacentRoad => road.Edge.Vertices().Intersect(adjacentRoad.Edge.Vertices()).First())
            .Select(vertex => board[vertex])

            // see if the intersection settlement is unowned or owned by the player
            .Where(settlement => settlement.Owner is null || settlement.Owner == player)
            .Any();
    }

    /// <summary>
    /// A building spot is valid if connects to a settlement owned by the player
    /// or if it is part of an uninterrupted road.
    /// </summary>
    public static bool PlacementRule(IBoard board, IPlayer player, IRoad road)
    {
        return AdjacentSettlement(board, player, road) || AdjacentRoad(board, player, road);
    }

    /// <summary>
    /// During the settlement phase two settlements and roads are placed each in a specific order.
    /// A road is placed after the first settlment was built (exactly one settlement is placed) 
    /// and again after the second settlment was built (exactly two settlements are placed).
    /// </summary>
    public static bool SettlementPhaseConstraint(IBoard board, IPlayer player)
    {
        int noSettlements = board.Settlements.Select(pair => pair.Value)
            .Where(settlement => settlement.Owner == player)
            .Count();

        return noSettlements is 1 or 2;
    }
}

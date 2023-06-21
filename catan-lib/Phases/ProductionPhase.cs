using CatanLib.Enums;
using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;
using CatanLib.Interfaces.Components.Tiles;
using HexagonLib;
using System.Diagnostics;

namespace CatanLib.Phases;

public static class ProductionPhase
{
    private static IEnumerable<IGrouping<Terrain, TileCoordinate>> GatherAffectedTiles(IBoard board, int diceRoll)
    {
        return board.Tiles.OfType<ITerrainTile>()
            .Where(tile => tile.NumberToken.Roll == diceRoll)
            .GroupBy(
                tile =>
                {
                    Debug.Assert(tile.TerrainType is not null);
                    return (Terrain)tile.TerrainType;
                },
                tile => tile.Coordinate
            );
    }

    private static void AddResource(IPlayer player, IBank bank, Resource resource, int count)
    {
        while (count > 0 && bank.Resources.Contains(resource))
        {
            player.Resources.Add(resource);
            bank.Resources.Remove(resource);
            count--;
        }
    }

    private static void DistributeResources(IEnumerable<ISettlement> settlements, IBank bank, Resource resource)
    {
        foreach (ISettlement settlement in settlements)
        {
            Debug.Assert(settlement.Owner is not null);
            AddResource(settlement.Owner, bank, resource, settlement.IsUpgraded ? 2 : 1);
        }
    }

    public static void Yield(IBoard board, IBank bank, int diceRoll)
    {
        if (diceRoll == 7)
        {
            throw new ArgumentException("This should not be called if a 7 was rolled!");
        }

        IEnumerable<IGrouping<Terrain, TileCoordinate>> affectedTiles = GatherAffectedTiles(board, diceRoll);

        foreach (IGrouping<Terrain, TileCoordinate> group in affectedTiles)
        {
            Resource resource = ResourceLookup.GetResource(group.Key);
            IEnumerable<ISettlement> settlements = group
                .SelectMany(coordinate => coordinate.Vertices())
                .Select(vertex => board[vertex])
                .Where(settlement => settlement.Owner is not null);

            // only yield all resources if the bank can provide the total yield

            int required = settlements.Select(settlement => settlement.IsUpgraded ? 2 : 1).Sum();
            IEnumerable<Resource> requiredResources = Enumerable.Repeat(resource, required);
            bool satisfiable = bank.Resources.ContainsRange(requiredResources);

            // yield as much as the bank can provide if only one player is affected

            int affectedPlayers = settlements.Select(settlement => settlement.Owner)
                .Distinct()
                .Count();

            if (satisfiable || affectedPlayers == 1)
            {
                DistributeResources(settlements, bank, resource);
            }
        }
    }
}

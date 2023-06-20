using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;

namespace CatanLib.Encoders;

public static partial class BoardEncoder
{
    private static IEnumerable<Action<ICatan>> GetSettlementActions(IBoard board)
    {
        return GetOrderedSettlements(board)
            .SelectMany(settlement => new[]
            {
                settlement.ExecutePrimary,
                settlement.ExecuteSecondary
            });
    }

    private static IEnumerable<Func<ICatan, bool>> GetSettlementActionMask(IBoard board)
    {
        return GetOrderedSettlements(board)
            .SelectMany(settlement => new[]
            {
                settlement.CanExecutePrimary,
                settlement.CanExecuteSecondary
            });
    }

    private static IEnumerable<Action<ICatan>> GetRoadActions(IBoard board)
    {
        return GetOrderedRoads(board)
            .Select<IRoad, Action<ICatan>>(road => road.ExecutePrimary);
    }

    private static IEnumerable<Func<ICatan, bool>> GetRoadActionMask(IBoard board)
    {
        return GetOrderedRoads(board)
            .Select<IRoad, Func<ICatan, bool>>(road => road.CanExecutePrimary);
    }

    public static IEnumerable<Action<ICatan>> GetActions(IBoard board)
    {
        return Enumerable.Empty<Action<ICatan>>()
            .Concat(GetSettlementActions(board))
            .Concat(GetRoadActions(board));
    }

    public static IEnumerable<Func<ICatan, bool>> GetActionMask(IBoard board)
    {
        return Enumerable.Empty<Func<ICatan, bool>>()
            .Concat(GetSettlementActionMask(board))
            .Concat(GetRoadActionMask(board));
    }

    public static IEnumerable<float> EncodeBoard(IBoard board, ICatan catan)
    {
        return Enumerable.Empty<float>()
            .Concat(GetOrderedCostalTiles(board).SelectMany(tile => tile.ToVector(catan)))
            .Concat(GetOrderedTerrainTiles(board).SelectMany(tile => tile.ToVector(catan)))
            .Concat(GetOrderedSettlements(board).SelectMany(settlement => settlement.ToVector(catan)))
            .Concat(GetOrderedRoads(board).SelectMany(road => road.ToVector(catan)));

    }
}

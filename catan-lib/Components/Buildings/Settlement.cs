using CatanLib.Encoders;
using CatanLib.Enums;
using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;
using CatanLib.Rules;
using HexagonLib;

namespace CatanLib.Components.Buildings;

public class Settlement : ISettlement
{
    public VertexCoordinate Coordinate { get; private set; }
    public IPlayer? Owner { get; private set; }
    public Piece Piece { get; } = Piece.Settlement;
    public IEnumerable<Resource> Costs { get; } = new Resource[]
    {
        Resource.Wood,
        Resource.Brick,
        Resource.Wheat,
        Resource.Sheep,
    };

    public bool IsUpgraded { get; private set; } = false;
    public Piece UpgradedPiece { get; } = Piece.City;
    public IEnumerable<Resource> UpgradeCosts { get; } = new Resource[]
    {
        Resource.Wheat,
        Resource.Wheat,
        Resource.Ore,
        Resource.Ore,
        Resource.Ore,
    };

    public Settlement(VertexCoordinate vertex)
    {
        Coordinate = vertex;
    }

    public void ExecutePrimary(ICatan catan)
    {
        IPlayer player = catan.CurrentPlayer;

        player.Pieces.Remove(Piece);
        player.Resources.RemoveRange(Costs);

        Owner = player;
    }

    public bool CanExecutePrimary(ICatan catan)
    {
        if (Owner is not null)
        {
            return false;
        }

        (IBoard board, IPlayer player) = (catan.Board, catan.CurrentPlayer);

        bool hasPiece = player.Pieces.Contains(Piece);
        bool hasResources = player.Resources.ContainsRange(Costs);

        bool distanceRule = SettlementPlacement.DistanceRule(board, this);
        bool placementRule = SettlementPlacement.PlacementRule(board, player, this);

        return catan.CurrentPhase switch
        {
            Phase.Settlement => hasPiece && distanceRule && SettlementPlacement.SettlementPhaseConstraint(board, player),
            Phase.Building => hasPiece && hasResources && distanceRule && placementRule,
            _ => false
        };
    }

    public void ExecuteSecondary(ICatan catan)
    {
        IPlayer player = catan.CurrentPlayer;

        player.Pieces.Add(Piece);
        player.Pieces.Remove(UpgradedPiece);
        player.Resources.RemoveRange(UpgradeCosts);

        IsUpgraded = true;
    }

    public bool CanExecuteSecondary(ICatan catan)
    {
        IPlayer player = catan.CurrentPlayer;

        if (Owner != player)
        {
            return false;
        }

        bool hasPiece = player.Pieces.Contains(UpgradedPiece);
        bool hasResources = player.Resources.ContainsRange(UpgradeCosts);

        return catan.CurrentPhase switch
        {
            Phase.Building => hasPiece && hasResources && !IsUpgraded,
            _ => false
        };
    }

    public IEnumerable<float> ToVector(ICatan catan)
    {
        IEnumerable<float> ownership = PlayerEncoder.EncodeOwnership(catan, this);
        return ownership.Append(IsUpgraded ? 1f : 0f);
    }

    public IEnumerable<string> ToDescriptiveVector(ICatan catan)
    {
        IEnumerable<string> ownership = PlayerEncoder.ProvidePlayerOrder(catan)
            .Select(player => $"Owned by player {player.Number}");
        return ownership.Append("Is city");
    }
}

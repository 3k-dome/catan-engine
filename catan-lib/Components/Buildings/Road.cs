using CatanLib.Encoders;
using CatanLib.Enums;
using CatanLib.Interfaces.Components.Buildings;
using CatanLib.Interfaces.Components.Other;
using CatanLib.Rules;
using HexagonLib;

namespace CatanLib.Components.Buildings;

public class Road : IRoad
{
    public EdgeCoordinate Edge { get; private set; }
    public IPlayer? Owner { get; private set; }
    public Piece Piece { get; } = Piece.Road;
    public IEnumerable<Resource> Costs { get; } = new Resource[]
    {
        Resource.Wood,
        Resource.Brick,
    };

    public Road(EdgeCoordinate edge)
    {
        Edge = edge;
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

        bool placementRule = RoadPlacement.PlacementRule(board, player, this);

        return catan.CurrentPhase switch
        {
            Phase.Settlement => hasPiece && placementRule && RoadPlacement.SettlementPhaseConstraint(board, player),
            Phase.Building => hasPiece && hasResources && placementRule,
            _ => false
        };
    }

    public IEnumerable<float> ToVector(ICatan catan)
    {
        IEnumerable<float> ownership = PlayerEncoder.EncodeOwnership(catan, this);
        return ownership;
    }

    public IEnumerable<string> ToDescriptiveVector(ICatan catan)
    {
        IEnumerable<string> ownership = PlayerEncoder.ProvidePlayerOrder(catan)
           .Select(player => $"Owned by player {player.Number}");
        return ownership;
    }
}

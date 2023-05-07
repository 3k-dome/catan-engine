﻿using CatanLib.Enums;
using CatanLib.Helpers;
using CatanLib.Interfaces.Components;
using HexagonLib;

namespace CatanLib.Parts
{
    public class Settlement : ISettlement
    {
        private VertexCoordinate? vertex;
        public VertexCoordinate Vertex
        {
            get => vertex ?? throw new NullReferenceException();
            set => vertex ??= value;
        }

        public bool IsSettlement { get; private set; }
        public bool IsCity { get; private set; }
        public IPlayer? Belongs { get; private set; }
        public IEnumerable<ResourceType> ResourceCosts { get; } = new[]
        {
            ResourceType.Wood,
            ResourceType.Brick,
            ResourceType.Wheat,
            ResourceType.Sheep,
        };
        public PieceType RequiredPiece { get; } = PieceType.Settlement;

        public IEnumerable<ResourceType> UpgradeResourceCosts { get; } = new[]
        {
            ResourceType.Wheat,
            ResourceType.Wheat,
            ResourceType.Ore,
            ResourceType.Ore,
            ResourceType.Ore,
        };
        public PieceType RequiredUpgradePiece { get; } = PieceType.City;

        public void Play<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            catan.CurrentPlayer.UseResources(ResourceCosts);
            catan.CurrentPlayer.PlacePiece(RequiredPiece);
            Belongs = catan.CurrentPlayer;
            (IsSettlement, IsCity) = (true, false);
        }

        public bool CanPlay<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            bool hasOwner = Belongs != null;
            bool hasResources = catan.CurrentPlayer.HasResources(ResourceCosts);
            bool hasPiece = catan.CurrentPlayer.HasPiece(RequiredPiece);

            bool distanceRule = Vertex.Neighbors()
                .Select(vertex => catan.Board[vertex])
                .All(settlement => !settlement.IsSettlement && !settlement.IsCity);

            bool placementRule = Vertex.Edges()
                .Select(edge => catan.Board[edge])
                .Any(road => road.Belongs == catan.CurrentPlayer);

            return !hasOwner && hasResources && hasPiece && distanceRule && placementRule;
        }

        public void Upgrade<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            catan.CurrentPlayer.UseResources(UpgradeResourceCosts);
            catan.CurrentPlayer.TakePiece(RequiredPiece);
            catan.CurrentPlayer.PlacePiece(RequiredUpgradePiece);
            (IsSettlement, IsCity) = (false, true);
        }

        public bool CanUpgrade<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            bool isUpgradable = IsSettlement && !IsCity;
            bool isOwner = Belongs == catan.CurrentPlayer;
            bool hasResources = catan.CurrentPlayer.HasResources(UpgradeResourceCosts);
            bool hasPiece = catan.CurrentPlayer.HasPiece(RequiredUpgradePiece);

            return isUpgradable && isOwner && hasResources && hasPiece;
        }

        public IEnumerable<Action<Catan<TSettlement, TRoad, TDice>>> GetActions<TSettlement, TRoad, TDice>()
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            yield return Play;
            yield return Upgrade;
        }

        public IEnumerable<Func<Catan<TSettlement, TRoad, TDice>, bool>> CanExecuteActions<TSettlement, TRoad, TDice>()
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            yield return CanPlay;
            yield return CanUpgrade;
        }

        public IEnumerable<float> ToVector<TSettlement, TRoad, TDice>(Catan<TSettlement, TRoad, TDice> catan)
        where TSettlement : ISettlement, new()
        where TRoad : IRoad, new()
        where TDice : IDice, new()
        {
            IEnumerable<float> playerEndoding = PlayerEncoding.Encode(catan.CurrentPlayer, Belongs);

            float[] stateEncoding = new float[2];
            stateEncoding[0] = IsSettlement ? 1 : 0;
            stateEncoding[1] = IsCity ? 1 : 0;

            return playerEndoding.Concat(stateEncoding);
        }
    }
}

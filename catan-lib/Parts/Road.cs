using CatanLib.Enums;
using CatanLib.Interfaces;
using HexagonLib;

namespace CatanLib.Parts
{
    public class Road : IRoad
    {
        private EdgeCoordinate? edge;
        public EdgeCoordinate Edge {
            get => edge ?? throw new NullReferenceException();
            set => edge ??= value;
        }

        public bool IsRoad { get; private set; }
        public PlayerNumber? Belongs { get; private set; }

        private readonly IEnumerable<ResourceType> costs = new[] { ResourceType.Wood, ResourceType.Brick };
        public IEnumerable<ResourceType> Costs => costs;

        public IEnumerable<float> ToVector()
        {
            float[] playerEncoding = new float[Enum.GetValues<PlayerNumber>().Length];
            if (Belongs != null)
            {
                playerEncoding[(int)Belongs] = 1;
            }

            float[] buildingEncoding = new float[] { IsRoad ? 1 : 0 };
            return Enumerable.Concat(playerEncoding, buildingEncoding);
        }

        public IEnumerable<string> ToExplainedVector()
        {
            throw new NotImplementedException();
        }

        public void ActionPlace()
        {
            // this should; each game element that has actions
            // should have access to the game itself, i.e. a container
            // for the boad and players
            // actions could then be performed from this container
            throw new NotImplementedException();
        }

        public bool CanPlace()
        {
            // see action
            // player needs enought resources & a spare road object
            // no road was build here yet
            throw new NotImplementedException();
        }

        public IEnumerable<Action> GetActions()
        {
            return new Action[] { ActionPlace };
        }

        public IEnumerable<Func<bool>> CanExecuteActions()
        {
            return new Func<bool>[] { CanPlace };
        }
    }
}

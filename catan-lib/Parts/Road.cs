using CatanLib.Enums;
using CatanLib.Interfaces;
using HexagonLib;

namespace CatanLib.Parts
{
    public class Road : IRoad
    {
        private EdgeCoordinate? edge;
        public EdgeCoordinate Edge
        {
            get => edge ?? throw new NullReferenceException();
            set => edge ??= value;
        }

        public bool IsRoad { get; private set; }

        public PlayerNumber? Belongs { get; private set; }

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
    }
}

using CatanLib.Enums;
using CatanLib.Interfaces.Components;

namespace CatanLib.Helpers
{
    public static class PlayerEncoding
    {
        public static IEnumerable<IPlayer> Order(IEnumerable<IPlayer> players, IPlayer current)
        {
            IEnumerable<IPlayer> opponets = players.Where(player => player.Number != current.Number)
                .OrderByDescending(player => player.Number);

            opponets = opponets.Append(current);
            return opponets.Reverse();
        }

        public static IEnumerable<float> Encode(IPlayer current, IPlayer? encode)
        {
            IEnumerable<PlayerNumber> playerNumers = Enum.GetValues<PlayerNumber>()
                .Except(new[] { current.Number })
                .OrderByDescending(number => number)
                .Append(current.Number)
                .Reverse();

            float[] encoding = new float[playerNumers.Count()];

            if (encode != null)
            {
                int index = playerNumers
                    .TakeWhile(number => number != encode.Number)
                    .Count();
                encoding[index] = 1;
            }

            return encoding;
        }
    }
}

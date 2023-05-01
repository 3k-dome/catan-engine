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
    }
}

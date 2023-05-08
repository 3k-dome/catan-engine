using CatanLib.Parts;

namespace CatanLib.Interfaces.Components;
public interface ICatan
{
    IDice Dice { get; }
    IBank Bank { get; }
    Board Board { get; }
    IEnumerable<IPlayer> Players { get; }
    IPlayer CurrentPlayer { get; }
}

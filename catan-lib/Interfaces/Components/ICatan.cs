namespace CatanLib.Interfaces;
public interface ICatan
{
    IEnumerable<IPlayer> Players { get; }
    IPlayer CurrentPlayer { get; }
    IDice Dice { get; }
}

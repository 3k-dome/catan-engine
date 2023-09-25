using CatanLib.Interfaces.Interaction.Other;

namespace CatanLib.Interfaces.Interaction.Collections;

public interface IEnumCollection<T> : IResettable where T : Enum
{
    Dictionary<T, uint> Items { get; }

    void Add(T item);
    void AddRange(IEnumerable<T> item);
    void Remove(T item);
    void RemoveRange(IEnumerable<T> item);
    bool Contains(T item);
    bool ContainsRange(IEnumerable<T> item);

    uint Count();
    uint Bound();

    IEnumerable<T> Available();
    IEnumerable<T> Flatten();

    public static void MoveValue(IEnumCollection<T> from, IEnumCollection<T> to, T value)
    {
        from.Remove(value);
        to.Add(value);
    }

    public static void MoveValues(IEnumCollection<T> from, IEnumCollection<T> to, IEnumerable<T> values)
    {
        from.RemoveRange(values);
        to.AddRange(values);
    }

    public IEnumerable<(T key, float value)> Encode();
}

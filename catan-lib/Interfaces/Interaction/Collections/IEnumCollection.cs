using CatanLib.Interfaces.Interaction.Other;
using CatanLib.Interfaces.Interaction.Vectorization;

namespace CatanLib.Interfaces.Interaction.Collections;

public interface IEnumCollection<T> : IResetable, IVectorizableComponent where T : Enum
{
    Dictionary<T, uint> Items { get; }

    void Add(T item);
    void AddRange(IEnumerable<T> item);
    void Remove(T item);
    void RemoveRange(IEnumerable<T> item);
    bool Contains(T item);
    bool ContainsRange(IEnumerable<T> item);
}

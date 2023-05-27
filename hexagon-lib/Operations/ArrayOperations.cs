namespace HexagonLib.Operations;

public static class ArrayOperations
{
    public static T[] Roll<T>(T[] array, int positions)
    {
        if (positions >= array.Length)
        {
            throw new ArgumentException(
                "This roll would rotate the entire array at least once."
                + " Only non-repeating rotations are currently possible."
            );
        }

        T[] result = new T[array.Length];
        array[positions..].CopyTo(result, 0);
        array[..positions].CopyTo(result, array.Length - positions);
        return result;
    }
}

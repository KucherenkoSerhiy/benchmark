namespace Application;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
    {
        using var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            yield return GetBatch(enumerator, size).ToList();
        }
    }

    private static IEnumerable<T> GetBatch<T>(IEnumerator<T> source, int size)
    {
        do
        {
            yield return source.Current; 
        } while (--size > 0
                 && source.MoveNext());
    }
}


namespace YK.Core.Extensions;

public static partial class Extensions
{
    public static void ForEachItem<T>(this IEnumerable<T> list, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in list)
        {
            action(item);
        }
    }

    public static IEnumerable<T> WhereIF<T>(this IEnumerable<T> list, bool condition, Func<T, bool> where)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(where);

        if (condition) return list.Where(where);
        return list;
    }
}

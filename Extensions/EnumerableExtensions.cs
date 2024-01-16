namespace ktsu.io.Extensions;

/// <summary>
/// Extension methods for enumerables.
/// </summary>
public static class EnumerableExtensions
{
	//from https://thomaslevesque.com/2019/11/18/using-foreach-with-index-in-c/
	/// <summary>
	/// Method that enumerates over an enumerable with the index of the item.
	/// </summary>
	/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
	/// <param name="source">The enumerable to enumerate over.</param>
	/// <returns>An enumerable of tuples containing the item and the index of the item.</returns>
	public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source) => source.Select((item, index) => (item, index));
}

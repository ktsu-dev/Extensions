namespace ktsu.io.Extensions;

using System.Collections.ObjectModel;

/// <summary>
/// Extension methods for enumerables.
/// </summary>
public static class EnumerableExtensions
{
	// from https://thomaslevesque.com/2019/11/18/using-foreach-with-index-in-c/
	/// <summary>
	/// Method that enumerates over an enumerable with the index of the item.
	/// </summary>
	/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
	/// <param name="source">The enumerable to enumerate over.</param>
	/// <returns>An enumerable of tuples containing the item and the index of the item.</returns>
	public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source) => source.Select((item, index) => (item, index));

	// from: https://stackoverflow.com/a/971092
	/// <summary>
	/// Method for adding an enumeration of items to a new collection.
	/// </summary>
	/// <typeparam name="T">The type of the items.</typeparam>
	/// <param name="items">The enumeration of items to add to the new collection.</param>
	/// <returns>The new collection with the items added.</returns>
	public static Collection<T> ToCollection<T>(this IEnumerable<T> items)
	{
		ArgumentNullException.ThrowIfNull(items, nameof(items));

		var collection = new Collection<T>();

		foreach (var item in items)
		{
			collection.Add(item);
		}

		return collection;
	}

	/// <summary>
	/// Apply an action to each item in an enumerable.
	/// </summary>
	/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
	/// <param name="enumerable">The enumerable to apply the action to.</param>
	/// <param name="action">The action to apply to each item in the enumerable.</param>
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
	{
		ArgumentNullException.ThrowIfNull(enumerable);
		ArgumentNullException.ThrowIfNull(action);

		foreach (var v in enumerable)
		{
			action(v);
		}
	}
}

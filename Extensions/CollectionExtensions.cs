namespace ktsu.io.Extensions;

/// <summary>
/// Extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
	/// <summary>
	/// Add items from an enumerable to a collection.
	/// </summary>
	/// <typeparam name="T">The type of the items.</typeparam>
	/// <param name="collection">The collection to add the items to.</param>
	/// <param name="items">The enumeration of items to add to the collection.</param>
	public static void AddMany<T>(this ICollection<T> collection, IEnumerable<T> items)
	{
		ArgumentNullException.ThrowIfNull(collection);
		ArgumentNullException.ThrowIfNull(items);

		foreach (var item in items)
		{
			collection.Add(item);
		}
	}
}

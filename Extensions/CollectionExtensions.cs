namespace ktsu.Extensions;

/// <summary>
/// Specifies how to handle null items when converting a collection to a string collection.
/// </summary>
public enum ToStringCollectionNullItemHandling
{
	/// <summary>
	/// Remove null items from the collection.
	/// </summary>
	Remove,

	/// <summary>
	/// Include null items in the collection.
	/// </summary>
	Include,

	/// <summary>
	/// Throw an exception if a null item is encountered.
	/// </summary>
	Throw,
}

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

	/// <summary>
	/// Checks if the collection contains any null items.
	/// </summary>
	/// <typeparam name="T">The type of the items in the collection.</typeparam>
	/// <param name="collection">The collection to check for null items.</param>
	/// <returns>True if the collection contains any null items; otherwise, false.</returns>
	public static bool AnyNull<T>(this ICollection<T> collection)
	{
		ArgumentNullException.ThrowIfNull(collection);
		return collection.Any(item => item is null);
	}

	/// <summary>
	/// Converts a collection to a collection of strings, handling null items according to the specified behavior.
	/// </summary>
	/// <typeparam name="T">The type of the items in the collection.</typeparam>
	/// <param name="collection">The collection to convert.</param>
	/// <param name="nullItemHandling">Specifies how to handle null items.</param>
	/// <returns>A collection of strings.</returns>
	public static ICollection<string?> ToStringCollection<T>(this ICollection<T> collection, ToStringCollectionNullItemHandling nullItemHandling)
	{
		ArgumentNullException.ThrowIfNull(collection);

		if (nullItemHandling == ToStringCollectionNullItemHandling.Throw)
		{
			if (collection.AnyNull())
			{
				throw new InvalidOperationException("The collection contains a null item.");
			}
		}

		return collection
			.Select(item => item?.ToString())
			.Where(item => nullItemHandling == ToStringCollectionNullItemHandling.Include || item is not null)
			.ToCollection();
	}

	/// <summary>
	/// Converts a collection to a collection of strings, removing null items by default.
	/// </summary>
	/// <typeparam name="T">The type of the items in the collection.</typeparam>
	/// <param name="collection">The collection to convert.</param>
	/// <returns>A collection of strings with null items removed.</returns>
	public static ICollection<string> ToStringCollection<T>(this ICollection<T> collection)
	{
		ArgumentNullException.ThrowIfNull(collection);

		return collection.ToStringCollection(ToStringCollectionNullItemHandling.Remove)
			.Select(item => item!)
			.ToCollection();
	}

	/// <summary>
	/// Writes the items of the collection to the console skipping null items.
	/// </summary>
	/// <typeparam name="T">The type of the items in the collection.</typeparam>
	/// <param name="collection">The collection whose items will be written to the console.</param>
	public static void WriteItemsToConsole<T>(this ICollection<T> collection) =>
		collection.ToStringCollection().ForEach(Console.WriteLine);
}

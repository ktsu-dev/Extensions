// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions;

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
	/// Replaces all items in a collection with items from an enumerable.
	/// </summary>
	/// <typeparam name="T">The type of the items.</typeparam>
	/// <param name="oldItems">The collection to replace items in.</param>
	/// <param name="newItems">The enumeration of new items to replace the old items with.</param>
	/// <exception cref="ArgumentNullException">
	/// Thrown if the <paramref name="oldItems"/> collection or the <paramref name="newItems"/> enumerable is null.
	/// </exception>
	public static void ReplaceWith<T>(this ICollection<T> oldItems, IEnumerable<T> newItems)
	{
		ArgumentNullException.ThrowIfNull(oldItems);
		ArgumentNullException.ThrowIfNull(newItems);

		oldItems.Clear();
		oldItems.AddMany(newItems);
	}
}

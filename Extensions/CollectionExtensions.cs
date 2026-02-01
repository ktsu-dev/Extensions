// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ktsu.Extensions.Test")]

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
	public static void AddFrom<T>(this ICollection<T> collection, IEnumerable<T> items)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (collection is null)
		{
			throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		foreach (T? item in items)
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
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (oldItems is null)
		{
			throw new ArgumentNullException(nameof(oldItems), "Old items collection cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (newItems is null)
		{
			throw new ArgumentNullException(nameof(newItems), "New items enumerable cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		oldItems.Clear();
		oldItems.AddFrom(newItems);
	}
}

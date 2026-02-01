// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions;

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
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		Collection<T> collection = [.. items];

		return collection;
	}

	// from: https://stackoverflow.com/a/971092
	/// <summary>
	/// Method for adding an enumeration of items to a new collection while locked.
	/// </summary>
	/// <typeparam name="T">The type of the items.</typeparam>
	/// <param name="items">The enumeration of items to add to the new collection.</param>
	/// <param name="lockObj">The object to lock on.</param>
	/// <returns>The new collection with the items added.</returns>
	public static Collection<T> ToCollection<T>(this IEnumerable<T> items, object lockObj)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (lockObj is null)
		{
			throw new ArgumentNullException(nameof(lockObj), "Lock object cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		lock (lockObj)
		{
			return ToCollection(items);
		}
	}

	/// <summary>
	/// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
	/// <param name="enumerable">The enumerable whose elements the action will be applied to. This cannot be null.</param>
	/// <param name="action">The action to perform on each element. This cannot be null.</param>
	/// <exception cref="ArgumentNullException">
	/// Thrown if the <paramref name="enumerable"/> or <paramref name="action"/> is null.
	/// </exception>
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (enumerable is null)
		{
			throw new ArgumentNullException(nameof(enumerable), "Enumerable cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (action is null)
		{
			throw new ArgumentNullException(nameof(action), "Action cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		foreach (T? v in enumerable)
		{
			action(v);
		}
	}

	/// <summary>
	/// Performs the specified action on each element of the <see cref="IEnumerable{T}"/> with thread-safety ensured by locking on the provided object.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
	/// <param name="enumerable">The enumerable whose elements the action will be applied to. This cannot be null.</param>
	/// <param name="lockObj">The object to lock on to ensure thread-safety during the enumeration. This cannot be null.</param>
	/// <param name="action">The action to perform on each element. This cannot be null.</param>
	/// <exception cref="ArgumentNullException">
	/// Thrown if the <paramref name="enumerable"/>, <paramref name="lockObj"/>, or <paramref name="action"/> is null.
	/// </exception>
	public static void ForEach<T>(this IEnumerable<T> enumerable, object lockObj, Action<T> action)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (enumerable is null)
		{
			throw new ArgumentNullException(nameof(enumerable), "Enumerable cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (lockObj is null)
		{
			throw new ArgumentNullException(nameof(lockObj), "Lock object cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (action is null)
		{
			throw new ArgumentNullException(nameof(action), "Action cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		lock (lockObj)
		{
			enumerable.ForEach(action);
		}
	}

	/// <summary>
	/// Checks if the enumerable contains any null items.
	/// </summary>
	/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
	/// <param name="items">The enumerable to check for null items.</param>
	/// <returns>True if the enumerable contains any null items; otherwise, false.</returns>
	public static bool AnyNull<T>(this IEnumerable<T> items)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		return items.Any(item => item is null);
	}

	/// <summary>
	/// Writes the items of the enumerable to the console, skipping null items.
	/// </summary>
	/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
	/// <param name="items">The enumerable whose items will be written to the console.</param>
	public static void WriteItemsToConsole<T>(this IEnumerable<T> items) =>
		items.ToStringEnumerable().ForEach(Console.WriteLine);

	/// <summary>
	/// Converts an enumerable to an enumerable of strings, removing null items by default.
	/// </summary>
	/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
	/// <param name="items">The enumerable to convert.</param>
	/// <returns>An enumerable of strings with null items removed.</returns>
	public static IEnumerable<string> ToStringEnumerable<T>(this IEnumerable<T> items)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		return items.ToStringEnumerable(NullItemHandling.Remove)
			.Select(item => item!);
	}

	/// <summary>
	/// Converts an enumerable to an enumerable of strings, handling null items according to the specified behavior.
	/// </summary>
	/// <typeparam name="T">The type of the items in the enumerable.</typeparam>
	/// <param name="items">The enumerable to convert.</param>
	/// <param name="nullItemHandling">Specifies how to handle null items.</param>
	/// <returns>An enumerable of strings.</returns>
	/// <exception cref="InvalidOperationException">Thrown if <paramref name="nullItemHandling"/> is set to <see cref="NullItemHandling.Throw"/> and the enumerable contains null items.</exception>
	public static IEnumerable<string?> ToStringEnumerable<T>(this IEnumerable<T> items, NullItemHandling nullItemHandling)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		if (nullItemHandling is NullItemHandling.Throw)
		{
			if (items.AnyNull())
			{
				throw new InvalidOperationException("The enumerable contains a null item.");
			}
		}

		return items
			.Select(item => item?.ToString())
			.Where(item => nullItemHandling is NullItemHandling.Include || item is not null);
	}

	/// <summary>
	/// Concatenates the elements of an enumerable as a string, using the specified separator between each element.
	/// </summary>
	/// <param name="items">The enumerable of items to concatenate. This cannot be null.</param>
	/// <param name="separator">The string to use as a separator. This cannot be null.</param>
	/// <returns>A string that consists of the elements in the enumerable delimited by the separator string.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> or <paramref name="separator"/> is null.</exception>
	public static string Join<T>(this IEnumerable<T> items, string separator)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (separator is null)
		{
			throw new ArgumentNullException(nameof(separator), "Separator cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		return items.Join(separator, NullItemHandling.Remove);
	}

	/// <summary>
	/// Concatenates the elements of an enumerable as a string, using the specified separator between each element,
	/// and handling null items according to the specified behavior.
	/// </summary>
	/// <param name="items">The enumerable of items to concatenate. This cannot be null.</param>
	/// <param name="separator">The string to use as a separator. This cannot be null.</param>
	/// <param name="nullItemHandling">Specifies how to handle null items in the enumerable.</param>
	/// <returns>A string that consists of the elements in the enumerable delimited by the separator string.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> or <paramref name="separator"/> is null.</exception>
	/// <exception cref="InvalidOperationException">Thrown if <paramref name="nullItemHandling"/> is set to <see cref="NullItemHandling.Throw"/> and the enumerable contains null items.</exception>
	public static string Join<T>(this IEnumerable<T> items, string separator, NullItemHandling nullItemHandling)
	{
#pragma warning disable KTSU0004 // Use Ensure.NotNull instead of manual null check
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Items cannot be null.");
		}
#pragma warning restore KTSU0004 // Use Ensure.NotNull instead of manual null check

		if (nullItemHandling is NullItemHandling.Throw)
		{
			if (items.AnyNull())
			{
				throw new InvalidOperationException("The enumerable contains a null item.");
			}
		}

		return string.Join(separator, items.Where(item => nullItemHandling is NullItemHandling.Include || item is not null).Select(i => i?.ToString()));
	}
}

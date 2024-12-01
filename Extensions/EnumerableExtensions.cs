namespace ktsu.Extensions;

using System.Collections.ObjectModel;
using System.Linq;
using ktsu.DeepClone;

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
		ArgumentNullException.ThrowIfNull(items);

		var collection = new Collection<T>();

		foreach (var item in items)
		{
			collection.Add(item);
		}

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
		ArgumentNullException.ThrowIfNull(items);
		ArgumentNullException.ThrowIfNull(lockObj);
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
		ArgumentNullException.ThrowIfNull(enumerable);
		ArgumentNullException.ThrowIfNull(action);

		foreach (var v in enumerable)
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
		ArgumentNullException.ThrowIfNull(enumerable);
		ArgumentNullException.ThrowIfNull(lockObj);
		ArgumentNullException.ThrowIfNull(action);

		lock (lockObj)
		{
			enumerable.ForEach(action);
		}
	}

	/// <summary>
	/// Creates a deep clone of a collection of items, returning a new collection of the specified type.
	/// </summary>
	/// <typeparam name="TItem">The type of the items in the collection, which must implement <see cref="IDeepCloneable{TItem}"/>.</typeparam>
	/// <typeparam name="TDest">The type of the destination collection, which must implement <see cref="ICollection{TItem}"/> and have a parameterless constructor.</typeparam>
	/// <param name="items">The collection of items to clone. This collection cannot be null.</param>
	/// <returns>A new collection of type <typeparamref name="TDest"/> containing deep clones of the items in the input collection.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> collection is null.</exception>
	public static TDest DeepClone<TItem, TDest>(this IEnumerable<TItem> items)
		where TItem : class, IDeepCloneable<TItem>
		where TDest : ICollection<TItem>, new()
	{
		ArgumentNullException.ThrowIfNull(items);
		var destination = new TDest();
		destination.AddMany(items.Select(i => i.DeepClone()));
		return destination;
	}

	/// <summary>
	/// Creates a deep clone of a collection of items, returning a new collection of the specified type, 
	/// with thread-safety ensured by locking on the provided object.
	/// </summary>
	/// <typeparam name="TItem">The type of the items in the collection, which must implement <see cref="IDeepCloneable{TItem}"/>.</typeparam>
	/// <typeparam name="TDest">The type of the destination collection, which must implement <see cref="ICollection{TItem}"/> and have a parameterless constructor.</typeparam>
	/// <param name="items">The collection of items to clone. This collection cannot be null.</param>
	/// <param name="lockObj">The object to lock on to ensure thread-safety during the cloning operation. This cannot be null.</param>
	/// <returns>A new collection of type <typeparamref name="TDest"/> containing deep clones of the items in the input collection.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if the <paramref name="items"/> collection or the <paramref name="lockObj"/> is null.
	/// </exception>
	public static TDest DeepClone<TItem, TDest>(this IEnumerable<TItem> items, object lockObj)
		where TItem : class, IDeepCloneable<TItem>
		where TDest : ICollection<TItem>, new()
	{
		ArgumentNullException.ThrowIfNull(items);
		ArgumentNullException.ThrowIfNull(lockObj);
		lock (lockObj)
		{
			return DeepClone<TItem, TDest>(items);
		}
	}

	/// <summary>
	/// Creates a shallow clone of a collection of items, returning a new collection of the specified type.
	/// </summary>
	/// <typeparam name="TItem">The type of the items in the collection.</typeparam>
	/// <typeparam name="TDest">The type of the destination collection, which must implement <see cref="ICollection{TItem}"/> and have a parameterless constructor.</typeparam>
	/// <param name="items">The collection of items to clone. This collection cannot be null.</param>
	/// <returns>A new collection of type <typeparamref name="TDest"/> containing the same items as the input collection.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> collection is null.</exception>
	public static TDest ShallowClone<TItem, TDest>(this IEnumerable<TItem> items)
		where TDest : ICollection<TItem>, new()
	{
		ArgumentNullException.ThrowIfNull(items);
		var destination = new TDest();
		destination.AddMany(items);
		return destination;
	}

	/// <summary>
	/// Creates a shallow clone of a collection of items, returning a new collection of the specified type,
	/// with thread-safety ensured by locking on the provided object.
	/// </summary>
	/// <typeparam name="TItem">The type of the items in the collection.</typeparam>
	/// <typeparam name="TDest">The type of the destination collection, which must implement <see cref="ICollection{TItem}"/> and have a parameterless constructor.</typeparam>
	/// <param name="items">The collection of items to clone. This collection cannot be null.</param>
	/// <param name="lockObj">The object to lock on to ensure thread-safety during the cloning operation. This cannot be null.</param>
	/// <returns>A new collection of type <typeparamref name="TDest"/> containing the same items as the input collection.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if the <paramref name="items"/> collection or the <paramref name="lockObj"/> is null.
	/// </exception>
	public static TDest ShallowClone<TItem, TDest>(this IEnumerable<TItem> items, object lockObj)
		where TDest : ICollection<TItem>, new()
	{
		ArgumentNullException.ThrowIfNull(items);
		ArgumentNullException.ThrowIfNull(lockObj);
		lock (lockObj)
		{
			return ShallowClone<TItem, TDest>(items);
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
		ArgumentNullException.ThrowIfNull(items);
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
		ArgumentNullException.ThrowIfNull(items);

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
		ArgumentNullException.ThrowIfNull(items);

		if (nullItemHandling == NullItemHandling.Throw)
		{
			if (items.AnyNull())
			{
				throw new InvalidOperationException("The enumerable contains a null item.");
			}
		}

		return items
			.Select(item => item?.ToString())
			.Where(item => nullItemHandling == NullItemHandling.Include || item is not null);
	}
}

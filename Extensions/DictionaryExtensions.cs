namespace ktsu.io.Extensions;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using ktsu.io.DeepClone;

/// <summary>
/// Extension methods for dictionaries.
/// </summary>
public static class DictionaryExtensions
{
	/// <summary>
	/// Method that gets a value from a dictionary if it exists, otherwise creates a new value and adds it to the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TVal">The type of the values in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to get the value from.</param>
	/// <param name="key">The key to get the value for.</param>
	/// <returns>The value for the key if it exists, otherwise a new value.</returns>
	public static TVal GetOrCreate<TKey, TVal>(this Dictionary<TKey, TVal> dictionary, TKey key) where TKey : notnull where TVal : new() => GetOrCreate(dictionary, key, new TVal());

	/// <summary>
	/// Method that gets a value from a dictionary if it exists, otherwise creates a new value and adds it to the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TVal">The type of the values in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to get the value from.</param>
	/// <param name="key">The key to get the value for.</param>
	/// <param name="defaultValue">The default value to add when an existing value is not found.</param>
	/// <returns>The value for the key if it exists, otherwise a new value.</returns>
	public static TVal GetOrCreate<TKey, TVal>(this Dictionary<TKey, TVal> dictionary, TKey key, TVal defaultValue) where TKey : notnull where TVal : new()
	{
		ArgumentNullException.ThrowIfNull(dictionary);
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(defaultValue);

		if (dictionary.TryGetValue(key, out var val))
		{
			return val;
		}

		dictionary.Add(key, defaultValue);
		return defaultValue;
	}

	/// <summary>
	/// Method that gets a value from a dictionary if it exists, otherwise creates a new value and adds it to the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TVal">The type of the values in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to get the value from.</param>
	/// <param name="key">The key to get the value for.</param>
	/// <param name="defaultValue">The default value to add when an existing value is not found.</param>
	/// <returns>The value for the key if it exists, otherwise a new value.</returns>
	public static TVal GetOrCreate<TKey, TVal>(this ConcurrentDictionary<TKey, TVal> dictionary, TKey key, TVal defaultValue) where TKey : notnull where TVal : new()
	{
		ArgumentNullException.ThrowIfNull(dictionary);
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(defaultValue);

		if (dictionary.TryGetValue(key, out var val))
		{
			return val;
		}

		bool result = dictionary.TryAdd(key, defaultValue);
		Debug.Assert(result);
		return defaultValue;
	}

	/// <summary>
	/// Creates a deep clone of a dictionary, returning a new dictionary with the same keys and deep-cloned values.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary. Keys must be non-nullable.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary, which must implement <see cref="IDeepCloneable{TValue}"/>.</typeparam>
	/// <param name="items">The dictionary to clone. This dictionary cannot be null.</param>
	/// <returns>A new dictionary with the same keys and deep-cloned values as the input dictionary.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> dictionary is null.</exception>
	public static IDictionary<TKey, TValue> DeepClone<TKey, TValue>(this IDictionary<TKey, TValue> items)
		where TKey : notnull
		where TValue : class, IDeepCloneable<TValue>
	{
		ArgumentNullException.ThrowIfNull(items);
		return items.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.DeepClone());
	}

	/// <summary>
	/// Creates a deep clone of a dictionary, returning a new dictionary with the same keys and deep-cloned values, 
	/// with thread-safety ensured by locking on the provided object.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary. Keys must be non-nullable.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary, which must implement <see cref="IDeepCloneable{TValue}"/>.</typeparam>
	/// <param name="items">The dictionary to clone. This dictionary cannot be null.</param>
	/// <param name="lockObj">The object to lock on to ensure thread-safety during the cloning operation. This cannot be null.</param>
	/// <returns>A new dictionary with the same keys and deep-cloned values as the input dictionary.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if the <paramref name="items"/> dictionary or the <paramref name="lockObj"/> is null.
	/// </exception>
	public static IDictionary<TKey, TValue> DeepClone<TKey, TValue>(this IDictionary<TKey, TValue> items, object lockObj)
		where TKey : notnull
		where TValue : class, IDeepCloneable<TValue>
	{
		ArgumentNullException.ThrowIfNull(items);
		ArgumentNullException.ThrowIfNull(lockObj);
		lock (lockObj)
		{
			return DeepClone(items);
		}
	}

	/// <summary>
	/// Creates a shallow clone of a dictionary, returning a new dictionary with the same keys and values.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary. Keys must be non-nullable.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <param name="items">The dictionary to clone. This dictionary cannot be null.</param>
	/// <returns>A new dictionary with the same keys and values as the input dictionary.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> dictionary is null.</exception>
	public static IDictionary<TKey, TValue> ShallowClone<TKey, TValue>(this IDictionary<TKey, TValue> items)
		where TKey : notnull
	{
		ArgumentNullException.ThrowIfNull(items);
		return items.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
	}

	/// <summary>
	/// Creates a shallow clone of a dictionary, returning a new dictionary with the same keys and values,
	/// with thread-safety ensured by locking on the provided object.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary. Keys must be non-nullable.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <param name="items">The dictionary to clone. This dictionary cannot be null.</param>
	/// <param name="lockObj">The object to lock on to ensure thread-safety during the cloning operation. This cannot be null.</param>
	/// <returns>A new dictionary with the same keys and values as the input dictionary.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if the <paramref name="items"/> dictionary or the <paramref name="lockObj"/> is null.
	/// </exception>
	public static IDictionary<TKey, TValue> ShallowClone<TKey, TValue>(this IDictionary<TKey, TValue> items, object lockObj)
		where TKey : notnull
	{
		ArgumentNullException.ThrowIfNull(items);
		ArgumentNullException.ThrowIfNull(lockObj);
		lock (lockObj)
		{
			return ShallowClone(items);
		}
	}

}

namespace ktsu.io.Extensions;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

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
}

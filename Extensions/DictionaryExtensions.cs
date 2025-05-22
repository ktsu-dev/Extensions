// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions;

using System.Collections.Concurrent;
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
	public static TVal GetOrCreate<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key) where TKey : notnull where TVal : notnull, new() => GetOrCreate(dictionary, key, new TVal());

	/// <summary>
	/// Method that gets a value from a dictionary if it exists, otherwise creates a new value and adds it to the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TVal">The type of the values in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to get the value from.</param>
	/// <param name="key">The key to get the value for.</param>
	/// <param name="defaultValue">The default value to add when an existing value is not found.</param>
	/// <returns>The value for the key if it exists, otherwise a new value.</returns>
	public static TVal GetOrCreate<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key, TVal defaultValue) where TKey : notnull where TVal : notnull, new()
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

		var result = dictionary.TryAdd(key, defaultValue);
		Debug.Assert(result);
		return defaultValue;
	}

	/// <summary>
	/// Adds a key-value pair to the dictionary if the key does not already exist, or replaces the existing value if the key does exist.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary. Keys must be non-nullable.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <param name="items">The dictionary to add or replace the key-value pair in. This dictionary cannot be null.</param>
	/// <param name="key">The key to add or replace in the dictionary. This key cannot be null.</param>
	/// <param name="value">The value to associate with the key in the dictionary.</param>
	/// <exception cref="ArgumentNullException">Thrown if the <paramref name="items"/> dictionary or the <paramref name="key"/> is null.</exception>
	public static void AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> items, TKey key, TValue value)
	where TKey : notnull
	{
		ArgumentNullException.ThrowIfNull(items);
		ArgumentNullException.ThrowIfNull(key);
		items[key] = value;
	}
}

// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.Extensions.Tests;

using System.Collections.Concurrent;

[TestClass]
public class DictionaryExtensionsTests
{
	[TestMethod]
	public void GetOrCreateShouldReturnExistingValue()
	{
		Dictionary<string, int> dictionary = new()
		{ { "key1", 42 } };

		int result = dictionary.GetOrCreate("key1");

		Assert.AreEqual(42, result);
	}

	[TestMethod]
	public void GetOrCreateShouldAddAndReturnNewValue()
	{
		Dictionary<string, int> dictionary = [];

		int result = dictionary.GetOrCreate("key1");

		Assert.AreEqual(0, result);
		Assert.HasCount(1, dictionary);
		Assert.AreEqual(0, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreateShouldAddAndReturnDefaultValue()
	{
		Dictionary<string, int> dictionary = [];

		int result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(99, result);
		Assert.HasCount(1, dictionary);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreateConcurrentDictionaryShouldReturnExistingValue()
	{
		ConcurrentDictionary<string, int> dictionary = new();
		dictionary.TryAdd("key1", 42);

		int result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(42, result);
	}

	[TestMethod]
	public void GetOrCreateConcurrentDictionaryShouldAddAndReturnDefaultValue()
	{
		ConcurrentDictionary<string, int> dictionary = new();

		int result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(99, result);
		Assert.HasCount(1, dictionary);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreateConcurrentDictionaryShouldReturnStoredValueWhenAnotherCallerAddsFirst()
	{
		// The comparer adds a rival value for the key the second time it hashes it, which is the
		// moment between a lookup that missed and the add that follows it
		RacingComparer comparer = new();
		ConcurrentDictionary<string, List<int>> dictionary = new(comparer);
		List<int> rival = [];
		comparer.OnSecondHash = () => dictionary.TryAdd("key1", rival);

		List<int> result = dictionary.GetOrCreate("key1", []);

		Assert.AreSame(dictionary["key1"], result);
	}

	[TestMethod]
	public void GetOrCreateConcurrentDictionaryShouldReturnSameInstanceToParallelCallers()
	{
		ConcurrentDictionary<string, ConcurrentBag<int>> dictionary = new();

		Parallel.For(0, 1000, i => dictionary.GetOrCreate("key1", []).Add(i));

		Assert.HasCount(1000, dictionary["key1"]);
	}

	private sealed class RacingComparer : IEqualityComparer<string>
	{
		private int hashCount;

		public Action? OnSecondHash { get; set; }

		public bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.Ordinal);

		public int GetHashCode(string obj)
		{
			if (++hashCount == 2)
			{
				OnSecondHash?.Invoke();
			}

			return StringComparer.Ordinal.GetHashCode(obj);
		}
	}

	[TestMethod]
	public void GetOrCreateShouldThrowArgumentNullExceptionWhenDictionaryIsNull()
	{
		Dictionary<string, int>? dictionary = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => dictionary.GetOrCreate("key1"));
	}

	[TestMethod]
	public void GetOrCreateShouldThrowArgumentNullExceptionWhenKeyIsNull()
	{
		Dictionary<string, int> dictionary = [];

		Assert.ThrowsExactly<ArgumentNullException>(() => dictionary.GetOrCreate(null!));
	}

	[TestMethod]
	public void GetOrCreateShouldThrowArgumentNullExceptionWhenDefaultValueIsNull()
	{
		Dictionary<string, DictionaryExtensionsTests> dictionary = [];

		Assert.ThrowsExactly<ArgumentNullException>(() => dictionary.GetOrCreate("key1", null!));
	}

	[TestMethod]
	public void AddOrReplaceShouldAddNewValue()
	{
		ConcurrentDictionary<string, int> dictionary = new();

		dictionary.AddOrReplace("key1", 42);

		Assert.HasCount(1, dictionary);
		Assert.AreEqual(42, dictionary["key1"]);
	}

	[TestMethod]
	public void AddOrReplaceShouldReplaceExistingValue()
	{
		ConcurrentDictionary<string, int> dictionary = new();
		dictionary.TryAdd("key1", 42);

		dictionary.AddOrReplace("key1", 99);

		Assert.HasCount(1, dictionary);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void AddOrReplaceShouldThrowArgumentNullExceptionWhenDictionaryIsNull()
	{
		ConcurrentDictionary<string, int>? dictionary = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => dictionary.AddOrReplace("key1", 42));
	}

	[TestMethod]
	public void AddOrReplaceShouldThrowArgumentNullExceptionWhenKeyIsNull()
	{
		ConcurrentDictionary<string, int> dictionary = new();

		Assert.ThrowsExactly<ArgumentNullException>(() => dictionary.AddOrReplace(null!, 42));
	}
}

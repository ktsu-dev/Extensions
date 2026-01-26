// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

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

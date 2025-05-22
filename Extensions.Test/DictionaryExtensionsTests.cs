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
		var dictionary = new Dictionary<string, int> { { "key1", 42 } };

		var result = dictionary.GetOrCreate("key1");

		Assert.AreEqual(42, result);
	}

	[TestMethod]
	public void GetOrCreateShouldAddAndReturnNewValue()
	{
		var dictionary = new Dictionary<string, int>();

		var result = dictionary.GetOrCreate("key1");

		Assert.AreEqual(0, result);
		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(0, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreateShouldAddAndReturnDefaultValue()
	{
		var dictionary = new Dictionary<string, int>();

		var result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(99, result);
		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreateConcurrentDictionaryShouldReturnExistingValue()
	{
		var dictionary = new ConcurrentDictionary<string, int>();
		dictionary.TryAdd("key1", 42);

		var result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(42, result);
	}

	[TestMethod]
	public void GetOrCreateConcurrentDictionaryShouldAddAndReturnDefaultValue()
	{
		var dictionary = new ConcurrentDictionary<string, int>();

		var result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(99, result);
		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreateShouldThrowArgumentNullExceptionWhenDictionaryIsNull()
	{
		Dictionary<string, int>? dictionary = null!;

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.GetOrCreate("key1"));
	}

	[TestMethod]
	public void GetOrCreateShouldThrowArgumentNullExceptionWhenKeyIsNull()
	{
		var dictionary = new Dictionary<string, int>();

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.GetOrCreate(null!));
	}

	[TestMethod]
	public void GetOrCreateShouldThrowArgumentNullExceptionWhenDefaultValueIsNull()
	{
		var dictionary = new Dictionary<string, DictionaryExtensionsTests>();

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.GetOrCreate("key1", null!));
	}

	[TestMethod]
	public void AddOrReplaceShouldAddNewValue()
	{
		var dictionary = new ConcurrentDictionary<string, int>();

		dictionary.AddOrReplace("key1", 42);

		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(42, dictionary["key1"]);
	}

	[TestMethod]
	public void AddOrReplaceShouldReplaceExistingValue()
	{
		var dictionary = new ConcurrentDictionary<string, int>();
		dictionary.TryAdd("key1", 42);

		dictionary.AddOrReplace("key1", 99);

		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void AddOrReplaceShouldThrowArgumentNullExceptionWhenDictionaryIsNull()
	{
		ConcurrentDictionary<string, int>? dictionary = null!;

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.AddOrReplace("key1", 42));
	}

	[TestMethod]
	public void AddOrReplaceShouldThrowArgumentNullExceptionWhenKeyIsNull()
	{
		var dictionary = new ConcurrentDictionary<string, int>();

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.AddOrReplace(null!, 42));
	}
}

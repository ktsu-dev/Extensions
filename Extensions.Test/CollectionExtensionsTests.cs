// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions.Tests;

[TestClass]
public class CollectionExtensionsTests
{
	[TestMethod]
	public void AddFromAddsItemsToCollection()
	{
		List<int> collection = [1, 2];
		List<int> itemsToAdd = [3, 4];

		collection.AddFrom(itemsToAdd);

		CollectionAssert.AreEqual(new List<int> { 1, 2, 3, 4 }, collection);
	}

	[TestMethod]
	public void AddFromNullCollectionThrowsArgumentNullException()
	{
		ICollection<int> collection = null!;
		List<int> itemsToAdd = [1, 2];

		Assert.ThrowsExactly<ArgumentNullException>(() => collection.AddFrom(itemsToAdd));
	}

	[TestMethod]
	public void AddFromNullItemsThrowsArgumentNullException()
	{
		List<int> collection = [1, 2];
		IEnumerable<int> itemsToAdd = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => collection.AddFrom(itemsToAdd));
	}

	[TestMethod]
	public void AnyNullWithNullItemsReturnsTrue()
	{
		List<string?> collection = ["a", null, "b"];

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void AnyNullWithoutNullItemsReturnsFalse()
	{
		List<string?> collection = ["a", "b", "c"];

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullNullCollectionThrowsArgumentNullException()
	{
		ICollection<string?> collection = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => collection.AnyNull());
	}

	[TestMethod]
	public void WriteItemsToConsoleWritesCorrectOutput()
	{
		List<object?> collection = ["a", null, 1];
		using StringWriter sw = new();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = $"a{Environment.NewLine}1{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void AddFromAddEmptyEnumerableDoesNotChangeCollection()
	{
		List<int> collection = [1, 2];
		IEnumerable<int> itemsToAdd = [];

		collection.AddFrom(itemsToAdd);

		CollectionAssert.AreEqual(new List<int> { 1, 2 }, collection);
	}

	[TestMethod]
	public void AddFromCollectionWithNullItemsAddsItems()
	{
		List<string?> collection = ["a", null];
		List<string?> itemsToAdd = ["b", null];

		collection.AddFrom(itemsToAdd);

		CollectionAssert.AreEqual(new List<string?> { "a", null, "b", null }, collection);
	}

	[TestMethod]
	public void AnyNullEmptyCollectionReturnsFalse()
	{
		List<string?> collection = [];

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullAllNullItemsReturnsTrue()
	{
		List<string?> collection = [null, null];

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void WriteItemsToConsoleEmptyCollectionNoOutput()
	{
		List<object?> collection = [];

		using StringWriter sw = new();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = string.Empty;
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void WriteItemsToConsoleWithNullItemsSkipsNulls()
	{
		List<object?> collection = ["test", null, "example"];

		using StringWriter sw = new();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = $"test{Environment.NewLine}example{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void AddFromWithLargeNumberOfItemsAddsAllItems()
	{
		List<int> collection = [];
		IEnumerable<int> itemsToAdd = Enumerable.Range(1, 1000);

		collection.AddFrom(itemsToAdd);

		Assert.AreEqual(1000, collection.Count);
		CollectionAssert.AreEqual(itemsToAdd.ToList(), collection);
	}

	[TestMethod]
	public void AddFromEnumerableContainsNullsAddsNullsToCollection()
	{
		List<string?> collection = ["start"];
		List<string?> itemsToAdd = [null, "middle", null, "end"];

		collection.AddFrom(itemsToAdd);

		CollectionAssert.AreEqual(new List<string?> { "start", null, "middle", null, "end" }, collection);
	}

	[TestMethod]
	public void AnyNullValueTypeCollectionReturnsFalse()
	{
		List<int> collection = [1, 2, 3];

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullNullableValueTypeWithNullReturnsTrue()
	{
		List<int?> collection = [1, null, 3];

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void ReplaceWithReplacesAllItemsInCollection()
	{
		List<int> collection = [1, 2, 3];
		List<int> newItems = [4, 5];

		collection.ReplaceWith(newItems);

		CollectionAssert.AreEqual(new List<int> { 4, 5 }, collection);
	}

	[TestMethod]
	public void ReplaceWithEmptyEnumerableClearsCollection()
	{
		List<int> collection = [1, 2, 3];
		IEnumerable<int> newItems = [];

		collection.ReplaceWith(newItems);

		Assert.AreEqual(0, collection.Count);
		CollectionAssert.AreEqual(new List<int>(), collection);
	}

	[TestMethod]
	public void ReplaceWithNullCollectionThrowsArgumentNullException()
	{
		ICollection<int> collection = null!;
		List<int> newItems = [1, 2];

		Assert.ThrowsExactly<ArgumentNullException>(() => collection.ReplaceWith(newItems));
	}

	[TestMethod]
	public void ReplaceWithNullItemsThrowsArgumentNullException()
	{
		List<int> collection = [1, 2];
		IEnumerable<int> newItems = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => collection.ReplaceWith(newItems));
	}

	[TestMethod]
	public void ReplaceWithNewItemsContainingNullsReplacesCorrectly()
	{
		List<string?> collection = ["a", "b", "c"];
		List<string?> newItems = ["x", null, "z"];

		collection.ReplaceWith(newItems);

		CollectionAssert.AreEqual(new List<string?> { "x", null, "z" }, collection);
	}
}

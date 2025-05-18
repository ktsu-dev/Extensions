// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions.Tests;

[TestClass]
public class CollectionExtensionsTests
{
	[TestMethod]
	public void AddManyAddsItemsToCollection()
	{
		var collection = new List<int> { 1, 2 };
		var itemsToAdd = new List<int> { 3, 4 };

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<int> { 1, 2, 3, 4 }, collection);
	}

	[TestMethod]
	public void AddManyNullCollectionThrowsArgumentNullException()
	{
		ICollection<int> collection = null!;
		var itemsToAdd = new List<int> { 1, 2 };

		Assert.ThrowsException<ArgumentNullException>(() => collection.AddMany(itemsToAdd));
	}

	[TestMethod]
	public void AddManyNullItemsThrowsArgumentNullException()
	{
		var collection = new List<int> { 1, 2 };
		IEnumerable<int> itemsToAdd = null!;

		Assert.ThrowsException<ArgumentNullException>(() => collection.AddMany(itemsToAdd));
	}

	[TestMethod]
	public void AnyNullWithNullItemsReturnsTrue()
	{
		var collection = new List<string?> { "a", null, "b" };

		var result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void AnyNullWithoutNullItemsReturnsFalse()
	{
		var collection = new List<string?> { "a", "b", "c" };

		var result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullNullCollectionThrowsArgumentNullException()
	{
		ICollection<string?> collection = null!;

		Assert.ThrowsException<ArgumentNullException>(() => collection.AnyNull());
	}

	[TestMethod]
	public void WriteItemsToConsoleWritesCorrectOutput()
	{
		var collection = new List<object?> { "a", null, 1 };
		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		var expected = $"a{Environment.NewLine}1{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void AddManyAddEmptyEnumerableDoesNotChangeCollection()
	{
		var collection = new List<int> { 1, 2 };
		var itemsToAdd = Enumerable.Empty<int>();

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<int> { 1, 2 }, collection);
	}

	[TestMethod]
	public void AddManyCollectionWithNullItemsAddsItems()
	{
		var collection = new List<string?> { "a", null };
		var itemsToAdd = new List<string?> { "b", null };

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<string?> { "a", null, "b", null }, collection);
	}

	[TestMethod]
	public void AnyNullEmptyCollectionReturnsFalse()
	{
		var collection = new List<string?>();

		var result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullAllNullItemsReturnsTrue()
	{
		var collection = new List<string?> { null, null };

		var result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void WriteItemsToConsoleEmptyCollectionNoOutput()
	{
		var collection = new List<object?>();

		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		var expected = string.Empty;
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void WriteItemsToConsoleWithNullItemsSkipsNulls()
	{
		var collection = new List<object?> { "test", null, "example" };

		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		var expected = $"test{Environment.NewLine}example{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void AddManyWithLargeNumberOfItemsAddsAllItems()
	{
		var collection = new List<int>();
		var itemsToAdd = Enumerable.Range(1, 1000);

		collection.AddMany(itemsToAdd);

		Assert.AreEqual(1000, collection.Count);
		CollectionAssert.AreEqual(itemsToAdd.ToList(), collection);
	}

	[TestMethod]
	public void AddManyEnumerableContainsNullsAddsNullsToCollection()
	{
		var collection = new List<string?> { "start" };
		var itemsToAdd = new List<string?> { null, "middle", null, "end" };

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<string?> { "start", null, "middle", null, "end" }, collection);
	}

	[TestMethod]
	public void AnyNullValueTypeCollectionReturnsFalse()
	{
		var collection = new List<int> { 1, 2, 3 };

		var result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullNullableValueTypeWithNullReturnsTrue()
	{
		var collection = new List<int?> { 1, null, 3 };

		var result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void ReplaceWithReplacesAllItemsInCollection()
	{
		var collection = new List<int> { 1, 2, 3 };
		var newItems = new List<int> { 4, 5 };

		collection.ReplaceWith(newItems);

		CollectionAssert.AreEqual(new List<int> { 4, 5 }, collection);
	}

	[TestMethod]
	public void ReplaceWithEmptyEnumerableClearsCollection()
	{
		var collection = new List<int> { 1, 2, 3 };
		var newItems = Enumerable.Empty<int>();

		collection.ReplaceWith(newItems);

		Assert.AreEqual(0, collection.Count);
		CollectionAssert.AreEqual(new List<int>(), collection);
	}

	[TestMethod]
	public void ReplaceWithNullCollectionThrowsArgumentNullException()
	{
		ICollection<int> collection = null!;
		var newItems = new List<int> { 1, 2 };

		Assert.ThrowsException<ArgumentNullException>(() => collection.ReplaceWith(newItems));
	}

	[TestMethod]
	public void ReplaceWithNullItemsThrowsArgumentNullException()
	{
		var collection = new List<int> { 1, 2 };
		IEnumerable<int> newItems = null!;

		Assert.ThrowsException<ArgumentNullException>(() => collection.ReplaceWith(newItems));
	}

	[TestMethod]
	public void ReplaceWithNewItemsContainingNullsReplacesCorrectly()
	{
		var collection = new List<string?> { "a", "b", "c" };
		var newItems = new List<string?> { "x", null, "z" };

		collection.ReplaceWith(newItems);

		CollectionAssert.AreEqual(new List<string?> { "x", null, "z" }, collection);
	}
}

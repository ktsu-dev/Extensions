namespace ktsu.Extensions.Tests;

[TestClass]
public class CollectionExtensionsTests
{
	[TestMethod]
	public void AddMany_AddsItemsToCollection()
	{
		var collection = new List<int> { 1, 2 };
		var itemsToAdd = new List<int> { 3, 4 };

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<int> { 1, 2, 3, 4 }, collection);
	}

	[TestMethod]
	public void AddMany_NullCollection_ThrowsArgumentNullException()
	{
		ICollection<int> collection = null!;
		var itemsToAdd = new List<int> { 1, 2 };

		Assert.ThrowsException<ArgumentNullException>(() => collection.AddMany(itemsToAdd));
	}

	[TestMethod]
	public void AddMany_NullItems_ThrowsArgumentNullException()
	{
		var collection = new List<int> { 1, 2 };
		IEnumerable<int> itemsToAdd = null!;

		Assert.ThrowsException<ArgumentNullException>(() => collection.AddMany(itemsToAdd));
	}

	[TestMethod]
	public void AnyNull_WithNullItems_ReturnsTrue()
	{
		var collection = new List<string?> { "a", null, "b" };

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void AnyNull_WithoutNullItems_ReturnsFalse()
	{
		var collection = new List<string?> { "a", "b", "c" };

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNull_NullCollection_ThrowsArgumentNullException()
	{
		ICollection<string?> collection = null!;

		Assert.ThrowsException<ArgumentNullException>(() => collection.AnyNull());
	}

	[TestMethod]
	public void WriteItemsToConsole_WritesCorrectOutput()
	{
		var collection = new List<object?> { "a", null, 1 };
		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = $"a{Environment.NewLine}1{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void AddMany_AddEmptyEnumerable_DoesNotChangeCollection()
	{
		var collection = new List<int> { 1, 2 };
		var itemsToAdd = Enumerable.Empty<int>();

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<int> { 1, 2 }, collection);
	}

	[TestMethod]
	public void AddMany_CollectionWithNullItems_AddsItems()
	{
		var collection = new List<string?> { "a", null };
		var itemsToAdd = new List<string?> { "b", null };

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<string?> { "a", null, "b", null }, collection);
	}

	[TestMethod]
	public void AnyNull_EmptyCollection_ReturnsFalse()
	{
		var collection = new List<string?>();

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNull_AllNullItems_ReturnsTrue()
	{
		var collection = new List<string?> { null, null };

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void WriteItemsToConsole_EmptyCollection_NoOutput()
	{
		var collection = new List<object?>();

		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = string.Empty;
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void WriteItemsToConsole_WithNullItems_SkipsNulls()
	{
		var collection = new List<object?> { "test", null, "example" };

		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = $"test{Environment.NewLine}example{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void AddMany_WithLargeNumberOfItems_AddsAllItems()
	{
		var collection = new List<int>();
		var itemsToAdd = Enumerable.Range(1, 1000);

		collection.AddMany(itemsToAdd);

		Assert.AreEqual(1000, collection.Count);
		CollectionAssert.AreEqual(itemsToAdd.ToList(), collection);
	}

	[TestMethod]
	public void AddMany_EnumerableContainsNulls_AddsNullsToCollection()
	{
		var collection = new List<string?> { "start" };
		var itemsToAdd = new List<string?> { null, "middle", null, "end" };

		collection.AddMany(itemsToAdd);

		CollectionAssert.AreEqual(new List<string?> { "start", null, "middle", null, "end" }, collection);
	}

	[TestMethod]
	public void AnyNull_ValueTypeCollection_ReturnsFalse()
	{
		var collection = new List<int> { 1, 2, 3 };

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNull_NullableValueTypeWithNull_ReturnsTrue()
	{
		var collection = new List<int?> { 1, null, 3 };

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}
}

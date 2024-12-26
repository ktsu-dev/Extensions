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

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void AnyNullWithoutNullItemsReturnsFalse()
	{
		var collection = new List<string?> { "a", "b", "c" };

		bool result = collection.AnyNull();

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

		string expected = $"a{Environment.NewLine}1{Environment.NewLine}";
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

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullAllNullItemsReturnsTrue()
	{
		var collection = new List<string?> { null, null };

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void WriteItemsToConsoleEmptyCollectionNoOutput()
	{
		var collection = new List<object?>();

		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = string.Empty;
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void WriteItemsToConsoleWithNullItemsSkipsNulls()
	{
		var collection = new List<object?> { "test", null, "example" };

		using var sw = new StringWriter();
		Console.SetOut(sw);

		collection.WriteItemsToConsole();

		string expected = $"test{Environment.NewLine}example{Environment.NewLine}";
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

		bool result = collection.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullNullableValueTypeWithNullReturnsTrue()
	{
		var collection = new List<int?> { 1, null, 3 };

		bool result = collection.AnyNull();

		Assert.IsTrue(result);
	}
}

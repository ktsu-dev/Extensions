namespace ktsu.io.Extensions.Tests;

[TestClass]
public class CollectionExtensionsTests
{
	[TestMethod]
	public void AddMany_AddsItems()
	{
		var collection = new List<int>();
		int[] items = [1, 2, 3];
		collection.AddMany(items);

		Assert.AreEqual(3, collection.Count);
		Assert.AreEqual(1, collection[0]);
		Assert.AreEqual(2, collection[1]);
		Assert.AreEqual(3, collection[2]);
	}
}

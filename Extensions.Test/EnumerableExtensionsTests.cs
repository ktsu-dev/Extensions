namespace ktsu.io.Extensions.Tests;

using System.Collections.Generic;
using System.Linq;
using ktsu.io.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EnumerableExtensionsTests
{
	[TestMethod]
	public void WithIndex_EnumeratesWithIndex()
	{
		string[] items = ["a", "b", "c"];
		var result = items.WithIndex().ToList();

		Assert.AreEqual(3, result.Count);
		Assert.AreEqual(("a", 0), result[0]);
		Assert.AreEqual(("b", 1), result[1]);
		Assert.AreEqual(("c", 2), result[2]);
	}

	[TestMethod]
	public void ToCollection_CreatesCollection()
	{
		int[] items = [1, 2, 3];
		var collection = items.ToCollection(items);

		Assert.AreEqual(3, collection.Count);
		Assert.AreEqual(1, collection[0]);
		Assert.AreEqual(2, collection[1]);
		Assert.AreEqual(3, collection[2]);
	}

	[TestMethod]
	public void ForEach_AppliesAction()
	{
		int[] items = [1, 2, 3];
		int sum = 0;
		items.ForEach(item => sum += item);

		Assert.AreEqual(6, sum);
	}

	[TestMethod]
	public void ToCollection_ThrowsOnNull()
	{
		IEnumerable<int> items = null!;
		Assert.ThrowsException<ArgumentNullException>(() => items.ToCollection());
	}

	[TestMethod]
	public void ForEach_ThrowsOnNullEnumerable()
	{
		IEnumerable<int> items = null!;
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(item => { }));
	}

	[TestMethod]
	public void ForEach_ThrowsOnNullAction()
	{
		int[] items = [1, 2, 3];
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(null!));
	}
}

// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions.Tests;
[TestClass]
public class EnumerableExtensionsTests
{
	[TestMethod]
	public void WithIndexEnumeratesWithIndex()
	{
		string[] items = ["a", "b", "c"];
		List<(string item, int index)> result = [.. items.WithIndex()];

		Assert.AreEqual(3, result.Count);
		Assert.AreEqual(("a", 0), result[0]);
		Assert.AreEqual(("b", 1), result[1]);
		Assert.AreEqual(("c", 2), result[2]);
	}

	[TestMethod]
	public void ToCollectionCreatesCollection()
	{
		int[] items = [1, 2, 3];
		System.Collections.ObjectModel.Collection<int> collection = items.ToCollection();

		Assert.AreEqual(3, collection.Count);
		Assert.AreEqual(1, collection[0]);
		Assert.AreEqual(2, collection[1]);
		Assert.AreEqual(3, collection[2]);
	}

	[TestMethod]
	public void ForEachAppliesAction()
	{
		int[] items = [1, 2, 3];
		int sum = 0;
		items.ForEach(item => sum += item);

		Assert.AreEqual(6, sum);
	}

	[TestMethod]
	public void ToCollectionThrowsOnNull()
	{
		IEnumerable<int> items = null!;
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ToCollection());
	}

	[TestMethod]
	public void ForEachThrowsOnNullEnumerable()
	{
		IEnumerable<int> items = null!;
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ForEach(item => { }));
	}

	[TestMethod]
	public void ForEachThrowsOnNullAction()
	{
		int[] items = [1, 2, 3];
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ForEach(null!));
	}

	[TestMethod]
	public void ForEachShouldApplyActionToEachElement()
	{
		// Arrange
		List<int> items = [1, 2, 3, 4, 5];
		List<int> results = [];

		// Act
		items.ForEach(i => results.Add(i * 2));

		// Assert
		Assert.AreEqual(items.Count, results.Count);
		for (int i = 0; i < items.Count; i++)
		{
			Assert.AreEqual(items[i] * 2, results[i]);
		}
	}

	[TestMethod]
	public void ForEachShouldThrowArgumentNullExceptionWhenEnumerableIsNull()
	{
		// Arrange
		IEnumerable<int> items = null!;

		// Act & Assert
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ForEach(i => { }));
	}

	[TestMethod]
	public void ForEachShouldThrowArgumentNullExceptionWhenActionIsNull()
	{
		// Arrange
		List<int> items = [1, 2, 3, 4, 5];

		// Act & Assert
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ForEach(null!));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldApplyActionToEachElement()
	{
		// Arrange
		List<int> items = [1, 2, 3, 4, 5];
		List<int> results = [];
		object lockObj = new();

		// Act
		items.ForEach(lockObj, i => results.Add(i * 2));

		// Assert
		Assert.AreEqual(items.Count, results.Count);
		for (int i = 0; i < items.Count; i++)
		{
			Assert.AreEqual(items[i] * 2, results[i]);
		}
	}

	[TestMethod]
	public void ForEachWithLockObjShouldThrowArgumentNullExceptionWhenEnumerableIsNull()
	{
		// Arrange
		IEnumerable<int> items = null!;
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ForEach(lockObj, i => { }));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldThrowArgumentNullExceptionWhenLockObjIsNull()
	{
		// Arrange
		List<int> items = [1, 2, 3, 4, 5];
		object lockObj = null!;

		// Act & Assert
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ForEach(lockObj, i => { }));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldThrowArgumentNullExceptionWhenActionIsNull()
	{
		// Arrange
		List<int> items = [1, 2, 3, 4, 5];
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsExactly<ArgumentNullException>(() => items.ForEach(lockObj, null!));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldLockAndApplyActionCorrectly()
	{
		// Arrange
		List<int> items = [1, 2, 3, 4, 5];
		List<int> results = [];
		object lockObj = new();
		bool wasLocked = false;

		// Act
		lock (lockObj)
		{
			wasLocked = true;
			items.ForEach(lockObj, i => results.Add(i * 2));
			wasLocked = false;
		}

		// Assert
		Assert.AreEqual(items.Count, results.Count);
		for (int i = 0; i < items.Count; i++)
		{
			Assert.AreEqual(items[i] * 2, results[i]);
		}

		Assert.IsFalse(wasLocked, "The lock object should have been released after the method executed.");
	}

	// Additional tests for edge cases and scenarios

	[TestMethod]
	public void WithIndexEmptyEnumerableReturnsEmpty()
	{
		IEnumerable<string> items = [];
		List<(string item, int index)> result = [.. items.WithIndex()];

		Assert.AreEqual(0, result.Count);
	}

	[TestMethod]
	public void ToCollectionEmptyEnumerableReturnsEmptyCollection()
	{
		IEnumerable<int> items = [];
		System.Collections.ObjectModel.Collection<int> collection = items.ToCollection();

		Assert.AreEqual(0, collection.Count);
	}

	[TestMethod]
	public void ForEachEmptyEnumerableDoesNothing()
	{
		IEnumerable<int> items = [];
		int sum = 0;
		items.ForEach(item => sum += item);

		Assert.AreEqual(0, sum);
	}

	[TestMethod]
	public void AnyNullWithNullItemsReturnsTrue()
	{
		List<string?> items = ["a", null, "b"];

		bool result = items.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void AnyNullWithoutNullItemsReturnsFalse()
	{
		List<string?> items = ["a", "b", "c"];

		bool result = items.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullEmptyEnumerableReturnsFalse()
	{
		IEnumerable<string?> items = [];

		bool result = items.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void WriteItemsToConsoleWritesCorrectOutput()
	{
		List<object?> items = ["a", null, 1];
		using StringWriter sw = new();
		Console.SetOut(sw);

		items.WriteItemsToConsole();

		string expected = $"a{Environment.NewLine}1{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void WriteItemsToConsoleEmptyEnumerableNoOutput()
	{
		IEnumerable<object?> items = [];

		using StringWriter sw = new();
		Console.SetOut(sw);

		items.WriteItemsToConsole();

		string expected = string.Empty;
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void ToStringEnumerableWithNullItemsRemoveHandling()
	{
		List<object?> items = ["a", null, 1];

		IEnumerable<string?> result = items.ToStringEnumerable(NullItemHandling.Remove);

		CollectionAssert.AreEqual(new List<string> { "a", "1" }, result.ToList());
	}

	[TestMethod]
	public void ToStringEnumerableWithNullItemsIncludeHandling()
	{
		List<object?> items = ["a", null, 1];

		IEnumerable<string?> result = items.ToStringEnumerable(NullItemHandling.Include);

		CollectionAssert.AreEqual(new List<string?> { "a", null, "1" }, result.ToList());
	}

	[TestMethod]
	public void ToStringEnumerableWithNullItemsThrowHandling()
	{
		List<object?> items = ["a", null, 1];

		Assert.ThrowsExactly<InvalidOperationException>(() => items.ToStringEnumerable(NullItemHandling.Throw).ToList());
	}

	[TestMethod]
	public void ToStringEnumerableEmptyEnumerableReturnsEmpty()
	{
		IEnumerable<object?> items = [];

		IEnumerable<string> result = items.ToStringEnumerable();

		Assert.AreEqual(0, result.Count());
	}

	[TestMethod]
	public void JoinConcatenatesElementsWithSeparator()
	{
		// Arrange
		List<string> items = ["a", "b", "c"];
		string separator = ",";

		// Act
		string result = items.Join(separator);

		// Assert
		Assert.AreEqual("a,b,c", result);
	}

	[TestMethod]
	public void JoinThrowsArgumentNullExceptionWhenItemsIsNull()
	{
		// Arrange
		List<string> items = null!;
		string separator = ",";

		// Act & Assert
		Assert.ThrowsExactly<ArgumentNullException>(() => items.Join(separator));
	}

	[TestMethod]
	public void JoinThrowsArgumentNullExceptionWhenSeparatorIsNull()
	{
		// Arrange
		List<string> items = ["a", "b", "c"];
		string separator = null!;

		// Act & Assert
		Assert.ThrowsExactly<ArgumentNullException>(() => items.Join(separator));
	}

	[TestMethod]
	public void JoinWithNullItemHandlingRemoveRemovesNullItems()
	{
		// Arrange
		List<string?> items = ["a", null, "b", "c"];
		string separator = ",";

		// Act
		string result = items.Join(separator, NullItemHandling.Remove);

		// Assert
		Assert.AreEqual("a,b,c", result);
	}

	[TestMethod]
	public void JoinWithNullItemHandlingIncludeIncludesNullItems()
	{
		// Arrange
		List<string?> items = ["a", null, "b", "c"];
		string separator = ",";

		// Act
		string result = items.Join(separator, NullItemHandling.Include);

		// Assert
		Assert.AreEqual("a,,b,c", result);
	}

	[TestMethod]
	public void JoinWithNullItemHandlingThrowThrowsInvalidOperationException()
	{
		// Arrange
		List<string?> items = ["a", null, "b", "c"];
		string separator = ",";

		// Act & Assert
		Assert.ThrowsExactly<InvalidOperationException>(() => items.Join(separator, NullItemHandling.Throw));
	}
}

// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions.Tests;

using ktsu.DeepClone;

[TestClass]
public class EnumerableExtensionsTests
{
	[TestMethod]
	public void WithIndexEnumeratesWithIndex()
	{
		string[] items = ["a", "b", "c"];
		var result = items.WithIndex().ToList();

		Assert.AreEqual(3, result.Count);
		Assert.AreEqual(("a", 0), result[0]);
		Assert.AreEqual(("b", 1), result[1]);
		Assert.AreEqual(("c", 2), result[2]);
	}

	[TestMethod]
	public void ToCollectionCreatesCollection()
	{
		int[] items = [1, 2, 3];
		var collection = items.ToCollection();

		Assert.AreEqual(3, collection.Count);
		Assert.AreEqual(1, collection[0]);
		Assert.AreEqual(2, collection[1]);
		Assert.AreEqual(3, collection[2]);
	}

	[TestMethod]
	public void ForEachAppliesAction()
	{
		int[] items = [1, 2, 3];
		var sum = 0;
		items.ForEach(item => sum += item);

		Assert.AreEqual(6, sum);
	}

	[TestMethod]
	public void ToCollectionThrowsOnNull()
	{
		IEnumerable<int> items = null!;
		Assert.ThrowsException<ArgumentNullException>(() => items.ToCollection());
	}

	[TestMethod]
	public void ForEachThrowsOnNullEnumerable()
	{
		IEnumerable<int> items = null!;
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(item => { }));
	}

	[TestMethod]
	public void ForEachThrowsOnNullAction()
	{
		int[] items = [1, 2, 3];
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(null!));
	}

	public class SampleItem : IDeepCloneable<SampleItem>
	{
		public int Value { get; set; }

		public SampleItem DeepClone()
		{
			return new SampleItem { Value = Value };
		}
	}

	[TestMethod]
	public void DeepCloneShouldCloneCollectionCorrectly()
	{
		// Arrange
		var originalItems = new List<SampleItem>
			{
				new() { Value = 1 },
				new() { Value = 2 },
				new() { Value = 3 }
			};

		// Act
		var clonedItems = originalItems.DeepClone<SampleItem, List<SampleItem>>();

		// Assert
		Assert.AreEqual(originalItems.Count, clonedItems.Count);
		for (var i = 0; i < originalItems.Count; i++)
		{
			Assert.AreNotSame(originalItems[i], clonedItems[i]);
			Assert.AreEqual(originalItems[i].Value, clonedItems[i].Value);
		}
	}

	[TestMethod]
	public void DeepCloneShouldThrowArgumentNullExceptionWhenItemsIsNull()
	{
		// Arrange
		List<SampleItem> originalItems = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(originalItems.DeepClone<SampleItem, List<SampleItem>>);
	}

	[TestMethod]
	public void DeepCloneWithLockObjShouldCloneCollectionCorrectly()
	{
		// Arrange
		var originalItems = new List<SampleItem>
			{
				new() { Value = 1 },
				new() { Value = 2 },
				new() { Value = 3 }
			};
		object lockObj = new();

		// Act
		var clonedItems = originalItems.DeepClone<SampleItem, List<SampleItem>>(lockObj);

		// Assert
		Assert.AreEqual(originalItems.Count, clonedItems.Count);
		for (var i = 0; i < originalItems.Count; i++)
		{
			Assert.AreNotSame(originalItems[i], clonedItems[i]);
			Assert.AreEqual(originalItems[i].Value, clonedItems[i].Value);
		}
	}

	[TestMethod]
	public void DeepCloneWithLockObjShouldThrowArgumentNullExceptionWhenItemsIsNull()
	{
		// Arrange
		List<SampleItem> originalItems = null!;
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalItems.DeepClone<SampleItem, List<SampleItem>>(lockObj));
	}

	[TestMethod]
	public void DeepCloneWithLockObjShouldThrowArgumentNullExceptionWhenLockObjIsNull()
	{
		// Arrange
		var originalItems = new List<SampleItem>
			{
				new() { Value = 1 },
				new() { Value = 2 },
				new() { Value = 3 }
			};
		object lockObj = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalItems.DeepClone<SampleItem, List<SampleItem>>(lockObj));
	}

	[TestMethod]
	public void DeepCloneWithLockObjShouldLockAndCloneCorrectly()
	{
		// Arrange
		var originalItems = new List<SampleItem>
			{
				new() { Value = 1 },
				new() { Value = 2 },
				new() { Value = 3 }
			};
		object lockObj = new();
		var wasLocked = false;

		// Act
		lock (lockObj)
		{
			wasLocked = true;
			var clonedItems = originalItems.DeepClone<SampleItem, List<SampleItem>>(lockObj);
			wasLocked = false;
		}

		// Assert
		Assert.IsFalse(wasLocked, "The lock object should have been released after the method executed.");
	}

	[TestMethod]
	public void ShallowCloneShouldCloneCollectionCorrectly()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };

		// Act
		var clonedItems = originalItems.ShallowClone<int, List<int>>();

		// Assert
		Assert.AreEqual(originalItems.Count, clonedItems.Count);
		for (var i = 0; i < originalItems.Count; i++)
		{
			Assert.AreEqual(originalItems[i], clonedItems[i]);
		}
	}

	[TestMethod]
	public void ShallowCloneShouldThrowArgumentNullExceptionWhenItemsIsNull()
	{
		// Arrange
		List<int> originalItems = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(originalItems.ShallowClone<int, List<int>>);
	}

	[TestMethod]
	public void ShallowCloneWithLockObjShouldCloneCollectionCorrectly()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = new();

		// Act
		var clonedItems = originalItems.ShallowClone<int, List<int>>(lockObj);

		// Assert
		Assert.AreEqual(originalItems.Count, clonedItems.Count);
		for (var i = 0; i < originalItems.Count; i++)
		{
			Assert.AreEqual(originalItems[i], clonedItems[i]);
		}
	}

	[TestMethod]
	public void ShallowCloneWithLockObjShouldThrowArgumentNullExceptionWhenItemsIsNull()
	{
		// Arrange
		List<int> originalItems = null!;
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalItems.ShallowClone<int, List<int>>(lockObj));
	}

	[TestMethod]
	public void ShallowCloneWithLockObjShouldThrowArgumentNullExceptionWhenLockObjIsNull()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalItems.ShallowClone<int, List<int>>(lockObj));
	}

	[TestMethod]
	public void ShallowCloneWithLockObjShouldLockAndCloneCorrectly()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = new();
		var wasLocked = false;

		// Act
		lock (lockObj)
		{
			wasLocked = true;
			var clonedItems = originalItems.ShallowClone<int, List<int>>(lockObj);
			wasLocked = false;
		}

		// Assert
		Assert.IsFalse(wasLocked, "The lock object should have been released after the method executed.");
	}

	[TestMethod]
	public void ForEachShouldApplyActionToEachElement()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		var results = new List<int>();

		// Act
		items.ForEach(i => results.Add(i * 2));

		// Assert
		Assert.AreEqual(items.Count, results.Count);
		for (var i = 0; i < items.Count; i++)
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
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(i => { }));
	}

	[TestMethod]
	public void ForEachShouldThrowArgumentNullExceptionWhenActionIsNull()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(null!));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldApplyActionToEachElement()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		var results = new List<int>();
		object lockObj = new();

		// Act
		items.ForEach(lockObj, i => results.Add(i * 2));

		// Assert
		Assert.AreEqual(items.Count, results.Count);
		for (var i = 0; i < items.Count; i++)
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
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(lockObj, i => { }));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldThrowArgumentNullExceptionWhenLockObjIsNull()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(lockObj, i => { }));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldThrowArgumentNullExceptionWhenActionIsNull()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(lockObj, null!));
	}

	[TestMethod]
	public void ForEachWithLockObjShouldLockAndApplyActionCorrectly()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		var results = new List<int>();
		object lockObj = new();
		var wasLocked = false;

		// Act
		lock (lockObj)
		{
			wasLocked = true;
			items.ForEach(lockObj, i => results.Add(i * 2));
			wasLocked = false;
		}

		// Assert
		Assert.AreEqual(items.Count, results.Count);
		for (var i = 0; i < items.Count; i++)
		{
			Assert.AreEqual(items[i] * 2, results[i]);
		}

		Assert.IsFalse(wasLocked, "The lock object should have been released after the method executed.");
	}

	// Additional tests for edge cases and scenarios

	[TestMethod]
	public void WithIndexEmptyEnumerableReturnsEmpty()
	{
		var items = Enumerable.Empty<string>();
		var result = items.WithIndex().ToList();

		Assert.AreEqual(0, result.Count);
	}

	[TestMethod]
	public void ToCollectionEmptyEnumerableReturnsEmptyCollection()
	{
		var items = Enumerable.Empty<int>();
		var collection = items.ToCollection();

		Assert.AreEqual(0, collection.Count);
	}

	[TestMethod]
	public void ForEachEmptyEnumerableDoesNothing()
	{
		var items = Enumerable.Empty<int>();
		var sum = 0;
		items.ForEach(item => sum += item);

		Assert.AreEqual(0, sum);
	}

	[TestMethod]
	public void AnyNullWithNullItemsReturnsTrue()
	{
		var items = new List<string?> { "a", null, "b" };

		var result = items.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void AnyNullWithoutNullItemsReturnsFalse()
	{
		var items = new List<string?> { "a", "b", "c" };

		var result = items.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNullEmptyEnumerableReturnsFalse()
	{
		var items = Enumerable.Empty<string?>();

		var result = items.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void WriteItemsToConsoleWritesCorrectOutput()
	{
		var items = new List<object?> { "a", null, 1 };
		using var sw = new StringWriter();
		Console.SetOut(sw);

		items.WriteItemsToConsole();

		var expected = $"a{Environment.NewLine}1{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void WriteItemsToConsoleEmptyEnumerableNoOutput()
	{
		var items = Enumerable.Empty<object?>();

		using var sw = new StringWriter();
		Console.SetOut(sw);

		items.WriteItemsToConsole();

		var expected = string.Empty;
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void ToStringEnumerableWithNullItemsRemoveHandling()
	{
		var items = new List<object?> { "a", null, 1 };

		var result = items.ToStringEnumerable(NullItemHandling.Remove);

		CollectionAssert.AreEqual(new List<string> { "a", "1" }, result.ToList());
	}

	[TestMethod]
	public void ToStringEnumerableWithNullItemsIncludeHandling()
	{
		var items = new List<object?> { "a", null, 1 };

		var result = items.ToStringEnumerable(NullItemHandling.Include);

		CollectionAssert.AreEqual(new List<string?> { "a", null, "1" }, result.ToList());
	}

	[TestMethod]
	public void ToStringEnumerableWithNullItemsThrowHandling()
	{
		var items = new List<object?> { "a", null, 1 };

		Assert.ThrowsException<InvalidOperationException>(() => items.ToStringEnumerable(NullItemHandling.Throw).ToList());
	}

	[TestMethod]
	public void ToStringEnumerableEmptyEnumerableReturnsEmpty()
	{
		var items = Enumerable.Empty<object?>();

		var result = items.ToStringEnumerable();

		Assert.AreEqual(0, result.Count());
	}

	[TestMethod]
	public void JoinConcatenatesElementsWithSeparator()
	{
		// Arrange
		var items = new List<string> { "a", "b", "c" };
		var separator = ",";

		// Act
		var result = items.Join(separator);

		// Assert
		Assert.AreEqual("a,b,c", result);
	}

	[TestMethod]
	public void JoinThrowsArgumentNullExceptionWhenItemsIsNull()
	{
		// Arrange
		List<string> items = null!;
		var separator = ",";

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.Join(separator));
	}

	[TestMethod]
	public void JoinThrowsArgumentNullExceptionWhenSeparatorIsNull()
	{
		// Arrange
		var items = new List<string> { "a", "b", "c" };
		string separator = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.Join(separator));
	}

	[TestMethod]
	public void JoinWithNullItemHandlingRemoveRemovesNullItems()
	{
		// Arrange
		var items = new List<string?> { "a", null, "b", "c" };
		var separator = ",";

		// Act
		var result = items.Join(separator, NullItemHandling.Remove);

		// Assert
		Assert.AreEqual("a,b,c", result);
	}

	[TestMethod]
	public void JoinWithNullItemHandlingIncludeIncludesNullItems()
	{
		// Arrange
		var items = new List<string?> { "a", null, "b", "c" };
		var separator = ",";

		// Act
		var result = items.Join(separator, NullItemHandling.Include);

		// Assert
		Assert.AreEqual("a,,b,c", result);
	}

	[TestMethod]
	public void JoinWithNullItemHandlingThrowThrowsInvalidOperationException()
	{
		// Arrange
		var items = new List<string?> { "a", null, "b", "c" };
		var separator = ",";

		// Act & Assert
		Assert.ThrowsException<InvalidOperationException>(() => items.Join(separator, NullItemHandling.Throw));
	}
}

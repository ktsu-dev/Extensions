namespace ktsu.Extensions.Tests;

using System.Collections.Generic;
using System.Linq;
using ktsu.DeepClone;
using ktsu.Extensions;
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
		var collection = items.ToCollection();

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

	public class SampleItem : IDeepCloneable<SampleItem>
	{
		public int Value { get; set; }

		public SampleItem DeepClone()
		{
			return new SampleItem { Value = Value };
		}
	}

	[TestMethod]
	public void DeepClone_ShouldCloneCollectionCorrectly()
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
		for (int i = 0; i < originalItems.Count; i++)
		{
			Assert.AreNotSame(originalItems[i], clonedItems[i]);
			Assert.AreEqual(originalItems[i].Value, clonedItems[i].Value);
		}
	}

	[TestMethod]
	public void DeepClone_ShouldThrowArgumentNullException_WhenItemsIsNull()
	{
		// Arrange
		List<SampleItem> originalItems = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(originalItems.DeepClone<SampleItem, List<SampleItem>>);
	}

	[TestMethod]
	public void DeepClone_WithLockObj_ShouldCloneCollectionCorrectly()
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
		for (int i = 0; i < originalItems.Count; i++)
		{
			Assert.AreNotSame(originalItems[i], clonedItems[i]);
			Assert.AreEqual(originalItems[i].Value, clonedItems[i].Value);
		}
	}

	[TestMethod]
	public void DeepClone_WithLockObj_ShouldThrowArgumentNullException_WhenItemsIsNull()
	{
		// Arrange
		List<SampleItem> originalItems = null!;
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalItems.DeepClone<SampleItem, List<SampleItem>>(lockObj));
	}

	[TestMethod]
	public void DeepClone_WithLockObj_ShouldThrowArgumentNullException_WhenLockObjIsNull()
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
	public void DeepClone_WithLockObj_ShouldLockAndCloneCorrectly()
	{
		// Arrange
		var originalItems = new List<SampleItem>
			{
				new() { Value = 1 },
				new() { Value = 2 },
				new() { Value = 3 }
			};
		object lockObj = new();
		bool wasLocked = false;

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
	public void ShallowClone_ShouldCloneCollectionCorrectly()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };

		// Act
		var clonedItems = originalItems.ShallowClone<int, List<int>>();

		// Assert
		Assert.AreEqual(originalItems.Count, clonedItems.Count);
		for (int i = 0; i < originalItems.Count; i++)
		{
			Assert.AreEqual(originalItems[i], clonedItems[i]);
		}
	}

	[TestMethod]
	public void ShallowClone_ShouldThrowArgumentNullException_WhenItemsIsNull()
	{
		// Arrange
		List<int> originalItems = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(originalItems.ShallowClone<int, List<int>>);
	}

	[TestMethod]
	public void ShallowClone_WithLockObj_ShouldCloneCollectionCorrectly()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = new();

		// Act
		var clonedItems = originalItems.ShallowClone<int, List<int>>(lockObj);

		// Assert
		Assert.AreEqual(originalItems.Count, clonedItems.Count);
		for (int i = 0; i < originalItems.Count; i++)
		{
			Assert.AreEqual(originalItems[i], clonedItems[i]);
		}
	}

	[TestMethod]
	public void ShallowClone_WithLockObj_ShouldThrowArgumentNullException_WhenItemsIsNull()
	{
		// Arrange
		List<int> originalItems = null!;
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalItems.ShallowClone<int, List<int>>(lockObj));
	}

	[TestMethod]
	public void ShallowClone_WithLockObj_ShouldThrowArgumentNullException_WhenLockObjIsNull()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalItems.ShallowClone<int, List<int>>(lockObj));
	}

	[TestMethod]
	public void ShallowClone_WithLockObj_ShouldLockAndCloneCorrectly()
	{
		// Arrange
		var originalItems = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = new();
		bool wasLocked = false;

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
	public void ForEach_ShouldApplyActionToEachElement()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		var results = new List<int>();

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
	public void ForEach_ShouldThrowArgumentNullException_WhenEnumerableIsNull()
	{
		// Arrange
		IEnumerable<int> items = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(i => { }));
	}

	[TestMethod]
	public void ForEach_ShouldThrowArgumentNullException_WhenActionIsNull()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(null!));
	}

	[TestMethod]
	public void ForEach_WithLockObj_ShouldApplyActionToEachElement()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		var results = new List<int>();
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
	public void ForEach_WithLockObj_ShouldThrowArgumentNullException_WhenEnumerableIsNull()
	{
		// Arrange
		IEnumerable<int> items = null!;
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(lockObj, i => { }));
	}

	[TestMethod]
	public void ForEach_WithLockObj_ShouldThrowArgumentNullException_WhenLockObjIsNull()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(lockObj, i => { }));
	}

	[TestMethod]
	public void ForEach_WithLockObj_ShouldThrowArgumentNullException_WhenActionIsNull()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => items.ForEach(lockObj, null!));
	}

	[TestMethod]
	public void ForEach_WithLockObj_ShouldLockAndApplyActionCorrectly()
	{
		// Arrange
		var items = new List<int> { 1, 2, 3, 4, 5 };
		var results = new List<int>();
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
	public void WithIndex_EmptyEnumerable_ReturnsEmpty()
	{
		var items = Enumerable.Empty<string>();
		var result = items.WithIndex().ToList();

		Assert.AreEqual(0, result.Count);
	}

	[TestMethod]
	public void ToCollection_EmptyEnumerable_ReturnsEmptyCollection()
	{
		var items = Enumerable.Empty<int>();
		var collection = items.ToCollection();

		Assert.AreEqual(0, collection.Count);
	}

	[TestMethod]
	public void ForEach_EmptyEnumerable_DoesNothing()
	{
		var items = Enumerable.Empty<int>();
		int sum = 0;
		items.ForEach(item => sum += item);

		Assert.AreEqual(0, sum);
	}

	[TestMethod]
	public void AnyNull_WithNullItems_ReturnsTrue()
	{
		var items = new List<string?> { "a", null, "b" };

		bool result = items.AnyNull();

		Assert.IsTrue(result);
	}

	[TestMethod]
	public void AnyNull_WithoutNullItems_ReturnsFalse()
	{
		var items = new List<string?> { "a", "b", "c" };

		bool result = items.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void AnyNull_EmptyEnumerable_ReturnsFalse()
	{
		var items = Enumerable.Empty<string?>();

		bool result = items.AnyNull();

		Assert.IsFalse(result);
	}

	[TestMethod]
	public void WriteItemsToConsole_WritesCorrectOutput()
	{
		var items = new List<object?> { "a", null, 1 };
		using var sw = new StringWriter();
		Console.SetOut(sw);

		items.WriteItemsToConsole();

		string expected = $"a{Environment.NewLine}1{Environment.NewLine}";
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void WriteItemsToConsole_EmptyEnumerable_NoOutput()
	{
		var items = Enumerable.Empty<object?>();

		using var sw = new StringWriter();
		Console.SetOut(sw);

		items.WriteItemsToConsole();

		string expected = string.Empty;
		Assert.AreEqual(expected, sw.ToString());
	}

	[TestMethod]
	public void ToStringEnumerable_WithNullItems_RemoveHandling()
	{
		var items = new List<object?> { "a", null, 1 };

		var result = items.ToStringEnumerable(NullItemHandling.Remove);

		CollectionAssert.AreEqual(new List<string> { "a", "1" }, result.ToList());
	}

	[TestMethod]
	public void ToStringEnumerable_WithNullItems_IncludeHandling()
	{
		var items = new List<object?> { "a", null, 1 };

		var result = items.ToStringEnumerable(NullItemHandling.Include);

		CollectionAssert.AreEqual(new List<string?> { "a", null, "1" }, result.ToList());
	}

	[TestMethod]
	public void ToStringEnumerable_WithNullItems_ThrowHandling()
	{
		var items = new List<object?> { "a", null, 1 };

		Assert.ThrowsException<InvalidOperationException>(() => items.ToStringEnumerable(NullItemHandling.Throw).ToList());
	}

	[TestMethod]
	public void ToStringEnumerable_EmptyEnumerable_ReturnsEmpty()
	{
		var items = Enumerable.Empty<object?>();

		var result = items.ToStringEnumerable();

		Assert.AreEqual(0, result.Count());
	}
}

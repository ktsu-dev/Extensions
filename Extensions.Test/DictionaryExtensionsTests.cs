namespace ktsu.io.Extensions.Tests;

using System.Collections.Concurrent;
using ktsu.io.DeepClone;
using ktsu.io.Extensions;

[TestClass]
public class DictionaryExtensionsTests
{
	[TestMethod]
	public void GetOrCreate_ShouldReturnExistingValue()
	{
		var dictionary = new Dictionary<string, int> { { "key1", 42 } };

		int result = dictionary.GetOrCreate("key1");

		Assert.AreEqual(42, result);
	}

	[TestMethod]
	public void GetOrCreate_ShouldAddAndReturnNewValue()
	{
		var dictionary = new Dictionary<string, int>();

		int result = dictionary.GetOrCreate("key1");

		Assert.AreEqual(0, result);
		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(0, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreate_ShouldAddAndReturnDefaultValue()
	{
		var dictionary = new Dictionary<string, int>();

		int result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(99, result);
		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreate_ConcurrentDictionary_ShouldReturnExistingValue()
	{
		var dictionary = new ConcurrentDictionary<string, int>();
		dictionary.TryAdd("key1", 42);

		int result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(42, result);
	}

	[TestMethod]
	public void GetOrCreate_ConcurrentDictionary_ShouldAddAndReturnDefaultValue()
	{
		var dictionary = new ConcurrentDictionary<string, int>();

		int result = dictionary.GetOrCreate("key1", 99);

		Assert.AreEqual(99, result);
		Assert.AreEqual(1, dictionary.Count);
		Assert.AreEqual(99, dictionary["key1"]);
	}

	[TestMethod]
	public void GetOrCreate_ShouldThrowArgumentNullException_WhenDictionaryIsNull()
	{
		Dictionary<string, int>? dictionary = null!;

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.GetOrCreate("key1"));
	}

	[TestMethod]
	public void GetOrCreate_ShouldThrowArgumentNullException_WhenKeyIsNull()
	{
		var dictionary = new Dictionary<string, int>();

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.GetOrCreate(null!));
	}

	[TestMethod]
	public void GetOrCreate_ShouldThrowArgumentNullException_WhenDefaultValueIsNull()
	{
		var dictionary = new Dictionary<string, DictionaryExtensionsTests>();

		Assert.ThrowsException<ArgumentNullException>(() => dictionary.GetOrCreate("key1", null!));
	}

	public class SampleValue : IDeepCloneable<SampleValue>
	{
		public int Value { get; set; }

		public SampleValue DeepClone()
		{
			return new SampleValue { Value = Value };
		}
	}

	[TestMethod]
	public void DeepCloneDictionary_ShouldCloneDictionaryCorrectly()
	{
		// Arrange
		var originalDict = new Dictionary<string, SampleValue>
		{
			{ "One", new SampleValue { Value = 1 } },
			{ "Two", new SampleValue { Value = 2 } },
			{ "Three", new SampleValue { Value = 3 } }
		};

		// Act
		var clonedDict = originalDict.DeepClone();

		// Assert
		Assert.AreEqual(originalDict.Count, clonedDict.Count);
		foreach (var kvp in originalDict)
		{
			Assert.IsTrue(clonedDict.ContainsKey(kvp.Key));
			Assert.AreNotSame(kvp.Value, clonedDict[kvp.Key]);
			Assert.AreEqual(kvp.Value.Value, clonedDict[kvp.Key].Value);
		}
	}

	[TestMethod]
	public void DeepCloneDictionary_ShouldThrowArgumentNullException_WhenItemsIsNull()
	{
		// Arrange
		Dictionary<string, SampleValue> originalDict = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalDict.DeepClone());
	}

	[TestMethod]
	public void DeepCloneDictionary_WithLockObj_ShouldCloneDictionaryCorrectly()
	{
		// Arrange
		var originalDict = new Dictionary<string, SampleValue>
		{
			{ "One", new SampleValue { Value = 1 } },
			{ "Two", new SampleValue { Value = 2 } },
			{ "Three", new SampleValue { Value = 3 } }
		};
		object lockObj = new();

		// Act
		var clonedDict = originalDict.DeepClone(lockObj);

		// Assert
		Assert.AreEqual(originalDict.Count, clonedDict.Count);
		foreach (var kvp in originalDict)
		{
			Assert.IsTrue(clonedDict.ContainsKey(kvp.Key));
			Assert.AreNotSame(kvp.Value, clonedDict[kvp.Key]);
			Assert.AreEqual(kvp.Value.Value, clonedDict[kvp.Key].Value);
		}
	}

	[TestMethod]
	public void DeepCloneDictionary_WithLockObj_ShouldThrowArgumentNullException_WhenItemsIsNull()
	{
		// Arrange
		Dictionary<string, SampleValue> originalDict = null!;
		object lockObj = new();

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalDict.DeepClone(lockObj));
	}

	[TestMethod]
	public void DeepCloneDictionary_WithLockObj_ShouldThrowArgumentNullException_WhenLockObjIsNull()
	{
		// Arrange
		var originalDict = new Dictionary<string, SampleValue>
		{
			{ "One", new SampleValue { Value = 1 } },
			{ "Two", new SampleValue { Value = 2 } },
			{ "Three", new SampleValue { Value = 3 } }
		};
		object lockObj = null!;

		// Act & Assert
		Assert.ThrowsException<ArgumentNullException>(() => originalDict.DeepClone(lockObj));
	}

	[TestMethod]
	public void DeepCloneDictionary_WithLockObj_ShouldLockAndCloneCorrectly()
	{
		// Arrange
		var originalDict = new Dictionary<string, SampleValue>
		{
			{ "One", new SampleValue { Value = 1 } },
			{ "Two", new SampleValue { Value = 2 } },
			{ "Three", new SampleValue { Value = 3 } }
		};
		object lockObj = new();
		bool wasLocked = false;

		// Act
		lock (lockObj)
		{
			wasLocked = true;
			var clonedDict = originalDict.DeepClone(lockObj);
			wasLocked = false;
		}

		// Assert
		Assert.IsFalse(wasLocked, "The lock object should have been released after the method executed.");
	}
}

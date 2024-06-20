namespace ktsu.io.Extensions.Tests;

using System.Collections.Concurrent;
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
}

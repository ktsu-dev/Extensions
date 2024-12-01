namespace ktsu.Extensions.Tests;

using ktsu.Extensions;
using ktsu.StrongStrings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StringExtensionsTests
{
	public record MyStrongString : AnyStrongString<MyStrongString> { }

	[TestMethod]
	public void StartsWithOrdinal_StringComparison_ReturnsTrue()
	{
		string str = "hello world";
		string value = "hello";
		Assert.IsTrue(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void StartsWithOrdinal_StringComparison_ReturnsFalse()
	{
		string str = "hello world";
		string value = "world";
		Assert.IsFalse(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinal_StringComparison_ReturnsTrue()
	{
		string str = "hello world";
		string value = "world";
		Assert.IsTrue(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinal_StringComparison_ReturnsFalse()
	{
		string str = "hello world";
		string value = "hello";
		Assert.IsFalse(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinal_StringComparison_ReturnsTrue()
	{
		string str = "hello world";
		string value = "lo wo";
		Assert.IsTrue(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinal_StringComparison_ReturnsFalse()
	{
		string str = "hello world";
		string value = "worlds";
		Assert.IsFalse(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffix_RemovesSuffixCorrectly()
	{
		string str = "filename.txt";
		string suffix = ".txt";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemoveSuffix_DoesNotRemoveIfSuffixNotFound()
	{
		string str = "filename.txt";
		string suffix = ".csv";
		Assert.AreEqual(str, str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefix_RemovesPrefixCorrectly()
	{
		string str = "prefix_filename";
		string prefix = "prefix_";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void RemovePrefix_DoesNotRemoveIfPrefixNotFound()
	{
		string str = "prefix_filename";
		string prefix = "suffix_";
		Assert.AreEqual(str, str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinal_ReplacesCorrectly()
	{
		string str = "hello world";
		string oldValue = "world";
		string newValue = "universe";
		string expected = "hello universe";
		Assert.AreEqual(expected, str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinal_DoesNotReplaceIfOldValueNotFound()
	{
		string str = "hello world";
		string oldValue = "earth";
		string newValue = "universe";
		Assert.AreEqual(str, str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void StartsWithOrdinal_StrongStringComparison_ReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		string value = "hello";
		Assert.IsTrue(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinal_StrongStringComparison_ReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		string value = "world";
		Assert.IsTrue(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinal_StrongStringComparison_ReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		string value = "lo wo";
		Assert.IsTrue(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffix_StrongString_RemovesSuffixCorrectly()
	{
		var str = (MyStrongString)"filename.txt";
		string suffix = ".txt";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemoveSuffix(suffix));
		Assert.AreEqual(expected, expected.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefix_StrongString_RemovesPrefixCorrectly()
	{
		var str = (MyStrongString)"prefix_filename";
		string prefix = "prefix_";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemovePrefix(prefix));
		Assert.AreEqual(expected, expected.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinal_ShouldReplaceAllOccurrences()
	{
		// Arrange
		var originalString = (MyStrongString)"Hello, World! Hello, Universe!";
		string oldValue = "Hello";
		string newValue = "Hi";

		// Act
		string result = originalString.ReplaceOrdinal(oldValue, newValue);

		// Assert
		Assert.AreEqual("Hi, World! Hi, Universe!", result);
	}

	[TestMethod]
	public void ReplaceOrdinal_ShouldThrowArgumentNullException_WhenSourceIsNull()
	{
		MyStrongString originalString = null!;
		string oldValue = "Hello";
		string newValue = "Hi";

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinal_ShouldThrowArgumentNullException_WhenOldValueIsNull()
	{
		var originalString = (MyStrongString)"Hello, World!";
		string oldValue = null!;
		string newValue = "Hi";

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinal_ShouldThrowArgumentNullException_WhenNewValueIsNull()
	{
		var originalString = (MyStrongString)"Hello, World!";
		string oldValue = "Hello";
		string newValue = null!;

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	// Additional tests for edge cases and scenarios

	[TestMethod]
	public void As_ConvertsWeakStringToStrongString()
	{
		string weakString = "hello world";
		var strongString = weakString.As<MyStrongString>();

		Assert.AreEqual(weakString, strongString.ToString());
	}

	[TestMethod]
	public void As_ThrowsArgumentNullException_WhenWeakStringIsNull()
	{
		string weakString = null!;

		Assert.ThrowsException<ArgumentNullException>(weakString.As<MyStrongString>);
	}

	[TestMethod]
	public void StartsWithOrdinal_ThrowsArgumentNullException_WhenStringIsNull()
	{
		string str = null!;
		string value = "hello";

		Assert.ThrowsException<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void StartsWithOrdinal_ThrowsArgumentNullException_WhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinal_ThrowsArgumentNullException_WhenStringIsNull()
	{
		string str = null!;
		string value = "world";

		Assert.ThrowsException<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinal_ThrowsArgumentNullException_WhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinal_ThrowsArgumentNullException_WhenStringIsNull()
	{
		string str = null!;
		string value = "lo wo";

		Assert.ThrowsException<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinal_ThrowsArgumentNullException_WhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffix_ThrowsArgumentNullException_WhenStringIsNull()
	{
		string str = null!;
		string suffix = ".txt";

		Assert.ThrowsException<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemoveSuffix_ThrowsArgumentNullException_WhenSuffixIsNull()
	{
		string str = "filename.txt";
		string suffix = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefix_ThrowsArgumentNullException_WhenStringIsNull()
	{
		string str = null!;
		string prefix = "prefix_";

		Assert.ThrowsException<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void RemovePrefix_ThrowsArgumentNullException_WhenPrefixIsNull()
	{
		string str = "prefix_filename";
		string prefix = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinal_ThrowsArgumentNullException_WhenStringIsNull()
	{
		string str = null!;
		string oldValue = "world";
		string newValue = "universe";

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinal_ThrowsArgumentNullException_WhenOldValueIsNull()
	{
		string str = "hello world";
		string oldValue = null!;
		string newValue = "universe";

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinal_ThrowsArgumentNullException_WhenNewValueIsNull()
	{
		string str = "hello world";
		string oldValue = "world";
		string newValue = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}
}

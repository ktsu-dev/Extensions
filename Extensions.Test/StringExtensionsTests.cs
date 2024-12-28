namespace ktsu.Extensions.Tests;

using ktsu.Extensions;
using ktsu.StrongStrings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class StringExtensionsTests
{
	public record MyStrongString : AnyStrongString<MyStrongString> { }

	[TestMethod]
	public void StartsWithOrdinalStringComparisonReturnsTrue()
	{
		string str = "hello world";
		string value = "hello";
		Assert.IsTrue(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void StartsWithOrdinalStringComparisonReturnsFalse()
	{
		string str = "hello world";
		string value = "world";
		Assert.IsFalse(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalStringComparisonReturnsTrue()
	{
		string str = "hello world";
		string value = "world";
		Assert.IsTrue(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalStringComparisonReturnsFalse()
	{
		string str = "hello world";
		string value = "hello";
		Assert.IsFalse(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalStringComparisonReturnsTrue()
	{
		string str = "hello world";
		string value = "lo wo";
		Assert.IsTrue(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalStringComparisonReturnsFalse()
	{
		string str = "hello world";
		string value = "worlds";
		Assert.IsFalse(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffixRemovesSuffixCorrectly()
	{
		string str = "filename.txt";
		string suffix = ".txt";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemoveSuffixDoesNotRemoveIfSuffixNotFound()
	{
		string str = "filename.txt";
		string suffix = ".csv";
		Assert.AreEqual(str, str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefixRemovesPrefixCorrectly()
	{
		string str = "prefix_filename";
		string prefix = "prefix_";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void RemovePrefixDoesNotRemoveIfPrefixNotFound()
	{
		string str = "prefix_filename";
		string prefix = "suffix_";
		Assert.AreEqual(str, str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinalReplacesCorrectly()
	{
		string str = "hello world";
		string oldValue = "world";
		string newValue = "universe";
		string expected = "hello universe";
		Assert.AreEqual(expected, str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalDoesNotReplaceIfOldValueNotFound()
	{
		string str = "hello world";
		string oldValue = "earth";
		string newValue = "universe";
		Assert.AreEqual(str, str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void StartsWithOrdinalStrongStringComparisonReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		string value = "hello";
		Assert.IsTrue(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalStrongStringComparisonReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		string value = "world";
		Assert.IsTrue(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalStrongStringComparisonReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		string value = "lo wo";
		Assert.IsTrue(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffixStrongStringRemovesSuffixCorrectly()
	{
		var str = (MyStrongString)"filename.txt";
		string suffix = ".txt";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemoveSuffix(suffix));
		Assert.AreEqual(expected, expected.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefixStrongStringRemovesPrefixCorrectly()
	{
		var str = (MyStrongString)"prefix_filename";
		string prefix = "prefix_";
		string expected = "filename";
		Assert.AreEqual(expected, str.RemovePrefix(prefix));
		Assert.AreEqual(expected, expected.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinalShouldReplaceAllOccurrences()
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
	public void ReplaceOrdinalShouldThrowArgumentNullExceptionWhenSourceIsNull()
	{
		MyStrongString originalString = null!;
		string oldValue = "Hello";
		string newValue = "Hi";

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalShouldThrowArgumentNullExceptionWhenOldValueIsNull()
	{
		var originalString = (MyStrongString)"Hello, World!";
		string oldValue = null!;
		string newValue = "Hi";

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalShouldThrowArgumentNullExceptionWhenNewValueIsNull()
	{
		var originalString = (MyStrongString)"Hello, World!";
		string oldValue = "Hello";
		string newValue = null!;

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	// Additional tests for edge cases and scenarios

	[TestMethod]
	public void AsConvertsWeakStringToStrongString()
	{
		string weakString = "hello world";
		var strongString = weakString.As<MyStrongString>();

		Assert.AreEqual(weakString, strongString.ToString());
	}

	[TestMethod]
	public void AsThrowsArgumentNullExceptionWhenWeakStringIsNull()
	{
		string weakString = null!;

		Assert.ThrowsException<ArgumentNullException>(weakString.As<MyStrongString>);
	}

	[TestMethod]
	public void StartsWithOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string value = "hello";

		Assert.ThrowsException<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void StartsWithOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string value = "world";

		Assert.ThrowsException<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string value = "lo wo";

		Assert.ThrowsException<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffixThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string suffix = ".txt";

		Assert.ThrowsException<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemoveSuffixThrowsArgumentNullExceptionWhenSuffixIsNull()
	{
		string str = "filename.txt";
		string suffix = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefixThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string prefix = "prefix_";

		Assert.ThrowsException<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void RemovePrefixThrowsArgumentNullExceptionWhenPrefixIsNull()
	{
		string str = "prefix_filename";
		string prefix = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string oldValue = "world";
		string newValue = "universe";

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenOldValueIsNull()
	{
		string str = "hello world";
		string oldValue = null!;
		string newValue = "universe";

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenNewValueIsNull()
	{
		string str = "hello world";
		string oldValue = "world";
		string newValue = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	// Tests for DetermineLineEndingStyle and NormalizeLineEndings

	[TestMethod]
	public void DetermineLineEndingStyleReturnsUnix()
	{
		string input = "line1\nline2\nline3\n";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(StringExtensions.LineEndingStyle.Unix, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsWindows()
	{
		string input = "line1\r\nline2\r\nline3\r\n";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(StringExtensions.LineEndingStyle.Windows, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsMac()
	{
		string input = "line1\rline2\rline3\r";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(StringExtensions.LineEndingStyle.Mac, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsMixed()
	{
		string input = "line1\nline2\r\nline3\r";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(StringExtensions.LineEndingStyle.Mixed, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsNone()
	{
		string input = "line1 line2 line3";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(StringExtensions.LineEndingStyle.None, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToUnix()
	{
		string input = "line1\r\nline2\rline3\n";
		string expected = "line1\nline2\nline3\n";
		string result = input.NormalizeLineEndings(StringExtensions.LineEndingStyle.Unix);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToWindows()
	{
		string input = "line1\nline2\rline3\r\n";
		string expected = "line1\r\nline2\r\nline3\r\n";
		string result = input.NormalizeLineEndings(StringExtensions.LineEndingStyle.Windows);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToMac()
	{
		string input = "line1\nline2\r\nline3\r";
		string expected = "line1\rline2\rline3\r";
		string result = input.NormalizeLineEndings(StringExtensions.LineEndingStyle.Mac);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToNone()
	{
		string input = "line1\nline2\r\nline3\r";
		string expected = "line1line2line3";
		string result = input.NormalizeLineEndings(StringExtensions.LineEndingStyle.None);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToMixed()
	{
		string input = "line1\r\nline2\rline3\n";
		string expected = "line1\nline2\nline3\n";
		string result = input.NormalizeLineEndings(StringExtensions.LineEndingStyle.Mixed);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleThrowsArgumentNullExceptionWhenInputIsNull()
	{
		string input = null!;
		Assert.ThrowsException<ArgumentNullException>(() => input.DetermineLineEndings());
	}

	[TestMethod]
	public void NormalizeLineEndingsThrowsArgumentNullExceptionWhenInputIsNull()
	{
		string input = null!;
		Assert.ThrowsException<ArgumentNullException>(() => input.NormalizeLineEndings(StringExtensions.LineEndingStyle.Unix));
	}

	[TestMethod]
	public void NormalizeLineEndingsThrowsNotImplementedExceptionWhenUnknownStyle()
	{
		string input = "line1\nline2\r\nline3\r";
		Assert.ThrowsException<NotImplementedException>(() => input.NormalizeLineEndings((StringExtensions.LineEndingStyle)999));
	}
}


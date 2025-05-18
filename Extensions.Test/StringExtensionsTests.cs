// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions.Tests;

using ktsu.StrongStrings;

[TestClass]
public class StringExtensionsTests
{
	public record MyStrongString : AnyStrongString<MyStrongString> { }

	[TestMethod]
	public void StartsWithOrdinalStringComparisonReturnsTrue()
	{
		var str = "hello world";
		var value = "hello";
		Assert.IsTrue(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void StartsWithOrdinalStringComparisonReturnsFalse()
	{
		var str = "hello world";
		var value = "world";
		Assert.IsFalse(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalStringComparisonReturnsTrue()
	{
		var str = "hello world";
		var value = "world";
		Assert.IsTrue(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalStringComparisonReturnsFalse()
	{
		var str = "hello world";
		var value = "hello";
		Assert.IsFalse(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalStringComparisonReturnsTrue()
	{
		var str = "hello world";
		var value = "lo wo";
		Assert.IsTrue(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalStringComparisonReturnsFalse()
	{
		var str = "hello world";
		var value = "worlds";
		Assert.IsFalse(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffixRemovesSuffixCorrectly()
	{
		var str = "filename.txt";
		var suffix = ".txt";
		var expected = "filename";
		Assert.AreEqual(expected, str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemoveSuffixDoesNotRemoveIfSuffixNotFound()
	{
		var str = "filename.txt";
		var suffix = ".csv";
		Assert.AreEqual(str, str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefixRemovesPrefixCorrectly()
	{
		var str = "prefix_filename";
		var prefix = "prefix_";
		var expected = "filename";
		Assert.AreEqual(expected, str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void RemovePrefixDoesNotRemoveIfPrefixNotFound()
	{
		var str = "prefix_filename";
		var prefix = "suffix_";
		Assert.AreEqual(str, str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinalReplacesCorrectly()
	{
		var str = "hello world";
		var oldValue = "world";
		var newValue = "universe";
		var expected = "hello universe";
		Assert.AreEqual(expected, str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalDoesNotReplaceIfOldValueNotFound()
	{
		var str = "hello world";
		var oldValue = "earth";
		var newValue = "universe";
		Assert.AreEqual(str, str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void StartsWithOrdinalStrongStringComparisonReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		var value = "hello";
		Assert.IsTrue(str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalStrongStringComparisonReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		var value = "world";
		Assert.IsTrue(str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalStrongStringComparisonReturnsTrue()
	{
		var str = (MyStrongString)"hello world";
		var value = "lo wo";
		Assert.IsTrue(str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffixStrongStringRemovesSuffixCorrectly()
	{
		var str = (MyStrongString)"filename.txt";
		var suffix = ".txt";
		var expected = "filename";
		Assert.AreEqual(expected, str.RemoveSuffix(suffix));
		Assert.AreEqual(expected, expected.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefixStrongStringRemovesPrefixCorrectly()
	{
		var str = (MyStrongString)"prefix_filename";
		var prefix = "prefix_";
		var expected = "filename";
		Assert.AreEqual(expected, str.RemovePrefix(prefix));
		Assert.AreEqual(expected, expected.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinalShouldReplaceAllOccurrences()
	{
		// Arrange
		var originalString = (MyStrongString)"Hello, World! Hello, Universe!";
		var oldValue = "Hello";
		var newValue = "Hi";

		// Act
		var result = originalString.ReplaceOrdinal(oldValue, newValue);

		// Assert
		Assert.AreEqual("Hi, World! Hi, Universe!", result);
	}

	[TestMethod]
	public void ReplaceOrdinalShouldThrowArgumentNullExceptionWhenSourceIsNull()
	{
		MyStrongString originalString = null!;
		var oldValue = "Hello";
		var newValue = "Hi";

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalShouldThrowArgumentNullExceptionWhenOldValueIsNull()
	{
		var originalString = (MyStrongString)"Hello, World!";
		string oldValue = null!;
		var newValue = "Hi";

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalShouldThrowArgumentNullExceptionWhenNewValueIsNull()
	{
		var originalString = (MyStrongString)"Hello, World!";
		var oldValue = "Hello";
		string newValue = null!;

		Assert.ThrowsException<ArgumentNullException>(() => originalString.ReplaceOrdinal(oldValue, newValue));
	}

	// Additional tests for edge cases and scenarios

	[TestMethod]
	public void AsConvertsWeakStringToStrongString()
	{
		var weakString = "hello world";
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
		var value = "hello";

		Assert.ThrowsException<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void StartsWithOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		var str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		var value = "world";

		Assert.ThrowsException<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		var str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		var value = "lo wo";

		Assert.ThrowsException<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		var str = "hello world";
		string value = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffixThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		var suffix = ".txt";

		Assert.ThrowsException<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemoveSuffixThrowsArgumentNullExceptionWhenSuffixIsNull()
	{
		var str = "filename.txt";
		string suffix = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefixThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		var prefix = "prefix_";

		Assert.ThrowsException<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void RemovePrefixThrowsArgumentNullExceptionWhenPrefixIsNull()
	{
		var str = "prefix_filename";
		string prefix = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		var oldValue = "world";
		var newValue = "universe";

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenOldValueIsNull()
	{
		var str = "hello world";
		string oldValue = null!;
		var newValue = "universe";

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenNewValueIsNull()
	{
		var str = "hello world";
		var oldValue = "world";
		string newValue = null!;

		Assert.ThrowsException<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	// Tests for DetermineLineEndingStyle and NormalizeLineEndings

	[TestMethod]
	public void DetermineLineEndingStyleReturnsUnix()
	{
		var input = "line1\nline2\nline3\n";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Unix, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsWindows()
	{
		var input = "line1\r\nline2\r\nline3\r\n";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Windows, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsMac()
	{
		var input = "line1\rline2\rline3\r";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Mac, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsMixed()
	{
		var input = "line1\nline2\r\nline3\r";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Mixed, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsNone()
	{
		var input = "line1 line2 line3";
		var result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.None, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToUnix()
	{
		var input = "line1\r\nline2\rline3\n";
		var expected = "line1\nline2\nline3\n";
		var result = input.NormalizeLineEndings(LineEndingStyle.Unix);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToWindows()
	{
		var input = "line1\nline2\rline3\r\n";
		var expected = "line1\r\nline2\r\nline3\r\n";
		var result = input.NormalizeLineEndings(LineEndingStyle.Windows);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToMac()
	{
		var input = "line1\nline2\r\nline3\r";
		var expected = "line1\rline2\rline3\r";
		var result = input.NormalizeLineEndings(LineEndingStyle.Mac);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToNone()
	{
		var input = "line1\nline2\r\nline3\r";
		var expected = "line1line2line3";
		var result = input.NormalizeLineEndings(LineEndingStyle.None);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToMixed()
	{
		var input = "line1\r\nline2\rline3\n";
		var expected = "line1\nline2\nline3\n";
		var result = input.NormalizeLineEndings(LineEndingStyle.Mixed);
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
		Assert.ThrowsException<ArgumentNullException>(() => input.NormalizeLineEndings(LineEndingStyle.Unix));
	}

	[TestMethod]
	public void NormalizeLineEndingsThrowsNotImplementedExceptionWhenUnknownStyle()
	{
		var input = "line1\nline2\r\nline3\r";
		Assert.ThrowsException<NotImplementedException>(() => input.NormalizeLineEndings((LineEndingStyle)999));
	}
}


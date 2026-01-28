// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions.Tests;

[TestClass]
public class StringExtensionsTests
{
	[TestMethod]
	public void StartsWithOrdinalStringComparisonReturnsTrue()
	{
		string str = "hello world";
		string value = "hello";
		Assert.IsTrue(str.StartsWithOrdinal(value), "StartsWithOrdinal should return true when the string starts with the specified value.");
	}

	[TestMethod]
	public void StartsWithOrdinalStringComparisonReturnsFalse()
	{
		string str = "hello world";
		string value = "world";
		Assert.IsFalse(str.StartsWithOrdinal(value), "StartsWithOrdinal should return false when the string does not start with the specified value.");
	}

	[TestMethod]
	public void EndsWithOrdinalStringComparisonReturnsTrue()
	{
		string str = "hello world";
		string value = "world";
		Assert.IsTrue(str.EndsWithOrdinal(value), "EndsWithOrdinal should return true when the string ends with the specified value.");
	}

	[TestMethod]
	public void EndsWithOrdinalStringComparisonReturnsFalse()
	{
		string str = "hello world";
		string value = "hello";
		Assert.IsFalse(str.EndsWithOrdinal(value), "EndsWithOrdinal should return false when the string does not end with the specified value.");
	}

	[TestMethod]
	public void ContainsOrdinalStringComparisonReturnsTrue()
	{
		string str = "hello world";
		string value = "lo wo";
		Assert.IsTrue(str.ContainsOrdinal(value), "ContainsOrdinal should return true when the string contains the specified value.");
	}

	[TestMethod]
	public void ContainsOrdinalStringComparisonReturnsFalse()
	{
		string str = "hello world";
		string value = "worlds";
		Assert.IsFalse(str.ContainsOrdinal(value), "ContainsOrdinal should return false when the string does not contain the specified value.");
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
	public void StartsWithOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string value = "hello";

		Assert.ThrowsExactly<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void StartsWithOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => str.StartsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string value = "world";

		Assert.ThrowsExactly<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void EndsWithOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => str.EndsWithOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string value = "lo wo";

		Assert.ThrowsExactly<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void ContainsOrdinalThrowsArgumentNullExceptionWhenValueIsNull()
	{
		string str = "hello world";
		string value = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => str.ContainsOrdinal(value));
	}

	[TestMethod]
	public void RemoveSuffixThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string suffix = ".txt";

		Assert.ThrowsExactly<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemoveSuffixThrowsArgumentNullExceptionWhenSuffixIsNull()
	{
		string str = "filename.txt";
		string suffix = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => str.RemoveSuffix(suffix));
	}

	[TestMethod]
	public void RemovePrefixThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string prefix = "prefix_";

		Assert.ThrowsExactly<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void RemovePrefixThrowsArgumentNullExceptionWhenPrefixIsNull()
	{
		string str = "prefix_filename";
		string prefix = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => str.RemovePrefix(prefix));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenStringIsNull()
	{
		string str = null!;
		string oldValue = "world";
		string newValue = "universe";

		Assert.ThrowsExactly<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenOldValueIsNull()
	{
		string str = "hello world";
		string oldValue = null!;
		string newValue = "universe";

		Assert.ThrowsExactly<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	[TestMethod]
	public void ReplaceOrdinalThrowsArgumentNullExceptionWhenNewValueIsNull()
	{
		string str = "hello world";
		string oldValue = "world";
		string newValue = null!;

		Assert.ThrowsExactly<ArgumentNullException>(() => str.ReplaceOrdinal(oldValue, newValue));
	}

	// Tests for DetermineLineEndingStyle and NormalizeLineEndings

	[TestMethod]
	public void DetermineLineEndingStyleReturnsUnix()
	{
		string input = "line1\nline2\nline3\n";
		LineEndingStyle result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Unix, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsWindows()
	{
		string input = "line1\r\nline2\r\nline3\r\n";
		LineEndingStyle result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Windows, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsMac()
	{
		string input = "line1\rline2\rline3\r";
		LineEndingStyle result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Mac, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsMixed()
	{
		string input = "line1\nline2\r\nline3\r";
		LineEndingStyle result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.Mixed, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleReturnsNone()
	{
		string input = "line1 line2 line3";
		LineEndingStyle result = input.DetermineLineEndings();
		Assert.AreEqual(LineEndingStyle.None, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToUnix()
	{
		string input = "line1\r\nline2\rline3\n";
		string expected = "line1\nline2\nline3\n";
		string result = input.NormalizeLineEndings(LineEndingStyle.Unix);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToWindows()
	{
		string input = "line1\nline2\rline3\r\n";
		string expected = "line1\r\nline2\r\nline3\r\n";
		string result = input.NormalizeLineEndings(LineEndingStyle.Windows);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToMac()
	{
		string input = "line1\nline2\r\nline3\r";
		string expected = "line1\rline2\rline3\r";
		string result = input.NormalizeLineEndings(LineEndingStyle.Mac);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToNone()
	{
		string input = "line1\nline2\r\nline3\r";
		string expected = "line1line2line3";
		string result = input.NormalizeLineEndings(LineEndingStyle.None);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NormalizeLineEndingsToMixed()
	{
		string input = "line1\r\nline2\rline3\n";
		string expected = "line1\nline2\nline3\n";
		string result = input.NormalizeLineEndings(LineEndingStyle.Mixed);
		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void DetermineLineEndingStyleThrowsArgumentNullExceptionWhenInputIsNull()
	{
		string input = null!;
		Assert.ThrowsExactly<ArgumentNullException>(() => input.DetermineLineEndings());
	}

	[TestMethod]
	public void NormalizeLineEndingsThrowsArgumentNullExceptionWhenInputIsNull()
	{
		string input = null!;
		Assert.ThrowsExactly<ArgumentNullException>(() => input.NormalizeLineEndings(LineEndingStyle.Unix));
	}

	[TestMethod]
	public void NormalizeLineEndingsThrowsNotImplementedExceptionWhenUnknownStyle()
	{
		string input = "line1\nline2\r\nline3\r";
		Assert.ThrowsExactly<NotImplementedException>(() => input.NormalizeLineEndings((LineEndingStyle)999));
	}
}


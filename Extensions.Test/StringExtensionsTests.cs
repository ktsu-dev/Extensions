// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.Extensions.Tests;

using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

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

	// A CRLF followed by an LF, and a CR followed by a CRLF, are two line breaks each. Normalizing in
	// several passes used to merge the first into a new CRLF under Mac, losing a break and leaving a
	// Windows ending in Mac output.

	[TestMethod]
	[DataRow("a\r\n\nb", LineEndingStyle.Unix, "a\n\nb")]
	[DataRow("a\r\n\nb", LineEndingStyle.Windows, "a\r\n\r\nb")]
	[DataRow("a\r\n\nb", LineEndingStyle.Mac, "a\r\rb")]
	[DataRow("a\r\n\nb", LineEndingStyle.None, "ab")]
	[DataRow("a\r\n\nb", LineEndingStyle.Mixed, "a\n\nb")]
	[DataRow("a\r\r\nb", LineEndingStyle.Unix, "a\n\nb")]
	[DataRow("a\r\r\nb", LineEndingStyle.Windows, "a\r\n\r\nb")]
	[DataRow("a\r\r\nb", LineEndingStyle.Mac, "a\r\rb")]
	[DataRow("a\r\r\nb", LineEndingStyle.None, "ab")]
	[DataRow("a\r\r\nb", LineEndingStyle.Mixed, "a\n\nb")]
	public void NormalizeLineEndingsKeepsEveryBreakOfAdjacentMixedEndings(string input, LineEndingStyle style, string expected) =>
		Assert.AreEqual(expected, input.NormalizeLineEndings(style));

	[TestMethod]
	[DataRow(LineEndingStyle.Unix)]
	[DataRow(LineEndingStyle.Windows)]
	[DataRow(LineEndingStyle.Mac)]
	public void NormalizeLineEndingsOfAdjacentMixedEndingsIsDetectedAsTheTargetStyle(LineEndingStyle style)
	{
		Assert.AreEqual(style, "a\r\n\nb".NormalizeLineEndings(style).DetermineLineEndings());
		Assert.AreEqual(style, "a\r\r\nb".NormalizeLineEndings(style).DetermineLineEndings());
	}

	// A regex match timeout covers a whole Replace call, not each match, so any input that took more than the
	// timeout to scan threw RegexMatchTimeoutException. Normalizing must handle large input of every style.

	[TestMethod]
	[DataRow(LineEndingStyle.Unix, "ab\ncd\nef\n")]
	[DataRow(LineEndingStyle.Windows, "ab\r\ncd\r\nef\r\n")]
	[DataRow(LineEndingStyle.Mac, "ab\rcd\ref\r")]
	[DataRow(LineEndingStyle.None, "abcdef")]
	[DataRow(LineEndingStyle.Mixed, "ab\ncd\nef\n")]
	public void NormalizeLineEndingsHandlesLargeInput(LineEndingStyle style, string expectedUnit)
	{
		ArgumentNullException.ThrowIfNull(expectedUnit);
		const int repeats = 6_000_000;
		string input = new StringBuilder("ab\r\ncd\ref\n".Length * repeats).Insert(0, "ab\r\ncd\ref\n", repeats).ToString();

		string result = input.NormalizeLineEndings(style);

		Assert.AreEqual(expectedUnit.Length * repeats, result.Length);
		Assert.AreEqual(expectedUnit, result[..expectedUnit.Length]);
		Assert.AreEqual(expectedUnit, result[^expectedUnit.Length..]);
	}

	// Pins the fix above: no regex in StringExtensions may carry a finite timeout, so a later analyzer fix
	// (SonarCloud S6444) can't bring the whole-call timeout back.
	[TestMethod]
	public void StringExtensionsRegexesHaveNoFiniteMatchTimeout()
	{
		IEnumerable<PropertyInfo> regexProperties = typeof(StringExtensions)
			.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
			.Where(property => property.PropertyType == typeof(Regex));

		foreach (PropertyInfo property in regexProperties)
		{
			Regex regex = (Regex)property.GetValue(null)!;
			Assert.AreEqual(Regex.InfiniteMatchTimeout, regex.MatchTimeout, $"{property.Name} has a finite match timeout.");
		}
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

	// Tests for NominalWordWrap

	[TestMethod]
	public void NominalWordWrapWrapsOnWordBoundaries()
	{
		// wrapWidth 110 / glyph 10 => 11 chars per line.
		string input = "hello world foo";
		List<string> result = [.. input.NominalWordWrap(110f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "hello world", "foo" }, result);
	}

	[TestMethod]
	public void NominalWordWrapPutsEachWordOnItsOwnLineWhenNarrow()
	{
		// wrapWidth 50 / glyph 10 => 5 chars per line.
		string input = "hello world foo";
		List<string> result = [.. input.NominalWordWrap(50f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "hello", "world", "foo" }, result);
	}

	[TestMethod]
	public void NominalWordWrapCollapsesRunsOfWhitespace()
	{
		string input = "hello   \t  world";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "hello world" }, result);
	}

	[TestMethod]
	public void NominalWordWrapHonorsForcedLineBreaks()
	{
		string input = "hello\nworld";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "hello", "world" }, result);
	}

	[TestMethod]
	public void NominalWordWrapPreservesBlankLines()
	{
		string input = "hello\n\nworld";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "hello", "", "world" }, result);
	}

	[TestMethod]
	[DataRow('\v')]
	[DataRow('\f')]
	[DataRow('\u0085')]
	[DataRow('\u2028')]
	[DataRow('\u2029')]
	public void NominalWordWrapHonorsUnicodeMandatoryBreaks(char separator)
	{
		string input = "Line one" + separator + "Line two";
		List<string> result = [.. input.NominalWordWrap(80f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "Line one", "Line two" }, result);
	}

	[TestMethod]
	[DataRow('\v')]
	[DataRow('\f')]
	[DataRow('\u0085')]
	[DataRow('\u2028')]
	[DataRow('\u2029')]
	public void NominalWordWrapPreservesBlankLinesBetweenUnicodeMandatoryBreaks(char separator)
	{
		string input = "a" + separator + separator + "b";
		List<string> result = [.. input.NominalWordWrap(80f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "a", "", "b" }, result);
	}

	[TestMethod]
	public void NominalWordWrapHonorsWindowsLineBreaks()
	{
		string input = "hello\r\nworld";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "hello", "world" }, result);
	}

	[TestMethod]
	public void NominalWordWrapHardBreaksWordLongerThanLine()
	{
		// wrapWidth 50 / glyph 10 => 5 chars per line.
		string input = "abcdefgh";
		List<string> result = [.. input.NominalWordWrap(50f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "abcde", "fgh" }, result);
	}

	[TestMethod]
	public void NominalWordWrapPacksRemainderOfHardBrokenWordWithFollowingWords()
	{
		// wrapWidth 50 / glyph 10 => 5 chars per line.
		string input = "abcdefgh hi";
		List<string> result = [.. input.NominalWordWrap(50f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "abcde", "fgh", "hi" }, result);
	}

	[TestMethod]
	public void NominalWordWrapReturnsEmptySequenceForEmptyInput()
	{
		string input = "";
		List<string> result = [.. input.NominalWordWrap(100f, 10f)];
		Assert.AreEqual(0, result.Count);
	}

	[TestMethod]
	public void NominalWordWrapClampsToAtLeastOneCharacterPerLine()
	{
		// wrapWidth 5 / glyph 10 => 0, clamped to 1 char per line.
		string input = "ab";
		List<string> result = [.. input.NominalWordWrap(5f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "a", "b" }, result);
	}

	[TestMethod]
	[DataRow(0.1f)]
	[DataRow(0.7f)]
	[DataRow(6.6f)]
	[DataRow(7.2f)]
	[DataRow(13.3f)]
	public void NominalWordWrapFitsExactlyNGlyphsInAWidthOfNGlyphs(float nominalGlyphWidth)
	{
		string input = new('a', 201);

		for (int n = 1; n <= 200; n++)
		{
			string firstLine = input.NominalWordWrap(n * nominalGlyphWidth, nominalGlyphWidth).First();
			Assert.AreEqual(n, firstLine.Length, $"A width of {n} glyphs of {nominalGlyphWidth} should hold {n} characters.");
		}
	}

	[TestMethod]
	[DataRow(1_000_000)]
	[DataRow(2_000_000)]
	[DataRow(10_000_000)]
	public void NominalWordWrapFitsExactlyNGlyphsInAVeryWideWidth(int n)
	{
		string input = new('a', n + 20);

		string firstLine = input.NominalWordWrap(n, 1f).First();
		Assert.AreEqual(n, firstLine.Length, $"A width of {n} glyphs should hold {n} characters, not more.");
	}

	[TestMethod]
	[DataRow(1_000_000)]
	[DataRow(2_000_000)]
	[DataRow(4_000_000)]
	public void NominalWordWrapDoesNotRoundUpAVeryWideWidthHalfAGlyphShort(int n)
	{
		string input = new('a', n + 20);

		string firstLine = input.NominalWordWrap(n - 0.5f, 1f).First();
		Assert.AreEqual(n - 1, firstLine.Length, $"A width of {n - 0.5f} glyphs should hold {n - 1} characters.");
	}

	[TestMethod]
	[DataRow(0.7f, 50_000)]
	[DataRow(0.7f, 100_000)]
	[DataRow(6.6f, 200_000)]
	[DataRow(13.3f, 100_000)]
	public void NominalWordWrapStillAbsorbsFloatErrorAtLargeInexactWidths(float nominalGlyphWidth, int n)
	{
		string input = new('a', n + 20);

		string firstLine = input.NominalWordWrap(n * nominalGlyphWidth, nominalGlyphWidth).First();
		Assert.AreEqual(n, firstLine.Length, $"A width of {n} glyphs of {nominalGlyphWidth} should hold {n} characters.");
	}

	[TestMethod]
	public void NominalWordWrapKeepsAWordThatExactlyFillsTheWidthOnOneLine()
	{
		List<string> result = [.. "hello world".NominalWordWrap(11 * 0.7f, 0.7f)];
		Assert.AreSequenceEqual(new List<string> { "hello world" }, result);
	}

	[TestMethod]
	[DataRow(0.7f)]
	[DataRow(6.6f)]
	public void NominalWordWrapDoesNotRoundUpAWidthHalfAGlyphShort(float nominalGlyphWidth)
	{
		string input = new('a', 201);

		for (int n = 1; n < 200; n++)
		{
			string firstLine = input.NominalWordWrap((n + 0.5f) * nominalGlyphWidth, nominalGlyphWidth).First();
			Assert.AreEqual(n, firstLine.Length, $"A width of {n + 0.5f} glyphs of {nominalGlyphWidth} should hold {n} characters.");
		}
	}

	[TestMethod]
	public void NominalWordWrapHardBreakDoesNotSplitSurrogatePairs()
	{
		// Each 😀 is a surrogate pair: two UTF-16 code units.
		List<string> emoji = [.. "😀😀😀".NominalWordWrap(3f, 1f)];
		Assert.AreSequenceEqual(new List<string> { "😀", "😀", "😀" }, emoji);

		List<string> mixed = [.. "ab😀cd".NominalWordWrap(3f, 1f)];
		Assert.AreSequenceEqual(new List<string> { "ab", "😀c", "d" }, mixed);
	}

	[TestMethod]
	public void NominalWordWrapLetsASurrogatePairOverhangAtWidthOne()
	{
		List<string> result = [.. "a😀b".NominalWordWrap(1f, 1f)];
		Assert.AreSequenceEqual(new List<string> { "a", "😀", "b" }, result);
	}

	[TestMethod]
	public void NominalWordWrapThrowsArgumentNullExceptionWhenTextIsNull()
	{
		string input = null!;
		Assert.ThrowsExactly<ArgumentNullException>(() => input.NominalWordWrap(100f, 10f).ToList());
	}

	[TestMethod]
	public void NominalWordWrapThrowsArgumentOutOfRangeExceptionWhenWrapWidthNotPositive()
	{
		string input = "hello";
		Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => input.NominalWordWrap(0f, 10f).ToList());
	}

	[TestMethod]
	public void NominalWordWrapThrowsArgumentOutOfRangeExceptionWhenGlyphWidthNotPositive()
	{
		string input = "hello";
		Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => input.NominalWordWrap(100f, 0f).ToList());
	}

	[TestMethod]
	public void NominalWordWrapThrowsArgumentOutOfRangeExceptionWhenWrapWidthIsNaN()
	{
		string input = "hello world foo";
		ArgumentOutOfRangeException exception = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => input.NominalWordWrap(float.NaN, 1f).ToList());
		Assert.AreEqual("wrapWidth", exception.ParamName);
	}

	[TestMethod]
	public void NominalWordWrapThrowsArgumentOutOfRangeExceptionWhenGlyphWidthIsNaN()
	{
		string input = "hello world foo";
		ArgumentOutOfRangeException exception = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => input.NominalWordWrap(10f, float.NaN).ToList());
		Assert.AreEqual("nominalGlyphWidth", exception.ParamName);
	}

	[TestMethod]
	[DataRow(float.PositiveInfinity, 1f)]
	[DataRow(float.MaxValue, float.Epsilon)]
	public void NominalWordWrapKeepsEachLineWholeWhenWidthRatioIsUnbounded(float wrapWidth, float nominalGlyphWidth)
	{
		string input = "hello world foo\nbar baz";
		List<string> result = [.. input.NominalWordWrap(wrapWidth, nominalGlyphWidth)];
		Assert.AreSequenceEqual(new List<string> { "hello world foo", "bar baz" }, result);
	}

	[TestMethod]
	public void NominalWordWrapBreaksAfterVisibleHyphen()
	{
		// wrapWidth 50 / glyph 10 => 5 chars per line.
		string input = "foo-bar-baz";
		List<string> result = [.. input.NominalWordWrap(50f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "foo-", "bar-", "baz" }, result);
	}

	[TestMethod]
	public void NominalWordWrapKeepsVisibleHyphenWhenNotBroken()
	{
		string input = "foo-bar";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "foo-bar" }, result);
	}

	[TestMethod]
	public void NominalWordWrapPrefersHyphenBoundaryOverHardBreak()
	{
		// wrapWidth 80 / glyph 10 => 8 chars per line; each hyphen chunk fits, so no hard break.
		string input = "foobar-bazqux";
		List<string> result = [.. input.NominalWordWrap(80f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "foobar-", "bazqux" }, result);
	}

	[TestMethod]
	[DataRow("self-explanatory", 4f, new[] { "self", "-exp", "lana", "tory" })]
	[DataRow("abcde-fghij", 5f, new[] { "abcde", "-fghi", "j" })]
	[DataRow("well-known e-mail", 4f, new[] { "well", "-kno", "wn", "e-", "mail" })]
	[DataRow("self- and", 4f, new[] { "sel", "f-", "and" })]
	[DataRow("wait-- what", 4f, new[] { "wai", "t--", "what" })]
	[DataRow("anti-", 4f, new[] { "ant", "i-" })]
	[DataRow("go well-", 4f, new[] { "go", "wel", "l-" })]
	[DataRow("abcde-", 5f, new[] { "abcd", "e-" })]
	[DataRow("pre- and post-war", 3f, new[] { "pr", "e-", "and", "pos", "t-", "war" })]
	[DataRow("x\U0001F600- y", 3f, new[] { "x", "\U0001F600-", "y" })]
	[DataRow("wait-- then", 5f, new[] { "wai", "t--", "then" })]
	[DataRow("wait--", 5f, new[] { "wai", "t--" })]
	[DataRow("late--too", 5f, new[] { "late", "--too" })]
	[DataRow("well--known fact", 5f, new[] { "well", "--kno", "wn", "fact" })]
	public void NominalWordWrapNeverLeavesALineOfOnlyHyphens(string input, float maxChars, string[] expected)
	{
		// A hard break that cuts only the trailing hyphen off a chunk keeps the next chunk of the word on the hyphen's line (#182).
		List<string> result = [.. input.NominalWordWrap(maxChars, 1f)];
		CollectionAssert.AreEqual(expected, result);
		Assert.IsFalse(result.Any(line => line.Length > 0 && line.Trim('-').Length == 0), "No line should hold only hyphens.");
		Assert.IsFalse(result.Any(line => line.Length > maxChars), "No line should be wider than the width.");
	}

	[TestMethod]
	public void NominalWordWrapNeverSplitsADoubleHyphenDash()
	{
		// Put a "--" dash after words of every length, so the hard break lands before, inside and after the run (#203).
		string[] tails = ["", " what", "too", " then more"];
		for (int width = 3; width <= 9; width++)
		{
			for (int wordLength = 1; wordLength <= 12; wordLength++)
			{
				foreach (string input in tails.Select(tail => new string('a', wordLength) + "--" + tail))
				{
					List<string> result = [.. input.NominalWordWrap(width, 1f)];
					foreach (string line in result)
					{
						Assert.IsFalse(line.StartsWith('-') && !line.StartsWith("--", StringComparison.Ordinal), $"{input} at width {width} split its dash: [{string.Join(" | ", result)}]");
						Assert.IsFalse(line.EndsWith('-') && !line.EndsWith("--", StringComparison.Ordinal), $"{input} at width {width} split its dash: [{string.Join(" | ", result)}]");
						Assert.IsFalse(line.Length > 0 && line.Trim('-').Length == 0, $"{input} at width {width} left a line of only hyphens: [{string.Join(" | ", result)}]");
						Assert.IsLessThanOrEqualTo(width, line.Length, $"{input} at width {width}: [{string.Join(" | ", result)}]");
					}

					Assert.AreEqual(input.Replace(" ", string.Empty, StringComparison.Ordinal), string.Concat(result).Replace(" ", string.Empty, StringComparison.Ordinal), $"{input} at width {width} lost content: [{string.Join(" | ", result)}]");
				}
			}
		}
	}

	[TestMethod]
	public void NominalWordWrapAtWidthOneKeepsTheHyphenOnItsOwnLine()
	{
		List<string> result = [.. "x-ray".NominalWordWrap(1f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "x", "-", "r", "a", "y" }, result);
	}

	[TestMethod]
	public void NominalWordWrapDoesNotBreakOnUnderscores()
	{
		// wrapWidth 50 / glyph 10 => 5 chars per line; underscores are not break points, so the word is hard-broken.
		string input = "foo_bar_baz";
		List<string> result = [.. input.NominalWordWrap(50f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "foo_b", "ar_ba", "z" }, result);
	}

	[TestMethod]
	public void NominalWordWrapBreaksAtZeroWidthSpace()
	{
		// wrapWidth 7 / glyph 1 => 7 chars per line; the zero-width space is the only break point in the first word.
		string input = "alpha\u200Bbeta gamma";
		List<string> result = [.. input.NominalWordWrap(7, 1)];
		CollectionAssert.AreEqual(new List<string> { "alpha", "beta", "gamma" }, result);
	}

	[TestMethod]
	public void NominalWordWrapBreaksAMarkedUrlOnlyAtItsZeroWidthSpaces()
	{
		string input = "see https://\u200Bexample.com/\u200Ba/\u200Bvery/\u200Blong/\u200Bpath now";
		List<string> result = [.. input.NominalWordWrap(12, 1)];
		CollectionAssert.AreEqual(new List<string> { "see https://", "example.com/", "a/very/long/", "path now" }, result);
	}

	[TestMethod]
	public void NominalWordWrapStripsZeroWidthSpaceWhenNotBroken()
	{
		string input = "\u200Bfoo\u200B\u200Bbar\u200B baz";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "foobar baz" }, result);
	}

	[TestMethod]
	public void NominalWordWrapGivesZeroWidthSpaceNoWidth()
	{
		// Six visible glyphs fit a six-character line even with zero-width spaces between them.
		string input = "ab\u200Bcd\u200Bef";
		List<string> result = [.. input.NominalWordWrap(6, 1)];
		CollectionAssert.AreEqual(new List<string> { "abcdef" }, result);
	}

	// A zero-width space starts a new segment, so a dash right after it is word-initial and never a break point,
	// just as after a space: "-5" and "--force" stay attached to their sign (#186).
	[TestMethod]
	[DataRow("a\u200B-5", 2f, new[] { "a", "-5" })]
	[DataRow("value\u200B-1", 6f, new[] { "value", "-1" })]
	[DataRow("x\u200B--force", 3f, new[] { "x", "--f", "orc", "e" })]
	public void NominalWordWrapKeepsADashAfterAZeroWidthSpaceWordInitial(string input, float width, string[] expected)
	{
		List<string> result = [.. input.NominalWordWrap(width, 1f)];
		CollectionAssert.AreEqual(expected, result);
	}

	[TestMethod]
	public void NominalWordWrapStripsSoftHyphenWhenNotBroken()
	{
		string input = "foo­bar";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "foobar" }, result);
	}

	[TestMethod]
	public void NominalWordWrapRendersSoftHyphenWhenBroken()
	{
		// wrapWidth 60 / glyph 10 => 6 chars per line; break lands at the soft hyphen.
		string input = "AAAAA­BBBBB";
		List<string> result = [.. input.NominalWordWrap(60f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "AAAAA-", "BBBBB" }, result);
	}

	[TestMethod]
	public void NominalWordWrapRendersSoftHyphenOnALineItFillsExactly()
	{
		List<string> result = [.. "AAAA\u00ADBBBBB".NominalWordWrap(5f, 1f)];
		Assert.AreSequenceEqual(new List<string> { "AAAA-", "BBBBB" }, result);
	}

	[TestMethod]
	public void NominalWordWrapBreaksWithoutTheHyphenWhenAFullFirstLineLeavesNoRoomForIt()
	{
		// The upper line is already full at the only break opportunity, so the hyphen would make it 6 characters.
		List<string> result = [.. "AAAAA\u00ADBBBBB".NominalWordWrap(5f, 1f)];
		Assert.AreSequenceEqual(new List<string> { "AAAAA", "BBBBB" }, result);
	}

	[TestMethod]
	public void NominalWordWrapCarriesAWordDownWhenItsSoftHyphenWouldOverflowAFullLine()
	{
		// "xx abc-" would be 7 characters, so the break falls back to the space before "abc".
		List<string> result = [.. "xx abc\u00ADde".NominalWordWrap(6f, 1f)];
		Assert.AreSequenceEqual(new List<string> { "xx", "abcde" }, result);
	}

	[TestMethod]
	public void NominalWordWrapFallsBackToAnEarlierSoftHyphenThatLeavesRoom()
	{
		// "abcde" is full when "fg" arrives, so the break moves back to the soft hyphen after "abc".
		List<string> result = [.. "abc\u00ADde\u00ADfg".NominalWordWrap(5f, 1f)];
		Assert.AreSequenceEqual(new List<string> { "abc-", "defg" }, result);
	}

	[TestMethod]
	public void NominalWordWrapNeverExceedsTheWidthAtASoftHyphen()
	{
		string[] inputs = ["AAAAA\u00ADBBBBB", "xx abc\u00ADde", "tr\u00ADans\u00ADla\u00ADtion", "in\u00ADter\u00ADna\u00ADtion\u00ADal\u00ADi\u00ADza\u00ADtion is a long word"];
		foreach (string input in inputs)
		{
			for (int width = 1; width <= 12; width++)
			{
				List<string> result = [.. input.NominalWordWrap(width, 1f)];
				foreach (string line in result)
				{
					Assert.IsLessThanOrEqualTo(width, line.Length, $"\"{line}\" from \"{input}\" is longer than {width}.");
				}

				Assert.AreEqual(input.Replace("\u00AD", string.Empty, StringComparison.Ordinal).Replace(" ", string.Empty, StringComparison.Ordinal), string.Concat(result).Replace("-", string.Empty, StringComparison.Ordinal).Replace(" ", string.Empty, StringComparison.Ordinal), $"\"{input}\" at width {width} lost or gained text.");
			}
		}
	}

	[TestMethod]
	public void NominalWordWrapDoesNotDoubleAVisibleHyphenFollowedByASoftHyphen()
	{
		// 5 chars per line; the break lands after "well-", where a soft hyphen follows the visible one.
		string input = "well-\u00ADknown";
		List<string> result = [.. input.NominalWordWrap(5f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "well-", "known" }, result);
	}

	[TestMethod]
	public void NominalWordWrapDoesNotDoubleAVisibleHyphenFollowedBySeveralSoftHyphens()
	{
		string input = "well-\u00AD\u00ADknown";
		List<string> result = [.. input.NominalWordWrap(5f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "well-", "known" }, result);
	}

	[TestMethod]
	public void NominalWordWrapKeepsAVisibleHyphenFollowedByASoftHyphenOnOneLineWhenItFits()
	{
		string input = "well-\u00ADknown";
		List<string> result = [.. input.NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "well-known" }, result);
	}

	[TestMethod]
	public void NominalWordWrapDoesNotDoubleASoftHyphenFollowedByAVisibleHyphen()
	{
		// 6 chars per line; the break lands where a soft hyphen comes before the visible one (mirror of the case above).
		List<string> result = [.. "a well­-known".NominalWordWrap(6f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "a", "well-", "known" }, result);
	}

	[TestMethod]
	public void NominalWordWrapKeepsAHyphenRunAfterASoftHyphenTogether()
	{
		List<string> result = [.. "well­--known".NominalWordWrap(6f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "well--", "known" }, result);
	}

	[TestMethod]
	public void NominalWordWrapDropsASoftHyphenBeforeAVisibleHyphenWhenItFits()
	{
		List<string> result = [.. "well­-known".NominalWordWrap(1000f, 10f)];
		CollectionAssert.AreEqual(new List<string> { "well-known" }, result);
	}

	[TestMethod]
	public void NominalWordWrapKeepsADoubleHyphenDashTogether()
	{
		// 12 chars per line; "It was late-" would fit, but the break must not fall between the two hyphens.
		string input = "It was late--too late to call";
		List<string> result = [.. input.NominalWordWrap(12f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "It was", "late--too", "late to call" }, result);
	}

	[TestMethod]
	public void NominalWordWrapBreaksAfterTheLastHyphenOfARun()
	{
		string input = "It was late--too";
		List<string> result = [.. input.NominalWordWrap(13f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "It was late--", "too" }, result);
	}

	[TestMethod]
	public void NominalWordWrapDoesNotBreakAfterAWordInitialHyphen()
	{
		List<string> number = [.. "ab -5".NominalWordWrap(4f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "ab", "-5" }, number);

		List<string> flag = [.. "Use the --force flag".NominalWordWrap(10f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "Use the", "--force", "flag" }, flag);
	}

	[TestMethod]
	public void NominalWordWrapStillBreaksAfterAHyphenThatFollowsALeadingOne()
	{
		// "-5-6": the second hyphen has a digit before it, so it is still a break opportunity.
		List<string> result = [.. "-5-6".NominalWordWrap(3f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "-5-", "6" }, result);
	}

	[TestMethod]
	public void NominalWordWrapDoesNotAddAHyphenAtASoftHyphenAfterALeadingDash()
	{
		// The soft hyphen after "--" is a break opportunity, but the visible dash already ends the upper line.
		List<string> result = [.. "ab --\u00ADforce".NominalWordWrap(5f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "ab --", "force" }, result);
	}

	[TestMethod]
	public void NominalWordWrapDoesNotBreakAtANoBreakSpace()
	{
		// 10 chars per line; "Total: 100 kg" would break before "kg", but U+00A0 keeps the number with its unit.
		List<string> result = [.. "Total: 100\u00A0kg".NominalWordWrap(10f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "Total:", "100\u00A0kg" }, result);
	}

	[TestMethod]
	public void NominalWordWrapKeepsNoBreakSpacesUnchanged()
	{
		List<string> result = [.. "a\u00A0b c\u2007d e\u202Ff".NominalWordWrap(100f, 1f)];
		CollectionAssert.AreEqual(new List<string> { "a\u00A0b c\u2007d e\u202Ff" }, result);
	}

	[TestMethod]
	public void NominalWordWrapHardBreaksALongTokenInLinearTime()
	{
		// One unbroken token, such as a base64 blob, cut into many narrow lines
		string token = new('a', 1_000_000);

		System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
		List<string> result = [.. token.NominalWordWrap(10f, 1f)];
		stopwatch.Stop();

		Assert.HasCount(100_000, result);
		Assert.IsTrue(result.TrueForAll(line => line == "aaaaaaaaaa"));
		Assert.IsLessThan(1_000, stopwatch.ElapsedMilliseconds);
	}
}

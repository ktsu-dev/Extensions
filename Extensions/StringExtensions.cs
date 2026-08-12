// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.Extensions;

using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Extension methods for strings.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "Not available in all frameworks.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "Not available in netstandard2.0")]
public static class StringExtensions
{
	/// <summary>
	/// Method that compares two strings using ordinal comparison.
	/// </summary>
	/// <param name="str">The string to compare.</param>
	/// <param name="value">The <paramref name="value"/> to compare to.</param>
	/// <returns>true if str starts with value; otherwise, false.</returns>
	public static bool StartsWithOrdinal(this string str, string value)
	{
		Ensure.NotNull(str);
		Ensure.NotNull(value);

		return str.StartsWith(value, StringComparison.Ordinal);
	}

	/// <summary>
	/// Method that compares two strings using ordinal comparison.
	/// </summary>
	/// <param name="str">The string to compare.</param>
	/// <param name="value">The <paramref name="value"/> to compare to.</param>
	/// <returns>true if str ends with value; otherwise, false.</returns>
	public static bool EndsWithOrdinal(this string str, string value)
	{
		Ensure.NotNull(str);
		Ensure.NotNull(value);

		return str.EndsWith(value, StringComparison.Ordinal);
	}

	/// <summary>
	/// Method that compares two strings using ordinal comparison.
	/// </summary>
	/// <param name="str">The string to compare.</param>
	/// <param name="value">The <paramref name="value"/> to compare to.</param>
	/// <returns>true if str contains value; otherwise, false.</returns>
	public static bool ContainsOrdinal(this string str, string value)
	{
		Ensure.NotNull(str);
		Ensure.NotNull(value);

#if NETSTANDARD2_0
		return str.Contains(value);
#else
		return str.Contains(value, StringComparison.Ordinal);
#endif
	}

	/// <summary>
	/// Removes the specified <paramref name="suffix"/> from the current string.
	/// </summary>
	/// <param name="s">The string to remove the suffix from.</param>
	/// <param name="suffix">The suffix to remove.</param>
	/// <returns>The string with the suffix removed.</returns>
	public static string RemoveSuffix(this string s, string suffix)
	{
		Ensure.NotNull(s);
		Ensure.NotNull(suffix);

		if (s.Length == 0 || suffix.Length == 0)
		{
			return s;
		}

		int suffixIndex = s.Length - suffix.Length;
		return s.EndsWithOrdinal(suffix) ? s.Substring(0, suffixIndex) : s;
	}

	/// <summary>
	/// Removes the specified <paramref name="prefix"/> from the current string.
	/// </summary>
	/// <param name="s">The string to remove the prefix from.</param>
	/// <param name="prefix">The prefix to remove.</param>
	/// <returns>The string with the prefix removed.</returns>
	public static string RemovePrefix(this string s, string prefix)
	{
		Ensure.NotNull(s);
		Ensure.NotNull(prefix);

		if (s.Length == 0 || prefix.Length == 0)
		{
			return s;
		}

		return s.StartsWithOrdinal(prefix) ? s.Substring(prefix.Length) : s;
	}

	/// <summary>
	/// Replace all occurrences of a string with another string using ordinal comparison.
	/// </summary>
	/// <param name="s">The string to search in.</param>
	/// <param name="oldValue">The string to replace.</param>
	/// <param name="newValue">The string to replace with.</param>
	/// <returns></returns>
	public static string ReplaceOrdinal(this string s, string oldValue, string newValue)
	{
		Ensure.NotNull(s);
		Ensure.NotNull(oldValue);
		Ensure.NotNull(newValue);

		if (s.Length == 0 || oldValue.Length == 0)
		{
			return s;
		}

#if NETSTANDARD2_0
		return s.Replace(oldValue, newValue);
#else
		return s.Replace(oldValue, newValue, StringComparison.Ordinal);
#endif
	}

	private static Regex LineEndingRegexUnix { get; } = new(@"(?<!\r)\n", RegexOptions.Compiled);
	private static Regex LineEndingRegexWindows { get; } = new(@"\r\n", RegexOptions.Compiled);
	private static Regex LineEndingRegexMac { get; } = new(@"\r(?!\n)", RegexOptions.Compiled);

	/// <summary>
	/// Determines the line ending style of the specified string.
	/// </summary>
	/// <param name="input">The string to analyze.</param>
	/// <returns>
	/// A <see cref="LineEndingStyle"/> value indicating the type of line endings used in the string.
	/// Returns <see cref="LineEndingStyle.Mixed"/> if multiple types of line endings are found.
	/// </returns>
	/// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Terneries here wouldnt be great")]
	public static LineEndingStyle DetermineLineEndings(this string input)
	{
		Ensure.NotNull(input);

		if (input.Length == 0)
		{
			return LineEndingStyle.None;
		}

		bool hasUnix = LineEndingRegexUnix.IsMatch(input);
		bool hasWindows = LineEndingRegexWindows.IsMatch(input);
		bool hasMac = LineEndingRegexMac.IsMatch(input);

		if (hasUnix && hasWindows && hasMac)
		{
			return LineEndingStyle.Mixed;
		}
		else if (hasUnix && hasWindows)
		{
			return LineEndingStyle.Mixed;
		}
		else if (hasUnix && hasMac)
		{
			return LineEndingStyle.Mixed;
		}
		else if (hasWindows && hasMac)
		{
			return LineEndingStyle.Mixed;
		}
		else if (hasUnix)
		{
			return LineEndingStyle.Unix;
		}
		else if (hasWindows)
		{
			return LineEndingStyle.Windows;
		}
		else if (hasMac)
		{
			return LineEndingStyle.Mac;
		}
		else
		{
			return LineEndingStyle.None;
		}
	}

	/// <summary>
	/// Normalizes the line endings in the specified string to the specified style.
	/// </summary>
	/// <param name="s">The string to normalize.</param>
	/// <param name="style">The style of line endings to normalize to.</param>
	/// <returns>The string with normalized line endings.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
	/// <exception cref="NotImplementedException">Thrown when an unknown line ending style is specified.</exception>
	public static string NormalizeLineEndings(this string s, LineEndingStyle style)
	{
		Ensure.NotNull(s);

		if (s.Length == 0)
		{
			return s;
		}

		return style switch
		{
			LineEndingStyle.None => LineEndingRegexUnix.Replace(LineEndingRegexWindows.Replace(LineEndingRegexMac.Replace(s, ""), ""), ""),
			LineEndingStyle.Unix => LineEndingRegexWindows.Replace(LineEndingRegexMac.Replace(s, "\n"), "\n"),
			LineEndingStyle.Windows => LineEndingRegexUnix.Replace(LineEndingRegexMac.Replace(s, "\r\n"), "\r\n"),
			LineEndingStyle.Mac => LineEndingRegexUnix.Replace(LineEndingRegexWindows.Replace(s, "\r"), "\r"),
			LineEndingStyle.Mixed => LineEndingRegexWindows.Replace(LineEndingRegexMac.Replace(s, "\n"), "\n"),
			_ => throw new NotImplementedException("Unknown line ending style."),
		};
	}

	/// <summary>
	/// Performs a best-effort word wrap on word boundaries, given a wrap width and a nominal glyph width.
	/// </summary>
	/// <param name="text">The text to wrap.</param>
	/// <param name="wrapWidth">The width to wrap at, in the same units as <paramref name="nominalGlyphWidth"/> (for example, pixels).</param>
	/// <param name="nominalGlyphWidth">The nominal (assumed uniform) width of a single glyph, in the same units as <paramref name="wrapWidth"/>.</param>
	/// <returns>
	/// The wrapped lines. Existing line breaks (Unix, Windows, or Mac) are honored as forced breaks and blank lines are
	/// preserved. Runs of whitespace within a line are collapsed to a single space. A break may also occur after a visible
	/// hyphen (the hyphen stays on the upper line) or at a soft hyphen (<c>­</c>), which renders as a hyphen only when
	/// a break lands there and is otherwise removed. Words that still cannot fit are hard-broken as a last resort so that
	/// no line exceeds the computed width, except that honoring a soft hyphen at a line boundary may add a single
	/// overhanging character.
	/// </returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="wrapWidth"/> or <paramref name="nominalGlyphWidth"/> is not greater than zero.</exception>
	public static IEnumerable<string> NominalWordWrap(this string text, float wrapWidth, float nominalGlyphWidth)
	{
		Ensure.NotNull(text);

		if (wrapWidth <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(wrapWidth), wrapWidth, "Wrap width must be greater than zero.");
		}

		if (nominalGlyphWidth <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(nominalGlyphWidth), nominalGlyphWidth, "Nominal glyph width must be greater than zero.");
		}

		if (text.Length == 0)
		{
			return [];
		}

		int maxCharsPerLine = Math.Max(1, (int)Math.Floor(wrapWidth / nominalGlyphWidth));
		return NominalWordWrapIterator(text, maxCharsPerLine);
	}

	/// <summary>
	/// The Unicode soft hyphen (U+00AD): an invisible break opportunity that renders as a hyphen only when a break lands there.
	/// </summary>
	private const char SoftHyphen = '­';

	private static IEnumerable<string> NominalWordWrapIterator(string text, int maxCharsPerLine)
	{
		string normalized = text.NormalizeLineEndings(LineEndingStyle.Unix);
		string[] segments = normalized.Split('\n');

		foreach (string segment in segments)
		{
			// A flat list of break-units for the segment: (text, precededBySpace, hyphenWhenBrokenBefore, canBreakBefore).
			List<(string Text, bool GlueSpace, bool HyphenBefore, bool CanBreak)> atoms = BuildAtoms(segment);
			if (atoms.Count == 0)
			{
				// Preserve blank/forced-break lines.
				yield return string.Empty;
				continue;
			}

			StringBuilder line = new();
			foreach ((string atomText, bool glueSpace, bool hyphenBefore, bool canBreak) in atoms)
			{
				bool atLineStart = line.Length == 0;
				int separatorLength = (!atLineStart && glueSpace) ? 1 : 0;
				int projectedLength = line.Length + separatorLength + atomText.Length;

				if (!atLineStart && projectedLength > maxCharsPerLine && canBreak)
				{
					// Break before this atom. A soft-hyphen boundary renders the hyphen on the upper line.
					if (hyphenBefore)
					{
						line.Append('-');
					}

					yield return line.ToString();
					line.Clear();
					atLineStart = true;
				}
				else if (!atLineStart)
				{
					if (glueSpace)
					{
						line.Append(' ');
					}

					line.Append(atomText);
					continue;
				}

				// At the start of a fresh line: place the atom, hard-breaking it if it still cannot fit.
				string remaining = atomText;
				while (remaining.Length > maxCharsPerLine)
				{
					yield return remaining.Substring(0, maxCharsPerLine);
					remaining = remaining.Substring(maxCharsPerLine);
				}

				line.Append(remaining);
			}

			if (line.Length > 0)
			{
				yield return line.ToString();
			}
		}
	}

	private static List<(string Text, bool GlueSpace, bool HyphenBefore, bool CanBreak)> BuildAtoms(string segment)
	{
		List<(string Text, bool GlueSpace, bool HyphenBefore, bool CanBreak)> atoms = [];
		string[] words = segment.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);

		foreach (string word in words)
		{
			List<(string Text, bool HyphenBefore)> chunks = ChunkWord(word);
			for (int c = 0; c < chunks.Count; c++)
			{
				bool isFirstAtomOverall = atoms.Count == 0;
				bool isWordStart = c == 0;

				// (Text, GlueSpace, HyphenBefore, CanBreak) — positional to satisfy the target tuple element names.
				atoms.Add((
					chunks[c].Text,
					isWordStart && !isFirstAtomOverall,
					chunks[c].HyphenBefore,
					!isFirstAtomOverall));
			}
		}

		return atoms;
	}

	private static List<(string Text, bool HyphenBefore)> ChunkWord(string word)
	{
		// Split a whitespace-delimited word into break-units at hyphenation opportunities:
		// after a visible hyphen (kept on the left chunk) or at a soft hyphen (removed unless a break lands there).
		List<(string Text, bool HyphenBefore)> chunks = [];
		StringBuilder current = new();
		bool hyphenBefore = false;

		foreach (char ch in word)
		{
			if (ch == SoftHyphen)
			{
				if (current.Length > 0)
				{
					chunks.Add((current.ToString(), hyphenBefore));
					current.Clear();
					hyphenBefore = true;
				}
				else if (chunks.Count > 0)
				{
					// Consecutive soft hyphens: keep marking the next chunk as hyphenated when broken.
					hyphenBefore = true;
				}

				continue;
			}

			current.Append(ch);
			if (ch == '-')
			{
				chunks.Add((current.ToString(), hyphenBefore));
				current.Clear();
				hyphenBefore = false;
			}
		}

		if (current.Length > 0)
		{
			chunks.Add((current.ToString(), hyphenBefore));
		}

		return chunks;
	}
}

/// <summary>
/// Specifies the different styles of line endings.
/// </summary>
/// <remarks>
/// This enumeration is used to identify and normalize line endings in strings.
/// </remarks>
public enum LineEndingStyle
{
	/// <summary>
	/// No line endings.
	/// </summary>
	None,
	/// <summary>
	/// Unix-style line endings (\n).
	/// </summary>
	Unix,
	/// <summary>
	/// Windows-style line endings (\r\n).
	/// </summary>
	Windows,
	/// <summary>
	/// Mac-style line endings (\r).
	/// </summary>
	Mac,
	/// <summary>
	/// Mixed line endings.
	/// </summary>
	Mixed,
}

// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Extensions;

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

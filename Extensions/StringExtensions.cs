namespace ktsu.Extensions;

using System.Text.RegularExpressions;

using ktsu.StrongStrings;

/// <summary>
/// Extension methods for strings.
/// </summary>
public static partial class StringExtensions
{
	/// <summary>
	/// Converts a weak string to a strong string of the specified type.
	/// </summary>
	/// <typeparam name="TDest">The type of the strong string.</typeparam>
	/// <param name="weakString">The weak string to convert.</param>
	/// <returns>The converted strong string.</returns>
	public static TDest As<TDest>(this string weakString)
		where TDest : AnyStrongString
		=> AnyStrongString.FromString<TDest>(weakString);

	/// <summary>
	/// Method that compares two strings using ordinal comparison.
	/// </summary>
	/// <param name="str">The string to compare.</param>
	/// <param name="value">The <paramref name="value"/> to compare to.</param>
	/// <returns>true if str starts with value; otherwise, false.</returns>
	public static bool StartsWithOrdinal(this string str, string value)
	{
		ArgumentNullException.ThrowIfNull(str);
		ArgumentNullException.ThrowIfNull(value);
		return str.StartsWith(value, StringComparison.Ordinal);
	}

	/// <summary>
	/// Method that compares two strings using ordinal comparison.
	/// </summary>
	/// <param name="str">The string to compare.</param>
	/// <param name="value">The <paramref name="value"/> to compare to.</param>
	/// <returns>true if str starts with value; otherwise, false.</returns>
	public static bool StartsWithOrdinal<TDerived>(this TDerived str, string value) where TDerived : AnyStrongString<TDerived>
	{
		ArgumentNullException.ThrowIfNull(str);
		ArgumentNullException.ThrowIfNull(value);
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
		ArgumentNullException.ThrowIfNull(str);
		ArgumentNullException.ThrowIfNull(value);
		return str.EndsWith(value, StringComparison.Ordinal);
	}

	/// <summary>
	/// Method that compares two strings using ordinal comparison.
	/// </summary>
	/// <param name="str">The string to compare.</param>
	/// <param name="value">The <paramref name="value"/> to compare to.</param>
	/// <returns>true if str ends with value; otherwise, false.</returns>
	public static bool EndsWithOrdinal<TDerived>(this TDerived str, string value) where TDerived : AnyStrongString<TDerived>
	{
		ArgumentNullException.ThrowIfNull(str);
		ArgumentNullException.ThrowIfNull(value);
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
		ArgumentNullException.ThrowIfNull(str);
		ArgumentNullException.ThrowIfNull(value);
		return str.Contains(value, StringComparison.Ordinal);
	}

	/// <summary>
	/// Method that compares two strings using ordinal comparison.
	/// </summary>
	/// <param name="str">The string to compare.</param>
	/// <param name="value">The <paramref name="value"/> to compare to.</param>
	/// <returns>true if str contains value; otherwise, false.</returns>
	public static bool ContainsOrdinal<TDerived>(this TDerived str, string value) where TDerived : AnyStrongString<TDerived>
	{
		ArgumentNullException.ThrowIfNull(str);
		ArgumentNullException.ThrowIfNull(value);
		return str.Contains(value, StringComparison.Ordinal);
	}

	/// <summary>
	/// Removes the specified <paramref name="suffix"/> from the current string.
	/// </summary>
	/// <param name="s">The string to remove the suffix from.</param>
	/// <param name="suffix">The suffix to remove.</param>
	/// <returns>The string with the suffix removed.</returns>
	public static string RemoveSuffix(this string s, string suffix)
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(suffix);

		return s.EndsWithOrdinal(suffix) ? s[..^suffix.Length] : s;
	}

	/// <summary>
	/// Removes the specified <paramref name="suffix"/> from the current string.
	/// </summary>
	/// <param name="s">The string to remove the suffix from.</param>
	/// <param name="suffix">The suffix to remove.</param>
	/// <returns>The string with the suffix removed.</returns>
	public static TDerived RemoveSuffix<TDerived>(this TDerived s, string suffix) where TDerived : AnyStrongString<TDerived>
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(suffix);

		return (TDerived)(s.EndsWithOrdinal(suffix) ? s.WeakString[..^suffix.Length] : s);
	}

	/// <summary>
	/// Removes the specified <paramref name="prefix"/> from the current string.
	/// </summary>
	/// <param name="s">The string to remove the prefix from.</param>
	/// <param name="prefix">The prefix to remove.</param>
	/// <returns>The string with the prefix removed.</returns>
	public static string RemovePrefix(this string s, string prefix)
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(prefix);

		return s.StartsWithOrdinal(prefix) ? s[prefix.Length..] : s;
	}

	/// <summary>
	/// Removes the specified <paramref name="prefix"/> from the current string.
	/// </summary>
	/// <param name="s">The string to remove the prefix from.</param>
	/// <param name="prefix">The prefix to remove.</param>
	/// <returns>The string with the prefix removed.</returns>
	public static TDerived RemovePrefix<TDerived>(this TDerived s, string prefix) where TDerived : AnyStrongString<TDerived>
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(prefix);

		return (TDerived)(s.StartsWithOrdinal(prefix) ? s.WeakString[prefix.Length..] : s);
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
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(oldValue);
		ArgumentNullException.ThrowIfNull(newValue);

		return s.Replace(oldValue, newValue, StringComparison.Ordinal);
	}

	/// <summary>
	/// Replace all occurrences of a string with another string using ordinal comparison.
	/// </summary>
	/// <param name="s">The string to search in.</param>
	/// <param name="oldValue">The string to replace.</param>
	/// <param name="newValue">The string to replace with.</param>
	/// <returns></returns>
	public static string ReplaceOrdinal<TDerived>(this TDerived s, string oldValue, string newValue) where TDerived : AnyStrongString<TDerived>
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentNullException.ThrowIfNull(oldValue);
		ArgumentNullException.ThrowIfNull(newValue);

		return s.Replace(oldValue, newValue);
	}

	/// <summary>
	/// Creates a regex to match Unix-style line endings (\n).
	/// </summary>
	/// <returns>A <see cref="Regex"/> object for Unix-style line endings.</returns>
	[GeneratedRegex(@"(?<!\r)\n")]
	private static partial Regex CreateLineEndingRegexUnix();

	/// <summary>
	/// Creates a regex to match Windows-style line endings (\r\n).
	/// </summary>
	/// <returns>A <see cref="Regex"/> object for Windows-style line endings.</returns>
	[GeneratedRegex(@"\r\n")]
	private static partial Regex CreateLineEndingRegexWindows();

	/// <summary>
	/// Creates a regex to match Mac-style line endings (\r).
	/// </summary>
	/// <returns>A <see cref="Regex"/> object for Mac-style line endings.</returns>
	[GeneratedRegex(@"\r(?!\n)")]
	private static partial Regex CreateLineEndingRegexMac();

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
		ArgumentNullException.ThrowIfNull(input);

		bool hasUnix = CreateLineEndingRegexUnix().IsMatch(input);
		bool hasWindows = CreateLineEndingRegexWindows().IsMatch(input);
		bool hasMac = CreateLineEndingRegexMac().IsMatch(input);

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
		ArgumentNullException.ThrowIfNull(s);

		return style switch
		{
			LineEndingStyle.None => CreateLineEndingRegexUnix().Replace(CreateLineEndingRegexWindows().Replace(CreateLineEndingRegexMac().Replace(s, ""), ""), ""),
			LineEndingStyle.Unix => CreateLineEndingRegexWindows().Replace(CreateLineEndingRegexMac().Replace(s, "\n"), "\n"),
			LineEndingStyle.Windows => CreateLineEndingRegexUnix().Replace(CreateLineEndingRegexMac().Replace(s, "\r\n"), "\r\n"),
			LineEndingStyle.Mac => CreateLineEndingRegexUnix().Replace(CreateLineEndingRegexWindows().Replace(s, "\r"), "\r"),
			LineEndingStyle.Mixed => CreateLineEndingRegexWindows().Replace(CreateLineEndingRegexMac().Replace(s, "\n"), "\n"),
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

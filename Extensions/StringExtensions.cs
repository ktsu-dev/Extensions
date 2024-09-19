namespace ktsu.Extensions;

using ktsu.StrongStrings;

/// <summary>
/// Extension methods for strings.
/// </summary>
public static class StringExtensions
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
}

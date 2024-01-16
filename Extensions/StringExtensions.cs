namespace ktsu.io.Extensions;

/// <summary>
/// Extension methods for strings.
/// </summary>
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
	/// <returns>true if str contains value; otherwise, false.</returns>
	public static bool ContainsOrdinal(this string str, string value)
	{
		ArgumentNullException.ThrowIfNull(str);
		ArgumentNullException.ThrowIfNull(value);
		return str.Contains(value, StringComparison.Ordinal);
	}
}

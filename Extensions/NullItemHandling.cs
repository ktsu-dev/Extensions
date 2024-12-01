namespace ktsu.Extensions;

/// <summary>
/// Specifies how to handle null items when converting a enumerables or collections to strings.
/// </summary>
public enum NullItemHandling
{
	/// <summary>
	/// Remove null items.
	/// </summary>
	Remove,

	/// <summary>
	/// Include null items.
	/// </summary>
	Include,

	/// <summary>
	/// Throw an exception if a null item is encountered.
	/// </summary>
	Throw,
}

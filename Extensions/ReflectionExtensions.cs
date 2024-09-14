namespace ktsu.Extensions;

using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class ReflectionExtensions
{
	/// <summary>
	/// Walks up the inheritance tree to find a method with the given name and binding flags.
	/// </summary>
	/// <param name="type">The type to search.</param>
	/// <param name="methodName">The name of the method to find.</param>
	/// <param name="bindingFlags">The binding flags to use when searching.</param>
	/// <param name="methodInfo">The method info if found; otherwise, null.</param>
	/// <returns>True if the method was found; otherwise, false.</returns>
	public static bool TryFindMethod(this Type type, string methodName, BindingFlags bindingFlags, out MethodInfo? methodInfo)
	{
		ArgumentNullException.ThrowIfNull(type);
		ArgumentNullException.ThrowIfNull(methodName);
		ArgumentException.ThrowIfNullOrEmpty(methodName);

		methodInfo = null;
		var methodOwner = type;
		while (methodInfo is null && methodOwner is not null)
		{
			methodInfo = methodOwner.GetMethod(methodName, bindingFlags);
			if (methodInfo is null)
			{
				methodOwner = methodOwner.BaseType;
			}
		}

		return methodInfo is not null;
	}
}

// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.Extensions;

using System.Reflection;

/// <summary>
/// Extension methods for reflection operations, providing utilities for working with types and methods.
/// </summary>
public static class ReflectionExtensions
{
	/// <summary>
	/// Walks up the inheritance tree to find a method with the given name and binding flags.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The search starts at <paramref name="type"/> and moves to each base class in turn, stopping at the first
	/// level that declares a method with the given name. When <paramref name="type"/> is an interface, the
	/// interfaces it inherits are searched after it, more-derived interfaces first.
	/// </para>
	/// <para>
	/// When the name is overloaded at that level, the overload with the fewest parameters is returned. A
	/// non-generic method is preferred over a generic one with the same parameter count, and any remaining tie is
	/// broken by ordinal comparison of the method signatures, so the result is always the same for the same type.
	/// </para>
	/// </remarks>
	/// <param name="type">The type to search.</param>
	/// <param name="methodName">The name of the method to find.</param>
	/// <param name="bindingFlags">The binding flags to use when searching.</param>
	/// <param name="methodInfo">The method info if found; otherwise, null.</param>
	/// <returns>True if the method was found; otherwise, false.</returns>
	public static bool TryFindMethod(this Type type, string methodName, BindingFlags bindingFlags, out MethodInfo? methodInfo)
	{
		Ensure.NotNull(type);
		Ensure.NotNull(methodName);

		if (string.IsNullOrEmpty(methodName))
		{
			throw new ArgumentException("Method name cannot be empty.", nameof(methodName));
		}

		methodInfo = null;
		foreach (Type[] level in SearchLevels(type))
		{
			methodInfo = level
				.SelectMany(t => t.GetMethods(bindingFlags | BindingFlags.DeclaredOnly))
				.Where(m => m.Name.Equals(methodName, StringComparison.Ordinal))
				.OrderBy(m => m.GetParameters().Length)
				.ThenBy(m => m.IsGenericMethodDefinition)
				.ThenBy(m => m.ToString(), StringComparer.Ordinal)
				.FirstOrDefault();

			if (methodInfo is not null)
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Yields the groups of types to search, nearest first: the type and its base classes one at a time, or for an
	/// interface, the interface itself followed by its inherited interfaces grouped by how many interfaces they in
	/// turn inherit, so that a derived interface is always searched before the interfaces it extends.
	/// </summary>
	private static IEnumerable<Type[]> SearchLevels(Type type)
	{
		if (type.IsInterface)
		{
			yield return [type];

			IEnumerable<Type[]> inheritedLevels = type.GetInterfaces()
				.GroupBy(i => i.GetInterfaces().Length)
				.OrderByDescending(g => g.Key)
				.Select(g => g.ToArray());

			foreach (Type[] level in inheritedLevels)
			{
				yield return level;
			}

			yield break;
		}

		for (Type? methodOwner = type; methodOwner is not null; methodOwner = methodOwner.BaseType)
		{
			yield return [methodOwner];
		}
	}
}

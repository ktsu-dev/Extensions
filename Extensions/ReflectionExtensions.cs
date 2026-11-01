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
	/// interfaces it inherits are searched after it, breadth-first by inheritance distance: its direct base
	/// interfaces first, then their direct bases, and so on, stopping at the nearest level that declares the name.
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
	/// interface, the interface itself followed by its inherited interfaces grouped by inheritance distance, so
	/// that each level holds exactly the interfaces one step further from <paramref name="type"/> than the last.
	/// </summary>
	private static IEnumerable<Type[]> SearchLevels(Type type)
	{
		if (type.IsInterface)
		{
			yield return [type];

			HashSet<Type> visited = [type];
			Type[] level = DirectInterfaces(type);
			while (level.Length > 0)
			{
				visited.UnionWith(level);
				yield return level;

				level = [.. level
					.SelectMany(DirectInterfaces)
					.Where(i => !visited.Contains(i))
					.Distinct()];
			}

			yield break;
		}

		for (Type? methodOwner = type; methodOwner is not null; methodOwner = methodOwner.BaseType)
		{
			yield return [methodOwner];
		}
	}

	/// <summary>
	/// Returns the interfaces <paramref name="type"/> inherits directly: those in
	/// <see cref="Type.GetInterfaces"/> that no other member of that set already inherits.
	/// </summary>
	private static Type[] DirectInterfaces(Type type)
	{
		Type[] all = type.GetInterfaces();
		return [.. all.Where(candidate => !all.Any(other => other != candidate && other.GetInterfaces().Contains(candidate)))];
	}
}

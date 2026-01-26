# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**ktsu.Extensions** is a utility library providing extension methods for standard .NET types: collections, strings, dictionaries, and reflection. It targets multiple frameworks: net9.0, net8.0, net7.0, net6.0, net5.0, netstandard2.0, and netstandard2.1.

## Build and Test Commands

```bash
# Build the library (all target frameworks)
dotnet build

# Build Release configuration
dotnet build --configuration Release

# Run all tests
dotnet test

# Run a specific test by name filter
dotnet test --filter "FullyQualifiedName~EnumerableExtensionsTests.WithIndexEnumeratesWithIndex"

# Run tests in a specific test class
dotnet test --filter "FullyQualifiedName~CollectionExtensionsTests"

# Create NuGet package
dotnet pack --configuration Release --output ./staging
```

## Architecture

### Source Organization

The library is organized by the type being extended:

- **EnumerableExtensions.cs** - `IEnumerable<T>` extensions: `WithIndex()`, `ToCollection()`, `ForEach()`, `AnyNull()`, `Join()`, `ToStringEnumerable()`
- **StringExtensions.cs** - String extensions: ordinal comparisons (`StartsWithOrdinal`, `EndsWithOrdinal`, `ContainsOrdinal`), prefix/suffix manipulation, line ending normalization
- **CollectionExtensions.cs** - `ICollection<T>` extensions: `AddFrom()`, `ReplaceWith()`
- **DictionaryExtensions.cs** - Dictionary extensions: `GetOrCreate()`, `AddOrReplace()` with `ConcurrentDictionary` support
- **ReflectionExtensions.cs** - Reflection extensions: `TryFindMethod()` for inheritance-aware method discovery
- **NullItemHandling.cs** - Enum defining null-handling strategies (Remove, Include, Throw)

### Key Patterns

**Multi-Framework Conditional Compilation**: Uses `#if NET6_0_OR_GREATER` for API availability differences:
```csharp
#if NET6_0_OR_GREATER
    ArgumentNullException.ThrowIfNull(str);
#else
    if (str is null) { throw new ArgumentNullException(nameof(str)); }
#endif
```

**Thread-Safe Variants**: Some methods accept an optional lock object for concurrent scenarios:
```csharp
public static void ForEach<T>(this IEnumerable<T> enumerable, object lockObj, Action<T> action)
```

**TryXxx Pattern**: Reflection methods return bool with out parameters instead of throwing exceptions.

### Build System

Uses the ktsu.Sdk custom MSBuild SDK for standardized configuration. Key files:
- `global.json` - SDK versions (.NET SDK, MSTest.Sdk, ktsu.Sdk)
- `Directory.Packages.props` - Central Package Management for NuGet dependencies
- `.editorconfig` - Strict code style enforcement (all rules as errors)

### Test Structure

Tests use MSTest.Sdk and target .NET 9.0 only. Test files mirror source files:
- `Extensions.Test/CollectionExtensionsTests.cs`
- `Extensions.Test/DictionaryExtensionsTests.cs`
- `Extensions.Test/EnumerableExtensionsTests.cs`
- `Extensions.Test/ReflectionExtensionsTests.cs`
- `Extensions.Test/StringExtensionsTests.cs`

## Version Management

Versioning is calculated automatically from git history. Include version markers in commit messages:
- `[major]` - Breaking API changes
- `[minor]` - New features (auto-detected for .cs file changes)
- `[patch]` - Bug fixes
- `[pre]` - Pre-release/experimental changes

Auto-generated files (do not edit manually): `VERSION.md`, `CHANGELOG.md`, `LICENSE.md`

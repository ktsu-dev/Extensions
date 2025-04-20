# ktsu.Extensions

> A comprehensive utility library of extension methods for collections, strings, dictionaries, and reflection in .NET.

[![License](https://img.shields.io/github/license/ktsu-dev/Extensions)](https://github.com/ktsu-dev/Extensions/blob/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/ktsu.Extensions.svg)](https://www.nuget.org/packages/ktsu.Extensions/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ktsu.Extensions.svg)](https://www.nuget.org/packages/ktsu.Extensions/)
[![Build Status](https://github.com/ktsu-dev/Extensions/workflows/build/badge.svg)](https://github.com/ktsu-dev/Extensions/actions)
[![GitHub Stars](https://img.shields.io/github/stars/ktsu-dev/Extensions?style=social)](https://github.com/ktsu-dev/Extensions/stargazers)

## Introduction

`ktsu.Extensions` is a utility library that enhances the functionality of standard .NET types through extension methods. It provides a wide range of utilities for explicit shallow and deep cloning, batch operations, string manipulations, and reflection helpers, making it easier to work with common data structures and types in a consistent, null-safe manner.

## Features

- **Enumerable Extensions**
  - `WithIndex`: Enumerates over an enumerable with the index of the item
  - `ToCollection`: Converts an enumerable to a collection
  - `ForEach`: Applies an action to each element of an enumerable
  - `DeepClone`/`ShallowClone`: Creates clones of a collection of items
  - `AnyNull`: Checks if the enumerable contains any null items

- **Collection Extensions**
  - `AddMany`: Adds items from an enumerable to a collection
  - `ToStringCollection`: Converts a collection to a collection of strings

- **Dictionary Extensions**
  - `GetOrCreate`: Gets the value for a key or creates a new value if the key doesn't exist
  - `AddOrReplace`: Adds a new value or replaces an existing value
  - `DeepClone`/`ShallowClone`: Creates clones of a dictionary

- **String Extensions**
  - `As<TDest>`: Converts between string types
  - Ordinal comparison helpers (`StartsWithOrdinal`, `EndsWithOrdinal`, `ContainsOrdinal`)
  - Prefix/suffix manipulation (`RemoveSuffix`, `RemovePrefix`)
  - Line ending utilities (`DetermineLineEndings`, `NormalizeLineEndings`)

- **Reflection Extensions**
  - `TryFindMethod`: Searches for methods across inheritance hierarchies

## Installation

### Package Manager Console

```powershell
Install-Package ktsu.Extensions
```

### .NET CLI

```bash
dotnet add package ktsu.Extensions
```

### Package Reference

```xml
<PackageReference Include="ktsu.Extensions" Version="x.y.z" />
```

## Usage Examples

### Enumerable Extensions

```csharp
using ktsu.Extensions;

// Iterate with index
foreach (var (item, index) in myList.WithIndex())
{
    Console.WriteLine($"Item at position {index}: {item}");
}

// Apply action to each item
myList.ForEach(item => Console.WriteLine(item));

// Create clones
var deepClone = myList.DeepClone();
var shallowClone = myList.ShallowClone();

// Check for nulls
if (myList.AnyNull())
{
    Console.WriteLine("List contains null items");
}
```

### String Extensions

```csharp
using ktsu.Extensions;

string text = "Hello, World!";

// Ordinal string comparisons
if (text.StartsWithOrdinal("Hello"))
{
    Console.WriteLine("Text starts with 'Hello'");
}

// Prefix/suffix manipulation
string withoutPrefix = text.RemovePrefix("Hello, ");  // "World!"
string withoutSuffix = text.RemoveSuffix("!");        // "Hello, World"

// Line ending handling
string mixedText = "Line1\r\nLine2\nLine3";
var lineEndingStyle = mixedText.DetermineLineEndings();  // LineEndingStyle.Mixed
string normalized = mixedText.NormalizeLineEndings(LineEndingStyle.Unix);  // All \n
```

### Dictionary Extensions

```csharp
using ktsu.Extensions;

var cache = new Dictionary<string, List<string>>();

// Get or create a value
var items = cache.GetOrCreate("key", () => new List<string>());
items.Add("item1");

// Add or replace a value
cache.AddOrReplace("key2", new List<string> { "item2" });

// Clone the dictionary
var deepClone = cache.DeepClone();
```

## Advanced Usage

### Working with StrongStrings

```csharp
using ktsu.Extensions;
using ktsu.StrongStrings;

// Convert a regular string to a strong string
var strongId = "12345".As<ID>();

// Apply string extensions to strong strings
if (strongId.StartsWithOrdinal("123"))
{
    // Do something with the strong string
}
```

### Null Item Handling

```csharp
using ktsu.Extensions;

var items = new[] { "one", null, "three" };

// Convert to strings with null handling
var strings1 = items.ToStringEnumerable(NullItemHandling.Skip);       // ["one", "three"]
var strings2 = items.ToStringEnumerable(NullItemHandling.UseEmpty);   // ["one", "", "three"]
var strings3 = items.ToStringEnumerable(NullItemHandling.UseNull);    // ["one", null, "three"]
var strings4 = items.ToStringEnumerable(NullItemHandling.UseDefault); // ["one", "(null)", "three"]
```

### Reflection Helpers

```csharp
using ktsu.Extensions;
using System.Reflection;

// Find a method across inheritance hierarchy
if (someType.TryFindMethod("MethodName", BindingFlags.Instance | BindingFlags.Public, out var methodInfo))
{
    // Use the method info
    methodInfo.Invoke(instance, parameters);
}
```

## API Reference

### String Extensions

| Method | Description |
|--------|-------------|
| `As<TDest>` | Converts a string to a strong string type |
| `StartsWithOrdinal` | Checks if string starts with value using ordinal comparison |
| `EndsWithOrdinal` | Checks if string ends with value using ordinal comparison |
| `ContainsOrdinal` | Checks if string contains value using ordinal comparison |
| `RemovePrefix` | Removes a prefix from a string if present |
| `RemoveSuffix` | Removes a suffix from a string if present |
| `ReplaceOrdinal` | Replaces text using ordinal comparison |
| `DetermineLineEndings` | Identifies line ending style in a string |
| `NormalizeLineEndings` | Converts line endings to a specific style |

### Collection Extensions

| Method | Description |
|--------|-------------|
| `AddMany` | Adds multiple items to a collection |
| `AnyNull` | Checks if collection contains any null items |
| `ToStringCollection` | Converts collection to string collection |
| `WriteItemsToConsole` | Displays collection items in console |

### Dictionary Extensions

| Method | Description |
|--------|-------------|
| `GetOrCreate` | Gets existing value or creates new one |
| `AddOrReplace` | Adds a new value or replaces existing one |
| `DeepClone` | Creates a deep copy of the dictionary |
| `ShallowClone` | Creates a shallow copy of the dictionary |

## Contributing

Contributions are welcome! Here's how you can help:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please make sure to update tests as appropriate.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

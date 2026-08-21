# ktsu.Extensions

> A comprehensive utility library of extension methods for collections, strings, dictionaries, and reflection in .NET.

[![License](https://img.shields.io/github/license/ktsu-dev/Extensions.svg?label=License&logo=nuget)](LICENSE.md)
[![NuGet Version](https://img.shields.io/nuget/v/ktsu.Extensions?label=Stable&logo=nuget)](https://nuget.org/packages/ktsu.Extensions)
[![NuGet Version](https://img.shields.io/nuget/vpre/ktsu.Extensions?label=Latest&logo=nuget)](https://nuget.org/packages/ktsu.Extensions)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ktsu.Extensions?label=Downloads&logo=nuget)](https://nuget.org/packages/ktsu.Extensions)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/m/ktsu-dev/Extensions?label=Commits&logo=github)](https://github.com/ktsu-dev/Extensions/commits/main)
[![GitHub contributors](https://img.shields.io/github/contributors/ktsu-dev/Extensions?label=Contributors&logo=github)](https://github.com/ktsu-dev/Extensions/graphs/contributors)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/ktsu-dev/Extensions/dotnet.yml?branch=main&label=Build&logo=github)](https://github.com/ktsu-dev/Extensions/actions)

## Introduction

`ktsu.Extensions` is a utility library that enhances the functionality of standard .NET types through extension methods. It provides a wide range of utilities for batch operations, string manipulations, and reflection helpers, making it easier to work with common data structures and types in a consistent, null-safe manner.

## Features

- **Enumerable Extensions**
  - `WithIndex`: Enumerates over an enumerable with the index of the item
  - `ToCollection`: Converts an enumerable to a collection
  - `ForEach`: Applies an action to each element of an enumerable
  - `AnyNull`: Checks if the enumerable contains any null items
  - `Join`: Concatenates elements with a separator
  - `ToStringEnumerable`: Converts items to strings with null handling
  - `WriteItemsToConsole`: Outputs collection items to the console

- **Collection Extensions**
  - `AddFrom`: Adds items from an enumerable to a collection
  - `ReplaceWith`: Replaces all items in a collection with items from an enumerable

- **Dictionary Extensions**
  - `GetOrCreate`: Gets the value for a key or creates a new value if the key doesn't exist
  - `AddOrReplace`: Adds a new value or replaces an existing value

- **String Extensions**
  - Ordinal comparison helpers (`StartsWithOrdinal`, `EndsWithOrdinal`, `ContainsOrdinal`)
  - Prefix/suffix manipulation (`RemoveSuffix`, `RemovePrefix`)
  - `ReplaceOrdinal`: Replaces text using ordinal comparison
  - Line ending utilities (`DetermineLineEndings`, `NormalizeLineEndings`)
  - `NominalWordWrap`: Best-effort word wrapping on word and hyphen boundaries for a given wrap width and nominal glyph width

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

// Check for nulls
if (myList.AnyNull())
{
    Console.WriteLine("List contains null items");
}

// Join items with a separator
var items = new[] { "apple", "banana", "cherry" };
string joined = items.Join(", ");  // "apple, banana, cherry"

// Convert to string enumerable
var numbers = new[] { 1, 2, 3 };
var strings = numbers.ToStringEnumerable();  // ["1", "2", "3"]
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

// Best-effort word wrap. Line length is wrapWidth / nominalGlyphWidth, so with a
// wrap width of 110 and a nominal glyph width of 10 each line holds up to 11 chars.
var lines = "the quick brown fox".NominalWordWrap(wrapWidth: 110f, nominalGlyphWidth: 10f);
// ["the quick", "brown fox"]

// Existing line breaks are honored, whitespace runs collapse, and a break may fall
// after a hyphen or at a soft hyphen (­ renders as "-" only when it wraps).
var wrapped = "context-sensitive".NominalWordWrap(wrapWidth: 90f, nominalGlyphWidth: 10f);
// ["context-", "sensitive"]
```

### Dictionary Extensions

```csharp
using ktsu.Extensions;

var cache = new Dictionary<string, List<string>>();

// Get or create a value (uses parameterless constructor)
var items = cache.GetOrCreate("key");
items.Add("item1");

// Get or create with a specific default value
var otherItems = cache.GetOrCreate("key2", new List<string> { "default" });

// Add or replace a value
cache.AddOrReplace("key3", new List<string> { "item2" });
```

### Collection Extensions

```csharp
using ktsu.Extensions;

var collection = new List<string>();

// Add multiple items at once
collection.AddFrom(new[] { "item1", "item2", "item3" });

// Replace all items in the collection
collection.ReplaceWith(new[] { "new1", "new2" }); // Collection now contains only "new1" and "new2"
```

## Advanced Usage

### Null Item Handling

```csharp
using ktsu.Extensions;

var items = new[] { "one", null, "three" };

// Convert to strings with null handling
var strings1 = items.ToStringEnumerable(NullItemHandling.Remove);   // ["one", "three"]
var strings2 = items.ToStringEnumerable(NullItemHandling.Include);  // ["one", null, "three"]
// NullItemHandling.Throw will throw an exception if null items are found

// Join with null handling
var joined = items.Join(", ", NullItemHandling.Remove);  // "one, three"
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

### Enumerable Extensions

| Method | Description |
|--------|-------------|
| `WithIndex` | Enumerates with the index of each item |
| `ToCollection` | Converts an enumerable to a collection |
| `ForEach` | Applies an action to each element |
| `AnyNull` | Checks if enumerable contains any null items |
| `Join` | Concatenates elements with a separator |
| `ToStringEnumerable` | Converts items to strings with null handling |
| `WriteItemsToConsole` | Displays enumerable items in console |

### String Extensions

| Method | Description |
|--------|-------------|
| `StartsWithOrdinal` | Checks if string starts with value using ordinal comparison |
| `EndsWithOrdinal` | Checks if string ends with value using ordinal comparison |
| `ContainsOrdinal` | Checks if string contains value using ordinal comparison |
| `RemovePrefix` | Removes a prefix from a string if present |
| `RemoveSuffix` | Removes a suffix from a string if present |
| `ReplaceOrdinal` | Replaces text using ordinal comparison |
| `DetermineLineEndings` | Identifies line ending style in a string |
| `NormalizeLineEndings` | Converts line endings to a specific style |
| `NominalWordWrap` | Best-effort word wrap on word and hyphen boundaries for a wrap width and nominal glyph width |

### Collection Extensions

| Method | Description |
|--------|-------------|
| `AddFrom` | Adds items from an enumerable to a collection |
| `ReplaceWith` | Replaces all items in a collection with new items |

### Dictionary Extensions

| Method | Description |
|--------|-------------|
| `GetOrCreate` | Gets existing value or creates new one |
| `AddOrReplace` | Adds a new value or replaces existing one |

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


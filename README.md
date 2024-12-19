# ktsu.Extensions

A utility library of extension methods designed to enhance the functionality of collections, enumerables, dictionaries, strings, and reflection in .NET. This library provides utilities for explicit shallow and deep cloning, batch operations, and advanced string manipulations, making it easier to work with common data structures and types.

## Features

- **Enumerable Extensions**
  - `WithIndex`: Enumerates over an enumerable with the index of the item.
  - `ToCollection`: Converts an enumerable to a collection.
  - `ForEach`: Applies an action to each element of an enumerable.
  - `DeepClone`: Creates a deep clone of a collection of items.
  - `ShallowClone`: Creates a shallow clone of a collection of items.
  - `AnyNull`: Checks if the enumerable contains any null items.
  - `WriteItemsToConsole`: Writes the items of the enumerable to the console, skipping null items.
  - `ToStringEnumerable`: Converts an enumerable to an enumerable of strings, handling null items according to the specified behavior.

- **Collection Extensions**
  - `AddMany`: Adds items from an enumerable to a collection.

- **Dictionary Extensions**
  - `GetOrCreate`: Gets the value associated with the specified key or creates a new value if the key does not exist.
  - `AddOrReplace`: Adds a new value or replaces the existing value for the specified key.
  - `DeepClone`: Creates a deep clone of a dictionary.
  - `ShallowClone`: Creates a shallow clone of a dictionary.

- **String Extensions**
  - `As<TDest>`: Converts a weak string to a strong string of the specified type, or converts between strong string types.
  - `StartsWithOrdinal`: Compares two strings using ordinal comparison to check if the string starts with the specified value.
  - `EndsWithOrdinal`: Compares two strings using ordinal comparison to check if the string ends with the specified value.
  - `ContainsOrdinal`: Compares two strings using ordinal comparison to check if the string contains the specified value.
  - `RemoveSuffix`: Removes the specified suffix from the current string.
  - `RemovePrefix`: Removes the specified prefix from the current string.
  - `ReplaceOrdinal`: Replaces all occurrences of a string with another string using ordinal comparison.

- **Reflection Extensions**
  - `TryFindMethod`: Tries to find a method with the specified name and binding flags in the given type.

## Installation

To install the library, use the following NuGet command:

```bash
dotnet add package ktsu.Extensions
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

## License

This project is licensed under the MIT License.

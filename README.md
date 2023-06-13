# Sweaj.Patterns
A library intended for easily adapting a familiar & flexible abstraction of many patterns I use today.

[![.NET](https://github.com/Reapism/Sweaj.Patterns/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Reapism/Sweaj.Patterns/actions/workflows/dotnet.yml)

# Conversions
The `IConverter` interface represents a contract for converting an object from one type to another synchronously, while the `IAsyncConverter`
interface represents a contract for converting an object from one type to another asynchronously, optionally supporting cancellation.
```csharp 
int convertedValue = converter.Convert(str);
```

```csharp 
int convertedValue = await converter.ConvertAsync(str);
```

# Cache

# Repository
Creating a generic repository that is fully flexible, all the way from its type, to its Key Type

```csharp
public interface IRepository<TKey, TEntity> { }
```

# Request / Response
The `ApiResponse` encapsulates the minimum ApiResponse, that 
```csharp
    public class ApiResponse { ... }
```

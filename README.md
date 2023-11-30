# Sweaj.Patterns ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) [![.NET](https://github.com/Reapism/Sweaj.Patterns/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Reapism/Sweaj.Patterns/actions/workflows/dotnet.yml)

A library intended for easily adapting familiar & flexible abstractions of many patterns I use today. Keep in mind that this is opinionated, and what drives me is flexible, but minimally functional. The library uses common contracts across multiple patterns which guides the development of a cohesive application, it makes it more rigid to follow specific patterns if you choose to, which makes diagnostics more simple, and architecture more guided.

# Adisory [status](status.png)
This library is under heavy development, I wouldn't advise using this library yet as most of the updates contain breaking changes. I am currently
updating this library as I continue to work with these patterns and find a good balance of flexibility and an opinionated approach to its contracts and implementations. 

# Library Features
* AI
* Attributes
* Cache
* Converters
* Data
  * DDD (Domain Driven Design)
  * Entities, AuditableEntities
  * Repositories
  * UnitOfWork
  * ValueObjects
  * Values (ValueStore & DataStore)
* Dates
* Exceptions
* Guards
* Logging
* Mapping
* NullObject
* Options
* Reflection
* Rest (Request/Response)
* Scientific
* Serialization

# WIKI
See the wiki for examples of each! Below are the best uses of library examples 

# IValueProvider<TValue>
A mechanism for providing a **valid** value of something. 

This is used thoroughly in the patterns library as being the payload for a request, the response object for responses, an entity in a database, or File contents, it represents the underlying value of an object.

## Conversions
Conversions are simple, you convert a `source` value to a `target` value.

The `IConverter` interface represents a contract for converting an object from one type to another synchronously, while the `IAsyncConverter`
interface represents a contract for converting an object from one type to another asynchronously, optionally supporting cancellation.
```csharp
string str = "7"

int convertedValue = converter.Convert(str);

// convertedValue = 7
```

## Request / Response
The `ApiResponse` encapsulates the minimum ApiResponse, that 
```csharp
    public class ApiResponse { ... }
```


![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)

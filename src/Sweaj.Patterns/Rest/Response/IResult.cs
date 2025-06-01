using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response;

/// <summary>
/// Represents a generic result interface that provides access to a value.
/// </summary>
/// <typeparam name="TValue">The type of the result value.</typeparam>
/// <remarks>
/// This interface extends <see cref="IValueProvider{T}"/> to unify result-handling patterns
/// across the application.
/// </remarks>
/// <seealso cref="IValueProvider{T}"/>
[Trackable]
public interface IResult<TValue> : IValueProvider<TValue?>
{ }

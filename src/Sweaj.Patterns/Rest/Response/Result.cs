using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response;

/// <summary>
/// Represents a generic result container that encapsulates a value and a unique result identifier.
/// </summary>
/// <typeparam name="TValue">The type of the encapsulated result value.</typeparam>
[Trackable]
public class Result<TValue> : IResult<TValue?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The result value to be encapsulated.</param>
    protected Result(TValue? value)
    {
        ResultId = Guid.NewGuid();
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="Result{TValue}"/> instance with the specified value.
    /// </summary>
    /// <param name="value">The value to encapsulate.</param>
    /// <returns>A new <see cref="Result{TValue}"/> instance.</returns>
    public static Result<TValue?> Create(TValue value)
    {
        return new Result<TValue?>(value);
    }

    /// <summary>
    /// Creates a new <see cref="Result{TValue}"/> instance using the value from the provided <see cref="IValueProvider{T}"/>.
    /// </summary>
    /// <param name="valueProvider">An instance that provides a value.</param>
    /// <returns>A new <see cref="Result{TValue}"/> instance.</returns>
    public static Result<TValue?> Create(IValueProvider<TValue> valueProvider)
    {
        return new Result<TValue?>(valueProvider.Value);
    }

    /// <summary>
    /// Asynchronously creates a new <see cref="Result{TValue}"/> using a value factory.
    /// </summary>
    /// <typeparam name="TParams">The type of parameters passed to the factory (not currently used).</typeparam>
    /// <param name="valueFactory">The factory responsible for asynchronously producing a value.</param>
    /// <param name="parameters">Parameters to configure value creation (placeholder for future use).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new <see cref="Result{TValue}"/>.</returns>
    public static async Task<Result<TValue?>> Create<TParams>(
        IValueFactory<TValue> valueFactory,
        TParams parameters,
        CancellationToken cancellationToken)
    {
        return new Result<TValue?>(await valueFactory.CreateValueAsync(cancellationToken));
    }

    /// <summary>
    /// Gets the unique identifier associated with this result instance.
    /// </summary>
    public Guid ResultId { get; }

    /// <summary>
    /// Gets the encapsulated result value.
    /// </summary>
    public TValue? Value { get; }
}

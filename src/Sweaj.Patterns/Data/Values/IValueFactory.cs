namespace Sweaj.Patterns.Data.Values
{
    /// <summary>
    /// Specifies that a derrived type has the necessary information to create itself asynchronously.
    /// </summary>
    /// <typeparam name="TValue">The value to create.</typeparam>
    public interface IValueFactory<TValue>
    {
        Task<TValue> CreateValueAsync(CancellationToken cancellationToken = default);
    }
}

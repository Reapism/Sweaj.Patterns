namespace Sweaj.Patterns.Data.Services
{
    public interface IValueProvider<TValue>
    {
        public TValue Value { get; }
    }

    public interface IValueFactory<TValue>
    {
        Task<TValue> CreateAsync<TParams>(Func<TParams, Task<TValue>> valueFactory, CancellationToken cancellationToken = default);
    }
}

namespace Sweaj.Patterns.Options
{
    public interface IOptions<T>
    {
        void Configure(Action<T> options);
    }

    public interface IAsyncOptions<T>
    {
        Task ConfigureAsync(Task<Action<T>> options);
    }
}

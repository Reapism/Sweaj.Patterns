namespace Sweaj.Patterns.Data.Services
{
    public interface IValueProvider<TValue>
    {
        public TValue Value { get; }
    }
}

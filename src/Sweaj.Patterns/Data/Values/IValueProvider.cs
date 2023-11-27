namespace Sweaj.Patterns.Data.Values
{
    /// <summary>
    /// Repres
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IValueProvider<TValue>
    {
        public TValue Value { get; }
    }
}

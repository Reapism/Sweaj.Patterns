namespace Sweaj.Patterns.Converters
{
    public interface IConverter<TValue, TReturn>
    {
        TReturn Convert(TValue value);
    }
}

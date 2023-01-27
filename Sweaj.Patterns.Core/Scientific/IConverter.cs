namespace Sweaj.Patterns.Scientific
{
    public interface IConverter<TValue, TReturn>
    {
        TReturn Convert(TValue value);
    }
}

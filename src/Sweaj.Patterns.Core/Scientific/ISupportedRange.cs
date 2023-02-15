namespace Sweaj.Patterns.Scientific
{
    public interface ISupportedRange<TValueObject>
    {
        TValueObject Min();

        TValueObject Max();
    }
}

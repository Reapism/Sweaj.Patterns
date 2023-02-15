namespace Sweaj.Patterns.Scientific
{
    public interface IMeasurement<TValue, TUnit>
    {
        TValue Value { get; set; }
        TUnit Unit { get; set; }
    }
}


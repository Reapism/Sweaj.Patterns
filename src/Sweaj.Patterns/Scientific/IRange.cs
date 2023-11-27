namespace Sweaj.Patterns.Scientific
{
    public interface IRange<TValue>
    {
        TValue Min();

        TValue Max();
    }
}

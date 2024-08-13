using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Scientific
{
    [Trackable]
    public interface IRange<TValue>
    {
        TValue Min();

        TValue Max();
    }
}

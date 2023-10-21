using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Dates
{
    [ImplementationStats]
    public interface IDateTimeProvider
    {
        DateTimeOffset Now();
    }
}

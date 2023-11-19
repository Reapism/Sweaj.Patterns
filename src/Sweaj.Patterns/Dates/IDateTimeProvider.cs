using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Dates
{
    [Trackable]
    public interface IDateTimeProvider
    {
        DateTimeOffset Now();
    }
}

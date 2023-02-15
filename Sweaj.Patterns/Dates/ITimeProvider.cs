namespace Sweaj.Patterns.Dates
{
    public interface ITimeProvider
    {
        DateTimeOffset GetCurrentUtcTime();
    }
}

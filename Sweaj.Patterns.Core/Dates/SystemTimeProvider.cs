namespace Sweaj.Patterns.Dates
{
    public class SystemTimeProvider : ITimeProvider
    {
        public DateTimeOffset GetCurrentUtcTime()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}

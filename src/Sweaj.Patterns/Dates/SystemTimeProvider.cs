namespace Sweaj.Patterns.Dates
{
    public class SystemTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}

namespace Sweaj.Patterns.Dates
{
    public sealed class SystemTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow; 
        }
    }
}

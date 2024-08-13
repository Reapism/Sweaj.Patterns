namespace Sweaj.Patterns.Dates
{
    /// <summary>
    /// Implements the time provider by using the execution environments time as UTC.
    /// </summary>
    public sealed class SystemTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow; 
        }
    }
}

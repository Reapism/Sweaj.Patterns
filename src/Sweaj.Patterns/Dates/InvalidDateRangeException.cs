namespace Sweaj.Patterns.Dates
{
    public sealed class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string message) : base(message) { }
    }
}

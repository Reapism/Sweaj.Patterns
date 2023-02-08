namespace Sweaj.Patterns.Data.ValueObjects
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string message) : base(message) { }
    }
}

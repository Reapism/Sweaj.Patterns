namespace Sweaj.Patterns.Exceptions
{
    public class PatternsException : Exception
    {
        public PatternsException()
        { }

        public PatternsException(string? message)
            : base(message)
        { }

        public PatternsException(string? message, Exception? innerException)
            : base(message, innerException)
        { }
    }
}

using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Exceptions
{
    /// <summary>
    /// A general exception generated from this library. This is used for tracking.
    /// </summary>\
    [Trackable]
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

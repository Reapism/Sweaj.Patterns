using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Logging
{
    /// <summary>
    /// A logger provided from 
    /// </summary> 
    [Trackable]
    public interface ILogger
    {
        /// <summary>
        /// An abstraction for a logger.
        /// </summary>
        /// <param name="logType">The type of log to transmit.</param>
        /// <param name="messageTemplate">The message template for the message.</param>
        /// <param name="args">Optionally provide arguments for the message template.</param>
        void Log(string logType, string messageTemplate, params object[]? args);
    }
}

namespace Sweaj.Patterns.Logging
{
    public static class ILoggerExtensions
    {
        private const string Trace = "TRC";
        private const string Info = "INF";
        private const string Debug = "DBG";
        private const string Warning = "WAR";
        private const string Error = "ERR";
        private const string Fatal = "FAT";

        /// <summary>
        /// Logs a trace message.
        /// </summary>
        /// <param name="logger">The logger to extend.</param>
        /// <param name="messageTemplate">A message template with optional args. <code>e.g. LogTrace("Today is {Date}, and it is {WeatherCondition}.", date, weatherCondtion) </code></param>
        /// <param name="args">Optional arguments </param>
        public static void LogTrace(this ILogger logger, string messageTemplate, params object[]? args)
        {
            logger.Log(Trace, messageTemplate, args);
        }

        public static void LogInfo(this ILogger logger, string messageTemplate, params object[]? args)
        {
            logger.Log(Info, messageTemplate, args);
        }
        public static void LogDebug(this ILogger logger, string messageTemplate, params object[]? args)
        {
            logger.Log(Debug, messageTemplate, args);
        }
        public static void LogWarning(this ILogger logger, string messageTemplate, object[]? args)
        {
            logger.Log(Warning, messageTemplate, args);
        }
        public static void LogError(this ILogger logger, string messageTemplate, params object[]? args)
        {
            logger.Log(Error, messageTemplate, args);
        }

        public static void LogFatal(this ILogger logger, string messageTemplate, params object[]? args)
        {
            logger.Log(Fatal, messageTemplate, args);
        }
    }
}

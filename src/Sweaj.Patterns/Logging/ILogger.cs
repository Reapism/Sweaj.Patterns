namespace Sweaj.Patterns.Logging
{
    public interface ILogger
    {
        void Log(string logType, string messageTemplate, params object[]? args);
    }
}

namespace SharedLibraries.Logging
{
    /// <summary>
    /// Responsible for logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets a logger name as ID of loggers.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Notifies a log message.
        /// </summary>
        /// <param name="severity">The log severity.</param>
        /// <param name="message">The log message.</param>
        void Log(Severity severity, string message);
    }
}

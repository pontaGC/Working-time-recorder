namespace SharedLibraries.Logging
{
    /// <summary>
    /// The logger which does nothing.
    /// </summary>
    public sealed class EmptyLogger : ILogger
    {
        private EmptyLogger()
        {
        }

        private static readonly Lazy<EmptyLogger> lazyInstance = new Lazy<EmptyLogger>();

        public static EmptyLogger Instance => lazyInstance.Value;

        /// <inheritdoc />
        public string Name => "Logger.Empty";

        /// <inheritdoc />
        public void Log(Severity severity, string message)
        {
        }
    }
}

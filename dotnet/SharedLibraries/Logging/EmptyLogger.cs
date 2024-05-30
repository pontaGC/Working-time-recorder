namespace SharedLibraries.Logging
{
    /// <summary>
    /// The logger which does nothing. This instance is singleton.
    /// </summary>
    public sealed class EmptyLogger : LoggerBase
    {
        private EmptyLogger()
            : base("Logger.Empty")
        {
        }

        private static readonly Lazy<EmptyLogger> lazyInstance = new Lazy<EmptyLogger>();

        /// <summary>
        /// Gets a singleton instance.
        /// </summary>
        public static EmptyLogger Instance => lazyInstance.Value;

        /// <inheritdoc />
        public override void Log(Severity severity, string message)
        {
        }
    }
}

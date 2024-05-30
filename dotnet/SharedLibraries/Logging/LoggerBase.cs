namespace SharedLibraries.Logging
{
    /// <summary>
    /// The logger base.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerBase"/> class.
        /// </summary>
        /// <param name="name">The name of logger.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is an empty string.</exception>
        protected LoggerBase(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            this.Name = name;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public void Log(string message)
        {
            this.Log(Severity.None, message);
        }

        /// <inheritdoc />
        public abstract void Log(Severity severity, string message);
    }
}

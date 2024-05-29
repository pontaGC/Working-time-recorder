namespace SharedLibraries.Logging
{
    /// <summary>
    /// The logger collection.
    /// </summary>
    public interface ILoggerCollection : IReadOnlyCollection<ILogger>
    {
        /// <summary>
        /// Gets a logger with the given logger name.
        /// </summary>
        /// <param name="loggerName">The name of logger to get.</param>
        /// <returns>
        /// An instance of the logger whose name is <paramref name="loggerName"/>.
        /// If the given logger name is not found, returns an instance of <see cref="EmptyLogger"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="loggerName"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="loggerName"/> is an empty string.</exception>
        public ILogger Resolve(string loggerName);
    }
}

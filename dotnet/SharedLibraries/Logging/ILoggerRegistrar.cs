namespace SharedLibraries.Logging
{
    /// <summary>
    /// Reponsible for stocking the registered logger.
    /// </summary>
    public interface ILoggerRegistrar
    {
        /// <summary>
        /// Registers a logger provider.
        /// </summary>
        /// <param name="loggerProvider">The logger provider.</param>
        /// <exception cref="ArgumentNullException"><paramref name="loggerProvider"/> is <c>null</c>.</exception>
        void Register(ILoggerProvider loggerProvider);
    }
}

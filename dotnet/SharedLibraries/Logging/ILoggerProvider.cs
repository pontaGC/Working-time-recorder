using System.Diagnostics.CodeAnalysis;

namespace SharedLibraries.Logging
{
    /// <summary>
    /// Responsible for providing a logger.
    /// </summary>
    public interface ILoggerProvider
    {
        /// <summary>
        /// Provides a logger instance.
        /// </summary>
        /// <returns>An instance of <see cref="ILogger"/>.</returns>
        [return:NotNull]
        ILogger Provide();
    }
}

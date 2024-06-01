using System.Diagnostics.CodeAnalysis;

using SharedLibraries.Logging;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.OutputWindows;

namespace WorkingTimeRecorder.wpf.Presentation.Logging
{
    /// <summary>
    /// Responsible for creating <see cref="OutputWindowLogger"/>.
    /// </summary>
    internal sealed class OutputWindowLoggerProvider : ILoggerProvider
    {
        private readonly IWTRSystem wtrSystem;
        private readonly IOutputWindowController outputWindowController;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindowLoggerProvider"/> class.
        /// </summary>
        /// <param name="wtrSystem">The wtr system.</param>
        /// <param name="outputWindowController">The output window controller.</param>
        /// <exception cref="ArgumentNullException"><paramref name="wtrSystem"/> or <paramref name="outputWindowController"/> is <c>null</c>.</exception>
        public OutputWindowLoggerProvider(IWTRSystem wtrSystem, IOutputWindowController outputWindowController)
        {
            ArgumentNullException.ThrowIfNull(wtrSystem);
            ArgumentNullException.ThrowIfNull(outputWindowController);

            this.wtrSystem = wtrSystem;
            this.outputWindowController = outputWindowController;
        }

        /// <inheritdoc />
        [return: NotNull]
        public ILogger Provide()
        {
            return new OutputWindowLogger(this.wtrSystem, this.outputWindowController);
        }
    }
}

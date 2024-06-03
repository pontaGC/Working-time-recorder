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
        private readonly IWTRSvc wtrSvc;
        private readonly IOutputWindowController outputWindowController;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindowLoggerProvider"/> class.
        /// </summary>
        /// <param name="wtrSvc">The wtr service.</param>
        /// <param name="outputWindowController">The output window controller.</param>
        /// <exception cref="ArgumentNullException"><paramref name="wtrSvc"/> or <paramref name="outputWindowController"/> is <c>null</c>.</exception>
        public OutputWindowLoggerProvider(IWTRSvc wtrSvc, IOutputWindowController outputWindowController)
        {
            ArgumentNullException.ThrowIfNull(wtrSvc);
            ArgumentNullException.ThrowIfNull(outputWindowController);

            this.wtrSvc = wtrSvc;
            this.outputWindowController = outputWindowController;
        }

        /// <inheritdoc />
        [return: NotNull]
        public ILogger Provide()
        {
            return new OutputWindowLogger(this.wtrSvc, this.outputWindowController);
        }
    }
}

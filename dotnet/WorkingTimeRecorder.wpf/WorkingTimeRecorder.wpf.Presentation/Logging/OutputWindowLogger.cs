using SharedLibraries.Logging;

using WorkingTimeRecorder.Core.Extensions;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.OutputWindows;

namespace WorkingTimeRecorder.wpf.Presentation.Logging
{
    /// <summary>
    /// The output window logger.
    /// </summary>
    internal sealed class OutputWindowLogger : LoggerBase
    {
        private readonly IOutputWindowController outputWindowController;
        private readonly IWTRSystem wtrSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindowLogger"/> class.
        /// </summary>
        /// <param name="wtrSystem">The wtr system.</param>
        /// <param name="outputWindowController">The output window controller.</param>
        public OutputWindowLogger(
            IWTRSystem wtrSystem,
            IOutputWindowController outputWindowController)
            : base(LogConstants.OutputWindow)
        {
            this.wtrSystem = wtrSystem;
            this.outputWindowController = outputWindowController;
        }

        /// <inheritdoc />
        public override void Log(Severity severity, string message)
        {
            if (severity == Severity.None)
            {
                this.outputWindowController.AppendMessage(message);
            }

            var severityText = this.GetSeverityText(severity);
            this.outputWindowController.AppendMessage(severity, $"{severityText}\t{message}");
        }

        private string GetSeverityText(Severity severity)
        {
            switch (severity)
            {
                case Severity.Information:
                    return this.wtrSystem.LanguageLocalizer.LocalizeInformation();

                case Severity.Warning:
                    return this.wtrSystem.LanguageLocalizer.LocalizeWarning();

                case Severity.Error:
                case Severity.Alert:
                case Severity.Fatal:
                    return this.wtrSystem.LanguageLocalizer.LocalizeError();

                default:
                    return string.Empty;
            }
        }
    }
}

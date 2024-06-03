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
        private readonly IWTRSvc wtrSvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindowLogger"/> class.
        /// </summary>
        /// <param name="wtrSvc">The wtr svc.</param>
        /// <param name="outputWindowController">The output window controller.</param>
        public OutputWindowLogger(
            IWTRSvc wtrSvc,
            IOutputWindowController outputWindowController)
            : base(LogConstants.OutputWindow)
        {
            this.wtrSvc = wtrSvc;
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
                    return this.wtrSvc.LanguageLocalizer.LocalizeInformation();

                case Severity.Warning:
                    return this.wtrSvc.LanguageLocalizer.LocalizeWarning();

                case Severity.Error:
                case Severity.Alert:
                case Severity.Fatal:
                    return this.wtrSvc.LanguageLocalizer.LocalizeError();

                default:
                    return string.Empty;
            }
        }
    }
}

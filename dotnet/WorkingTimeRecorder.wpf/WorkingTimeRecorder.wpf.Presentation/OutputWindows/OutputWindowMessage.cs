using SharedLibraries.Logging;
using WorkingTimeRecorder.Core.Mvvm;

namespace WorkingTimeRecorder.wpf.Presentation.OutputWindows
{
    /// <summary>
    /// One message in output window.
    /// </summary>
    internal class OutputWindowMessage : ViewModelBase
    {
        private Severity severity = Severity.None;
        private string? text;

        /// <summary>
        /// Gets or sets a severity of the message.
        /// </summary>
        public Severity Severity
        {
            get => this.severity;
            set => this.SetProperty(ref this.severity, value);
        }

        /// <summary>
        /// Gets or sets a message text.
        /// </summary>
        public string Text
        {
            get => text ?? string.Empty;
            set => SetProperty(ref text, value);
        }
    }
}

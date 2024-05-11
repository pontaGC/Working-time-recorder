namespace WorkingTimeRecorder.wpf.Presentation.Core.Dialogs
{
    /// <summary>
    /// Represents the setting to custom a message box.
    /// </summary>
    public class MessageBoxSettings
    {
        /// <summary>
        /// Gets or sets a text of the affirmative button, e.g. "OK", Yes".
        /// </summary>
        public string? AffirmativeButtonText { get; set; }

        /// <summary>
        /// Gets or sets a text of the negative button, e.g. "Cancel", "No".
        /// </summary>
        public string? NegativeButtonText { get; set; }
    }
}

using SharedLibraries.Logging;

namespace WorkingTimeRecorder.wpf.Presentation.Core.OutputWindows
{
    /// <summary>
    /// Responsible for controlling output window.
    /// </summary>
    public interface IOutputWindowController
    {
        #region Methods

        /// <summary>
        /// Shows output window.
        /// </summary>
        void ShowWindow();

        /// <summary>
        /// Closes output window.
        /// </summary>
        /// <returns><c>true</c>, if closing the tool window is successfully. Otherwise; <c>false</c>.</returns>
        bool CloseWindow();

        /// <summary>
        /// Appends the message.
        /// </summary>
        /// <param name="severity">The severity of the message.</param>
        /// <param name="message">The message to show.</param>
        void AppendMessage(Severity severity, string message);

        /// <summary>
        /// Appends the message.
        /// </summary>
        /// <param name="message">The message to show.</param>
        void AppendMessage(string message);

        /// <summary>
        /// Clears all log in the output window.
        /// </summary>
        void Clear();

        #endregion
    }
}

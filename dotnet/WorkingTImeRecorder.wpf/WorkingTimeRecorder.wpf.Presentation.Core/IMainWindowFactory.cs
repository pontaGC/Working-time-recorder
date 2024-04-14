using System.Windows;

namespace WorkingTimeRecorder.wpf.Presentation.Core
{
    /// <summary>
    /// Responsible for creating a main window.
    /// </summary>
    public interface IMainWindowFactory
    {
        /// <summary>
        /// Creates a main window.
        /// </summary>
        /// <returns>An instance of the main widnow.</returns>
        Window Create();
    }
}

using System.Windows;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTImeRecorder.wpf;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    internal sealed class MainWindowFactory : IMainWindowFactory
    {
        /// <inheritdoc />
        public Window Create()
        {
            var viewModel = new MainWindowViewModel();
            return new MainWindow()
            {
                DataContext = viewModel,
            };
        }
    }
}

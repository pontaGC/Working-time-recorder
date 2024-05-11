using System.Windows;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    internal sealed class MainWindowFactory : IMainWindowFactory
    {
        private readonly IDialogs dialogs;

        public MainWindowFactory(IDialogs dialogs)
        {
            this.dialogs = dialogs;
        }

        /// <inheritdoc />
        public Window Create()
        {
            var viewModel = new MainWindowViewModel(this.dialogs);
            return new MainWindow()
            {
                DataContext = viewModel,
            };
        }
    }
}

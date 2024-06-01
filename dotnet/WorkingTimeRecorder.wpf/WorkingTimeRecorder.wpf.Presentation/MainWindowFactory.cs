using System.Windows;

using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.wpf.Presentation.OutputWindows;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    internal sealed class MainWindowFactory : IMainWindowFactory
    {
        private readonly IWTRSystem wtrSystem;
        private readonly IDialogs dialogs;
        private readonly IOutputWindowRegistrar outputWindowRegistrar;

        public MainWindowFactory(IWTRSystem wtrSystem, IDialogs dialogs, IOutputWindowRegistrar outputWindowRegistrar)
        {
            this.wtrSystem = wtrSystem;
            this.dialogs = dialogs;
            this.outputWindowRegistrar = outputWindowRegistrar;
        }

        /// <inheritdoc />
        public Window Create()
        {
            var viewModel = new MainWindowViewModel(this.wtrSystem, this.dialogs)
            {
                OutputWindow = new OutputWindowViewModel(),
            };
            var mainWindow = new MainWindow()
            {
                DataContext = viewModel,
            };

            this.outputWindowRegistrar.Register(mainWindow.OutputWindow, viewModel.OutputWindow);

            return mainWindow;
        }
    }
}

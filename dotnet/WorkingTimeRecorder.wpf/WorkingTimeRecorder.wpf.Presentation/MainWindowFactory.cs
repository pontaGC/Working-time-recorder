using System.Windows;
using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Persistences;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.wpf.Presentation.OutputWindows;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    internal sealed class MainWindowFactory : IMainWindowFactory
    {
        private readonly IWTRSvc wtrSvc;
        private readonly IDialogs dialogs;
        private readonly IEntityPersistence<TaskCollection> taskCollectionPersistence;
        private readonly IOutputWindowRegistrar outputWindowRegistrar;

        public MainWindowFactory(
            IWTRSvc wtrSvc,
            IDialogs dialogs,
            IEntityPersistence<TaskCollection> taskCollectionPersitence,
            IOutputWindowRegistrar outputWindowRegistrar)
        {
            this.wtrSvc = wtrSvc;
            this.dialogs = dialogs;
            this.taskCollectionPersistence = taskCollectionPersitence;
            this.outputWindowRegistrar = outputWindowRegistrar;
        }

        /// <inheritdoc />
        public Window Create()
        {
            var viewModel = new MainWindowViewModel(this.wtrSvc, this.dialogs, this.taskCollectionPersistence)
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

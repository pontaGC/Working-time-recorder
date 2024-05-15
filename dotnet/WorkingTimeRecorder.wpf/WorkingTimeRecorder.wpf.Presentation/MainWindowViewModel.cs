using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.wpf.Presentation.TaskViews;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <summary>
    /// The view-model of the main window.
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogs dialogs;

        public MainWindowViewModel(IDialogs dialogs)
        {
            this.dialogs = dialogs;

            this.TaskList = new TaskViewModel();
            var dummyItem = new TaskItemViewModel()
            {
                No = 1,
                Name = "Dummy task",
            };
            this.TaskList.TaskItems.Add(dummyItem);

            this.DummyCommand = new DelegateCommand(this.DummyAction);
        }

        /// <summary>
        /// Gets a task list to record the working time.
        /// </summary>
        public TaskViewModel TaskList { get; }

        public ICommand DummyCommand { get; }

        private void DummyAction()
        {
        }
    }
}

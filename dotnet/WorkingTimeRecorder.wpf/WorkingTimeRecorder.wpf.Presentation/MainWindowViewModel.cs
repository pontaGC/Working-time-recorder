using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using WorkingTimeRecorder.Core.Models.Tasks;
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
        private readonly TaskItem dummyItemModel;

        public MainWindowViewModel(IDialogs dialogs)
        {
            this.dialogs = dialogs;

            this.TaskList = new TaskViewModel();
            this.dummyItemModel = new TaskItem()
            {
                Name = "Dummy task",
            };
            dummyItemModel.ElapsedWorkTime.SetHours(1);
            dummyItemModel.ElapsedWorkTime.SetHours(20);
            var dummyItem = new TaskItemViewModel(dummyItemModel)
            {
                No = 1,
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
            this.dummyItemModel.ElapsedWorkTime.IncrementHours();
            this.dummyItemModel.ElapsedWorkTime.IncrementMinutes();
        }
    }
}

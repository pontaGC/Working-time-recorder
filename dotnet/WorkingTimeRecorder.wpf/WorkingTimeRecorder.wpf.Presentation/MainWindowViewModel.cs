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
        private readonly TaskItem dummyItem;
        private readonly TaskItem dummyItem2;
        private readonly TaskItem dummyItem3;

        public MainWindowViewModel(IDialogs dialogs)
        {
            this.dialogs = dialogs;

            this.TaskList = new TaskViewModel();
            this.dummyItem = new TaskItem()
            {
                Name = "Dummy task",
            };
            this.dummyItem.ElapsedWorkTime.SetHours(1);
            this.dummyItem.ElapsedWorkTime.SetHours(20);
            var dummyItem = new TaskItemViewModel(this.dummyItem)
            {
                No = 1,
                IsRecordingTarget = true,
            };

            this.dummyItem2 = new TaskItem()
            {
                Name = "Dummy task2",
            };
            this.dummyItem.ElapsedWorkTime.SetHours(1);
            this.dummyItem.ElapsedWorkTime.SetHours(20);
            var dummyItem2vm = new TaskItemViewModel(this.dummyItem2)
            {
                No = 2,
            };

            this.dummyItem3 = new TaskItem()
            {
                Name = "Dummy task3",
            };
            this.dummyItem.ElapsedWorkTime.SetHours(1);
            this.dummyItem.ElapsedWorkTime.SetHours(20);
            var dummyItem3vm = new TaskItemViewModel(this.dummyItem3)
            {
                No = 3,
            };

            this.TaskList.TaskItems.Add(dummyItem);
            this.TaskList.TaskItems.Add(dummyItem2vm);
            this.TaskList.TaskItems.Add(dummyItem3vm);

            this.DummyCommand = new DelegateCommand(this.DummyAction);
        }

        /// <summary>
        /// Gets a task list to record the working time.
        /// </summary>
        public TaskViewModel TaskList { get; }

        public ICommand DummyCommand { get; }

        private void DummyAction()
        {
            this.dummyItem.ElapsedWorkTime.IncrementHours();
            this.dummyItem.ElapsedWorkTime.IncrementMinutes();

            var recordingTargets = this.TaskList.TaskItems.Where(x => x.IsRecordingTarget).ToArray();
        }
    }
}

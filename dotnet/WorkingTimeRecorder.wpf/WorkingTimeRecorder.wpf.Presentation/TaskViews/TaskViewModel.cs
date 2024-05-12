using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTimeRecorder.Core.Mvvm;

namespace WorkingTimeRecorder.wpf.Presentation.TaskViews
{
    internal class TaskViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets a collection of the task item.
        /// </summary>
        public ObservableCollection<TaskItemViewModel> TaskItems { get; } = new ObservableCollection<TaskItemViewModel>();
    }
}

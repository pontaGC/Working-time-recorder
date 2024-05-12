using WorkingTimeRecorder.Core.Mvvm;

namespace WorkingTimeRecorder.wpf.Presentation.TaskViews
{
    /// <summary>
    /// The task item view-model.
    /// </summary>
    internal class TaskItemViewModel : ViewModelBase
    {
        #region Fields

        private int no;
        private string name = string.Empty;
        private string workingTime = TaskConstants.ZeroWorkingTime;
        private string manHours = TaskConstants.ZeroMonHours;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a task number.
        /// </summary>
        public int No
        {
            get => this.no;
            set => this.SetProperty(ref this.no, value);
        }

        /// <summary>
        /// Gets or sets a task name.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets a working time (HH:MM:SS) of this task.
        /// </summary>
        public string WorkingTime
        {
            get => this.workingTime;
            set => this.SetProperty(ref this.workingTime, value);
        }

        /// <summary>
        /// Gets or sets man-hours of this task.
        /// </summary>
        public string MonHours
        {
            get => this.manHours;
            set => this.SetProperty(ref this.manHours, value);
        }

        #endregion
    }
}

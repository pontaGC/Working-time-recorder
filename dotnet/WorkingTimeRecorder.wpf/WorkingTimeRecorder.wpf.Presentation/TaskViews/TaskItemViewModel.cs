using WorkingTimeRecorder.Core.Models.Tasks;
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
        private string elapsedWorkTime = TaskConstants.ZeroWorkingTime;
        private double manHours = TaskConstants.ZeroMonHours;

        private readonly ITaskItem model;

        #endregion

        #region Constructors

        public TaskItemViewModel(ITaskItem model)
        {
            ArgumentNullException.ThrowIfNull(model);

            this.model = model;
            this.Name = model.Name;
            this.ElapsedWorkTime = GetDisplayElapsedWorkTime(model.ElapsedWorkTime.Hours, model.ElapsedWorkTime.Miniutes);

            this.model.ElapsedWorkTime.ChangedEvent += this.OnElapsedWorkTimeChanged;
        }

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
        /// Gets or sets an elapsed work time (HH:MM:SS) of this task.
        /// </summary>
        public string ElapsedWorkTime
        {
            get => this.elapsedWorkTime;
            set => this.SetProperty(ref this.elapsedWorkTime, value);
        }

        /// <summary>
        /// Gets or sets man-hours of this task.
        /// </summary>
        public double ManHours
        {
            get => this.manHours;
            set => this.SetProperty(ref this.manHours, value);
        }

        #endregion

        #region Private Methods

        #region Event Handlers

        private void OnElapsedWorkTimeChanged(object? sender, ElapsedWorkTimeChangedEventArgs e)
        {
            this.ElapsedWorkTime = GetDisplayElapsedWorkTime(e.After.Hours, e.After.Miniutes);
        }

        #endregion

        private static string GetDisplayElapsedWorkTime(uint hours, uint minutes)
        {
            return $"{hours}:{minutes:D2}";
        }

        #endregion
    }
}

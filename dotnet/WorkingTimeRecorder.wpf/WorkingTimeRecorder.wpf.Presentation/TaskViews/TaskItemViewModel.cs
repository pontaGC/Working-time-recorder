using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.Core.Rules.Property;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.wpf.Presentation.TaskViews
{
    /// <summary>
    /// The task item view-model.
    /// </summary>
    internal class TaskItemViewModel : ValidationViewModelBase<TaskItemViewModel>
    {
        #region Fields

        private int no;
        private bool isRecordingTarget;
        private string name = string.Empty;
        private string elapsedWorkTime = TaskConstants.ZeroWorkingTime;
        private double manHours = TaskConstants.ZeroMonHours;

        private readonly IWTRSvc wtrSvc;
        private readonly ITaskItem model;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemViewModel"/> class.
        /// </summary>
        /// <param name="wtrSvc">The wtr service.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="wtrSvc"/> or <paramref name="model"/> is <c>null</c>.</exception>
        public TaskItemViewModel(
            IWTRSvc wtrSvc,
            ITaskItem model)
        {
            ArgumentNullException.ThrowIfNull(wtrSvc);
            ArgumentNullException.ThrowIfNull(model);

            this.wtrSvc = wtrSvc;

            this.model = model;
            this.Name = model.Name;
            this.ElapsedWorkTime = GetDisplayElapsedWorkTime(model.ElapsedWorkTime.Hours, model.ElapsedWorkTime.Miniutes);
            this.ManHours = model.ManHours;

            this.model.ElapsedWorkTime.ChangedEvent += this.OnElapsedWorkTimeChanged;

            this.RegisterRules();
            this.ValidateAllProperties();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets ID of the model.
        /// </summary>
        public string Id => this.model.Id;

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
            set 
            {
                if (this.SetProperty(ref this.name, value))
                {
                    this.model.Name = value;
                }
            }
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

        /// <summary>
        /// Gets or sets a value indicating this task item is the target which is the subject to elapsed time recording.
        /// </summary>
        public bool IsRecordingTarget
        {
            get => this.isRecordingTarget;
            set => this.SetProperty(ref this.isRecordingTarget, value);
        }

        #endregion

        #region Private Methods

        #region Event Handlers

        private void OnElapsedWorkTimeChanged(object? sender, ElapsedWorkTimeChangedEventArgs e)
        {
            this.ElapsedWorkTime = GetDisplayElapsedWorkTime(e.After.Hours, e.After.Miniutes);
            this.ManHours = e.After.ConvertToManHours(Tasks.PersonDay);
        }

        #endregion

        private static string GetDisplayElapsedWorkTime(uint hours, uint minutes)
        {
            return $"{hours}:{minutes:D2}";
        }

        private void RegisterRules()
        {
            this.RegisterNameRules();
        }

        private void RegisterNameRules()
        {
            var propertyName = nameof(this.Name);
            var languageLocalizer = this.wtrSvc.LanguageLocalizer;

            this.AddRule(
                new RequiredTextFieldPropertyRule<TaskItemViewModel>(
                    languageLocalizer,
                    propertyName,
                    x => x.Name));

            this.AddRule(
                new StringLengthPropertyRule<TaskItemViewModel>(
                    languageLocalizer,
                    propertyName,
                    x => x.Name,
                    TaskConstants.MaxNameLength));
        }

        #endregion
    }
}

namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The elapsed work time of a task.
    /// </summary>
    [Serializable]
    public class ElapsedWorkTime : IElapsedWorkTime
    {
        #region Fields

        private uint hours;
        private uint minutes;

        #endregion

        #region Events

        /// <inheritdoc />
        public event EventHandler<ElapsedWorkTimeChangedEventArgs> ChangedEvent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ElapsedWorkTime"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="taskId"/> is <c>null</c>.</exception>
        public ElapsedWorkTime(string taskId)
        {
            this.SetTaskId(taskId);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public string TaskId { get; private set; } = string.Empty;

        /// <inheritdoc />
        public uint Hours
        { 
            get => this.hours;
            private set => this.hours = value;
        }

        /// <inheritdoc />
        public uint Miniutes 
        {
            get => this.minutes;
            private set => this.minutes = value;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public bool SetTaskId(string newValue)
        {
            ArgumentNullException.ThrowIfNull(newValue);

            if (this.TaskId == newValue)
            {
                return false;
            }

            this.TaskId = newValue;
            return true;
        }

        /// <inheritdoc />
        public bool SetHours(uint newValue)
        {
            return this.SetTimeProperty(ref this.hours, newValue);
        }

        /// <inheritdoc />
        public bool SetMiniutes(uint newValue)
        {
            return this.SetTimeProperty(ref this.minutes, newValue);
        }

        /// <inheritdoc />
        public void IncrementHours()
        {
            this.IncrementTimeProperty(ref this.hours);
        }

        /// <inheritdoc />
        public void IncrementMinutes()
        {
            this.IncrementTimeProperty(ref this.minutes);
        }

        #endregion

        #region Private Methods

        private bool SetTimeProperty(ref uint backingStore, uint newValue)
        {
            if (backingStore == newValue)
            {
                return false;
            }

            var before = new ElapsedWorkTimeValueObject(this);
            backingStore = newValue;
            var after = this;

            this.RaiseChangedEvent(new ElapsedWorkTimeChangedEventArgs(before, after));
            return true;
        }

        private void IncrementTimeProperty(ref uint backingStore)
        {
            var before = new ElapsedWorkTimeValueObject(this);
            backingStore += 1;
            var after = this;

            this.RaiseChangedEvent(new ElapsedWorkTimeChangedEventArgs(before, after));
        }

        private void RaiseChangedEvent(ElapsedWorkTimeChangedEventArgs args)
        {
            this.ChangedEvent?.Invoke(this, args);
        }

        #endregion
    }
}

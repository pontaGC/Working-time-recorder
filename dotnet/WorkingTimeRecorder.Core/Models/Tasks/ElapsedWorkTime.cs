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

        #region Properties

        /// <inheritdoc />
        public string TaskId { get; set; } = string.Empty;

        /// <inheritdoc />
        public uint Hours
        { 
            get => this.hours;
            set => this.SetValueToField(ref this.hours, value);
        }

        /// <inheritdoc />
        public uint Miniutes 
        {
            get => this.minutes;
            set => this.SetValueToField(ref this.minutes, value);
        }

        #endregion

        #region Private Methods

        private bool SetValueToField(ref uint field, uint newValue)
        {
            if (field == newValue)
            {
                return false;
            }

            var before = new ElapsedWorkTimeValueObject(this);
            field = newValue;
            var after = new ElapsedWorkTimeValueObject(this);

            this.RaiseChangedEvent(new ElapsedWorkTimeChangedEventArgs(before, after));

            return true;
        }

        private void RaiseChangedEvent(ElapsedWorkTimeChangedEventArgs args)
        {
            this.ChangedEvent?.Invoke(this, args);
        }

        #endregion
    }
}

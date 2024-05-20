namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The elapsed work time of a task.
    /// </summary>
    public interface IElapsedWorkTime
    {
        #region Events

        /// <summary>
        /// This event occurrs when the elapsed work time is changed.
        /// </summary>
        public event EventHandler<ElapsedWorkTimeChangedEventArgs> ChangedEvent;

        #endregion

        /// <summary>
        /// Gets a task ID.
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// Gets or sets the hours part of the elapsed work time.
        /// </summary>
        public uint Hours { get; set; }

        /// <summary>
        /// Gets or sets the minutes part of the elapsed work time.
        /// </summary>
        public uint Miniutes { get; set; }
    }
}

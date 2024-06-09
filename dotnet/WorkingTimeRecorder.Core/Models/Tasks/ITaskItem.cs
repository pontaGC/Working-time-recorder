namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// A task item to record working time.
    /// </summary>
    public interface ITaskItem
    {
        #region Propeties

        /// <summary>
        /// Gets a task ID.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets or sets a task name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an elapsed work time (HH:MM) of this task.
        /// </summary>
        public IElapsedWorkTime ElapsedWorkTime { get; set; }

        /// <summary>
        /// Gets a man-hours of this task.
        /// </summary>
        public double ManHours { get; }

        #endregion
    }
}

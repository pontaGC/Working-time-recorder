namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The readable elapsed work time.
    /// </summary>
    public interface IReadOnlyElapsedWorkTIme
    {
        #region Properties

        /// <summary>
        /// Gets a task ID.
        /// </summary>
        public string TaskId { get; }

        /// <summary>
        /// Gets the hours part of the elapsed work time.
        /// </summary>
        public uint Hours { get; }

        /// <summary>
        /// Gets the minutes part of the elapsed work time.
        /// </summary>
        public uint Miniutes { get; }

        #endregion
    }
}

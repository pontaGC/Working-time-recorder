namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// Responsible for getting or setting an elapsed work time of a task.
    /// </summary>
    public interface IElapsedWorkTime : IReadOnlyElapsedWorkTIme
    {
        #region Events

        /// <summary>
        /// This event occurrs when the elapsed work time is changed.
        /// </summary>
        event EventHandler<ElapsedWorkTimeChangedEventArgs> ChangedEvent;

        #endregion

        #region Methods

        /// <summary>
        /// Sets a task ID.
        /// </summary>
        /// <param name="newValue">The setting value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="newValue"/> is <c>null</c>.</exception>
        /// <returns><c>true</c> if the given value is set, otherwise, <c>false</c>.</returns>
        bool SetTaskId(string newValue);

        /// <summary>
        /// Sets a hours part of the elapsed work time.
        /// </summary>
        /// <param name="newValue">The setting value.</param>
        /// <returns><c>true</c> if the given value is set, otherwise, <c>false</c>.</returns>
        bool SetHours(uint newValue);

        /// <summary>
        /// sets the minutes part of the elapsed work time.
        /// </summary>
        /// <param name="newValue">The setting value.</param>
        /// <returns><c>true</c> if the given value is set, otherwise, <c>false</c>.</returns>
        bool SetMiniutes(uint newValue);

        /// <summary>
        /// Added one hour.
        /// </summary>
        void IncrementHours();

        /// <summary>
        /// Added one minute.
        /// </summary>
        void IncrementMinutes();

        #endregion
    }
}

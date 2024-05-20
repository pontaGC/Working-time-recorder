namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The snapshot of the <see cref="ElapsedWorkTime"/>.
    /// </summary>
    public class ElapsedWorkTimeValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElapsedWorkTimeValueObject"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <exception cref="ArgumentNullException"><paramref name="taskId"/> or <paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="taskId"/> is an empty string.</exception>
        public ElapsedWorkTimeValueObject(ElapsedWorkTime source)
        {
            ArgumentNullException.ThrowIfNull(source);

            this.TaskId = source.TaskId;
            this.Hours = source.Hours;
            this.Miniuts = source.Miniutes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElapsedWorkTimeValueObject"/> class.
        /// </summary>
        /// <param name="taskId">The task ID.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <exception cref="ArgumentNullException"><paramref name="taskId"/> or <paramref name="source"/> is <c>null</c>.</exception>
        public ElapsedWorkTimeValueObject(string taskId, uint hours, uint minutes)
        {
            ArgumentException.ThrowIfNullOrEmpty(taskId);

            this.TaskId = taskId;
            this.Hours = hours;
            this.Miniuts = minutes;
        }

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
        public uint Miniuts { get; }
    }
}

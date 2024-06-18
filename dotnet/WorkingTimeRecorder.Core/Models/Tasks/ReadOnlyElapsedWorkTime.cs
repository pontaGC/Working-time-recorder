﻿namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The snapshot of the <see cref="ElapsedWorkTime"/>.
    /// </summary>
    public class ReadOnlyElapsedWorkTime : IReadOnlyElapsedWorkTIme
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyElapsedWorkTime"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public ReadOnlyElapsedWorkTime(ElapsedWorkTime source)
        {
            ArgumentNullException.ThrowIfNull(source);

            this.TaskId = source.TaskId;
            this.Hours = source.Hours;
            this.Miniutes = source.Miniutes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyElapsedWorkTime"/> class.
        /// </summary>
        /// <param name="taskId">The task ID.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <exception cref="ArgumentNullException"><paramref name="taskId"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="taskId"/> is an empty string.</exception>
        public ReadOnlyElapsedWorkTime(string taskId, uint hours, uint minutes)
        {
            ArgumentException.ThrowIfNullOrEmpty(taskId);

            this.TaskId = taskId;
            this.Hours = hours;
            this.Miniutes = minutes;
        }

        /// <inheritdoc />
        public string TaskId { get; }

        /// <inheritdoc />
        public uint Hours { get; }

        /// <inheritdoc />
        public uint Miniutes { get; }
    }
}
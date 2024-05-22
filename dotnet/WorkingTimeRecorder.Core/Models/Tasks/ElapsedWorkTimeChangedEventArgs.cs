using System.Diagnostics.CodeAnalysis;

namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The elapsed work time changed event args.
    /// </summary>
    public class ElapsedWorkTimeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElapsedWorkTimeChangedEventArgs"/> class.
        /// </summary>
        /// <param name="before">The before form.</param>
        /// <param name="after">The after form.</param>
        public ElapsedWorkTimeChangedEventArgs(
            IReadOnlyElapsedWorkTIme before,
            IReadOnlyElapsedWorkTIme after)
        {
            ArgumentNullException.ThrowIfNull(before);
            ArgumentNullException.ThrowIfNull(after);

            this.Before = before;
            this.After = after;
        }

        /// <summary>
        /// Gets the elapsed work time before changing.
        /// </summary>
        [NotNull]
        public IReadOnlyElapsedWorkTIme Before { get; }

        /// <summary>
        /// Gets the elapsed work time after changing.
        /// </summary>
        [NotNull]
        public IReadOnlyElapsedWorkTIme After { get; }
    }
}

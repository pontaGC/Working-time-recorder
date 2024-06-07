namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The args of the event that occurrs when the value of man-hours per person day.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    public class PersonDayChangedEventArgs(double oldValue, double newValue) : EventArgs
    {
        /// <summary>
        /// Gets a old value.
        /// </summary>
        public double OldValue { get; } = oldValue;

        /// <summary>
        /// Gets a new value.
        /// </summary>
        public double NewValue { get; } = newValue;
    }
}

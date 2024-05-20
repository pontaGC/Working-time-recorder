using System.Diagnostics.CodeAnalysis;

namespace WorkingTimeRecorder.Core.Models.Tasks
{
    public class ElapsedWorkTimeChangedEventArgs : EventArgs
    {
        public ElapsedWorkTimeChangedEventArgs(
            ElapsedWorkTimeValueObject before,
            ElapsedWorkTimeValueObject after)
        {
            ArgumentNullException.ThrowIfNull(before);
            ArgumentNullException.ThrowIfNull(after);

            this.Before = before;
            this.After = after;
        }

        [NotNull]
        public ElapsedWorkTimeValueObject Before { get; }

        [NotNull]
        public ElapsedWorkTimeValueObject After { get; }
    }
}

using System.Timers;
using Timer = System.Timers.Timer;

namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The elapsed work time of a task.
    /// </summary>
    [Serializable]
    public class ElapsedWorkTime : IElapsedWorkTime
    {
        #region Fields

        private const int TimerInterval = 30000; // 30 seconds

        private uint hours;
        private uint minutes;
        private double currentElapsedMinutes = 0;
        private Timer timer;

        #endregion

        #region Events

        /// <inheritdoc />
        public event EventHandler<ElapsedWorkTimeChangedEventArgs> ChangedEvent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ElapsedWorkTime"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="taskId"/> is <c>null</c>.</exception>
        public ElapsedWorkTime(string taskId)
        {
            this.SetTaskId(taskId);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public bool IsRunning { get; private set; }

        /// <inheritdoc />
        public string TaskId { get; private set; } = string.Empty;

        /// <inheritdoc />
        public uint Hours
        { 
            get => this.hours;
            private set => this.hours = value;
        }

        /// <inheritdoc />
        public uint Miniutes 
        {
            get => this.minutes;
            private set => this.minutes = value;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public bool SetTaskId(string newValue)
        {
            ArgumentNullException.ThrowIfNull(newValue);

            if (this.TaskId == newValue)
            {
                return false;
            }

            this.TaskId = newValue;
            return true;
        }

        /// <inheritdoc />
        public bool SetHours(uint newValue)
        {
            return this.SetTimeProperty(ref this.hours, newValue);
        }

        /// <inheritdoc />
        public bool SetMiniutes(uint newValue)
        {
            return this.SetTimeProperty(ref this.minutes, newValue);
        }

        /// <inheritdoc />
        public void IncrementHours()
        {
            this.IncrementTimeProperty(ref this.hours);
        }

        /// <inheritdoc />
        public void IncrementMinutes()
        {
            this.IncrementTimeProperty(ref this.minutes);
        }

        /// <inheritdoc />
        public void StartMesuringTime()
        {
            if (this.IsRunning)
            {
                // Already started
                return;
            }

            // Initialization
            this.IsRunning = true;
            this.currentElapsedMinutes = 0;

            this.timer = new Timer(TimerInterval)
            {
                AutoReset = true,
                Enabled = true,
            };
            this.timer.Elapsed += this.OnTimeElapsed;

            this.timer.Start();
        }

        /// <inheritdoc />
        public void StopMesuringTime()
        {
            if (!this.IsRunning)
            {
                // Mesuring time is not done
                return;
            }

            // Finalization
            this.timer.Stop();
            this.timer.Elapsed -= this.OnTimeElapsed;
            this.timer.Close();

            this.IsRunning = false;
        }

        #endregion

        #region Private Methods

        private bool SetTimeProperty(ref uint backingStore, uint newValue)
        {
            if (backingStore == newValue)
            {
                return false;
            }

            var before = new ReadOnlyElapsedWorkTime(this);
            backingStore = newValue;
            var after = new ReadOnlyElapsedWorkTime(this);

            this.RaiseChangedEvent(new ElapsedWorkTimeChangedEventArgs(before, after));
            return true;
        }

        private void IncrementTimeProperty(ref uint backingStore)
        {
            var before = new ReadOnlyElapsedWorkTime(this);
            backingStore += 1;
            var after = new ReadOnlyElapsedWorkTime(this);

            this.RaiseChangedEvent(new ElapsedWorkTimeChangedEventArgs(before, after));
        }

        private void OnTimeElapsed(object? sender, ElapsedEventArgs args)
        {
            const double OneMinuteSeconds = 60;

            var before = new ReadOnlyElapsedWorkTime(this);

            var intervalMinutes = (TimerInterval / 1000) / OneMinuteSeconds;
            this.currentElapsedMinutes += intervalMinutes;

            var currentElapsedTime = ConvertMinutesToHoursAndMinutes((uint)this.currentElapsedMinutes);
            this.Hours += currentElapsedTime.Hours;
            this.Miniutes += currentElapsedTime.Miniutes;

            var after = new ReadOnlyElapsedWorkTime(this);

            this.RaiseChangedEvent(new ElapsedWorkTimeChangedEventArgs(before, after));
        }

        private void RaiseChangedEvent(ElapsedWorkTimeChangedEventArgs args)
        {
            this.ChangedEvent?.Invoke(this, args);
        }

        private static (uint Hours, uint Miniutes) ConvertMinutesToHoursAndMinutes(uint minutes)
        {
            const int OneHourMinutes = 60;
            return (minutes / OneHourMinutes, minutes % OneHourMinutes);
        }

        #endregion
    }
}

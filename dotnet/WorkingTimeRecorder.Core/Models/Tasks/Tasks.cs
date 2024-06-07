using System.Diagnostics;

using WorkingTimeRecorder.Core.Models.Entities;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The tasks to record working time.
    /// </summary>
    [Serializable]
    public class Tasks : Entity
    {
        #region Fields

        private static double? personDay;

        #endregion

        #region Events

        /// <summary>
        /// The event occurs when the value of man-hours per person day is changed.
        /// </summary>
        public static event EventHandler<PersonDayChangedEventArgs>? PersonDayChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a man-hours per person-day.
        /// </summary>
        public static double PersonDay
        {
            get
            {
                Debug.Assert(personDay.HasValue, "The man-hours per person-day has been not set.");
                return personDay ?? ManHoursConstants.DefaultPersonDay;
            }
            set => personDay = value;
        }

        /// <inheritdoc />
        public override string Id { get; protected set; }

        /// <summary>
        /// Gets a collection of the task item.
        /// </summary>
        public IList<TaskItem> Items { get; set; } = new List<TaskItem>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Raises <see cref="PersonDayChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The event args.</param>
        public static void RaisePersonDayChangedEvent(object? sender, PersonDayChangedEventArgs args)
        {
            Debug.Assert(args is not null);
            PersonDayChanged?.Invoke(sender, args);
        }

        #endregion
    }
}

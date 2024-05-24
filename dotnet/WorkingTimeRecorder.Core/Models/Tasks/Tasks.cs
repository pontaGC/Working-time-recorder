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
        private static double? personDay;

        /// <summary>
        /// Gets a man-hours per person-day.
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
    }
}

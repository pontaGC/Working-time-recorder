using WorkingTimeRecorder.Core.Models.Entities;

namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The tasks to record working time.
    /// </summary>
    [Serializable]
    public class Tasks : Entity
    {
        /// <summary>
        /// Gets a man-hours per person-day.
        /// </summary>
        public static double PersonDay { get; set; }

        /// <inheritdoc />
        public override string Id { get; protected set; }

        /// <summary>
        /// Gets a collection of the task item.
        /// </summary>
        public IList<TaskItem> Items { get; set; } = new List<TaskItem>();
    }
}

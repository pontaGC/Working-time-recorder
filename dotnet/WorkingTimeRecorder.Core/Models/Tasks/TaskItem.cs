using WorkingTimeRecorder.Core.Models.Entities;

namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The task item to record working time.
    /// </summary>
    public class TaskItem : Entity, ITaskItem
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItem"/> class.
        /// </summary>
        public TaskItem()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItem"/> class.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <c>null</c>.</exception>
        public TaskItem(string id)
        {
            ArgumentNullException.ThrowIfNull(id);

            this.Id = id;
            this.ElapsedWorkTime = new ElapsedWorkTime(id);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public override string Id { get; protected set; }

        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public IElapsedWorkTime ElapsedWorkTime { get; set; }

        /// <inheritdoc />
        public double ManHours { get; set; }

        #endregion
    }
}

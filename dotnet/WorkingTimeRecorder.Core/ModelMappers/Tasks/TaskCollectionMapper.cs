using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Models.Tasks.Serializable;

namespace WorkingTimeRecorder.Core.Mappers.Models
{
    /// <summary>
    /// The task collection entity mapper.
    /// </summary>
    internal class TaskCollectionMapper : IEntityMapper<TaskCollection, TaskCollectionV>
    {
        #region Fields

        private readonly IEntityMapper<TaskItem, TaskItemV> taskItemMapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskCollectionMapper"/> class.
        /// </summary>
        /// <param name="taskItemMapper">The task item mapper.</param>
        /// <exception cref="ArgumentNullException"><paramref name="taskItemMapper"/> is <c>null</c>.</exception>
        public TaskCollectionMapper(IEntityMapper<TaskItem, TaskItemV> taskItemMapper)
        {
            ArgumentNullException.ThrowIfNull(taskItemMapper);

            this.taskItemMapper = taskItemMapper;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public TaskCollectionV Map(TaskCollection entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var result = new TaskCollectionV
            {
                Id = entity.Id,
            };

            if (entity.Items is null)
            {
                return result;
            }

            var taskItems = new List<TaskItemV>();
            foreach (var item in entity.Items.Where(x => x is not null))
            {
                var mappedTaskItem = this.taskItemMapper.Map(item);
                taskItems.Add(mappedTaskItem);
            }

            result.Items = taskItems;
            return result;
        }

        /// <inheritdoc />
        public TaskCollection MapBack(TaskCollectionV source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var entityId = string.IsNullOrEmpty(source.Id) ? Guid.NewGuid().ToString() : source.Id;
            var entity = new TaskCollection(entityId);
            if (source.Items is null)
            {
                return entity;
            }

            foreach (var item in source.Items.Where(x => x is not null))
            {
                var taskItem = this.taskItemMapper.MapBack(item);
                entity.Items.Add(taskItem);
            }

            return entity;
        }

        #endregion
    }
}

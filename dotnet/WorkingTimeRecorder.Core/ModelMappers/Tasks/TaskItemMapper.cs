using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Models.Tasks.Serializable;

namespace WorkingTimeRecorder.Core.Mappers.Models
{
    /// <summary>
    /// The task item entity mapper.
    /// </summary>
    internal class TaskItemMapper : IEntityMapper<TaskItem, TaskItemV>
    {
        /// <inheritdoc />
        public TaskItemV Map(TaskItem entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var result = new TaskItemV()
            {
                Id = entity.Id,
                Name = entity.Name,
                ElapsedWorkTime = new ElapsedWorkTimeV(),
            };

            var sourceWorkTime = entity.ElapsedWorkTime;
            if (sourceWorkTime is not null)
            {
                result.ElapsedWorkTime.Hours = sourceWorkTime.Hours;
                result.ElapsedWorkTime.Miniutes = sourceWorkTime.Miniutes;
            }

            return result;
        }

        /// <inheritdoc />
        public TaskItem MapBack(TaskItemV source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var taskId = string.IsNullOrEmpty(source.Id) ? Guid.NewGuid().ToString() : source.Id;
            var entity = new TaskItem(taskId)
            {
                Name = source.Name,
                ElapsedWorkTime = new ElapsedWorkTime(taskId)
            };

            var sourceWorkTime = source.ElapsedWorkTime;
            if (sourceWorkTime is not null)
            {
                entity.ElapsedWorkTime.SetHours(sourceWorkTime.Hours);
                entity.ElapsedWorkTime.SetMiniutes(sourceWorkTime.Miniutes);
            }

            return entity;
        }
    }
}

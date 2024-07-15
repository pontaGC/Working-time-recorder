using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Persistences;
using WorkingTimeRecorder.Core.Mappers;
using WorkingTimeRecorder.Core.Models.Tasks.Serializable;

namespace WorkingTimeRecorder.wpf.Implementation.Persistences.Tasks
{
    /// <summary>
    /// The task collection persistence.
    /// </summary>
    internal sealed class TaskCollectionPersistence : EntityPersistenceBase<TaskCollection>
    {
        #region Fields

        private readonly ConcurrentDictionary<string, TaskCollection> taskCollections = new ConcurrentDictionary<string, TaskCollection>();

        private readonly IEntityMapper<TaskCollection, TaskCollectionV> entityMapper;

        #endregion

        #region Constructors

        public TaskCollectionPersistence(IEntityMapper<TaskCollection, TaskCollectionV> entityMapper)
        {
            ArgumentNullException.ThrowIfNull(entityMapper);

            this.entityMapper = entityMapper;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override TaskCollection? GetById(string entityId)
        {
            if (string.IsNullOrEmpty(entityId))
            {
                return null;
            }

            if (!this.taskCollections.TryGetValue(entityId, out var taskCollection))
            {
                return null;
            }

            return taskCollection;
        }

        /// <inheritdoc />
        [return: NotNull]
        public override IEnumerable<TaskCollection> GetAll()
        {
            return this.taskCollections.Values;
        }

        /// <inheritdoc />
        public override bool Add(TaskCollection entity)
        {
            if (entity is null)
            {
                return false;
            }

            return this.taskCollections.TryAdd(entity.Id, entity);
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc />
        protected override TaskCollection Load([NotNull] Stream source)
        {
            var loadedEntity = LoadEntity(this.entityMapper, source);
            this.taskCollections.TryAdd(loadedEntity.Id, loadedEntity);
            return loadedEntity;
        }

        /// <inheritdoc />
        protected override void Save([NotNull] TaskCollection entity, [NotNull] Stream destination)
        {
            SaveEntity(this.entityMapper, entity, destination);
        }

        #endregion
    }
}

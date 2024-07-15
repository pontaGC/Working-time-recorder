using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using SharedLibraries.System.Xml;
using WorkingTimeRecorder.Core.Mappers;
using WorkingTimeRecorder.Core.Models.Entities;

namespace WorkingTimeRecorder.Core.Persistences
{
    /// <summary>
    /// The base implementation for <see cref="IEntityPersistence{TEntity}{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    internal abstract class EntityPersistenceBase<TEntity> : IEntityPersistence<TEntity>
        where TEntity : Entity
    {
        #region Explict interface implementation

        /// <inheritdoc />
        bool IEntityPersistence<TEntity>.CanLoad(Stream source)
        {
            if (source is null)
            {
                return false;
            }

            return this.CanLoad(source);
        }

        /// <inheritdoc />
        TEntity IEntityPersistence<TEntity>.Load(Stream source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return this.Load(source);
        }

        /// <inheritdoc />
        bool IEntityPersistence<TEntity>.CanSave(TEntity entity, Stream destination)
        {
            if (entity is null || destination is null)
            {
                return false;
            }

            return this.CanSave(entity, destination);
        }

        /// <inheritdoc />
        void IEntityPersistence<TEntity>.Save(TEntity entity, Stream destination)
        {
            ArgumentNullException.ThrowIfNull(entity);
            ArgumentNullException.ThrowIfNull(destination);

            this.Save(entity, destination);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public abstract TEntity? GetById(string entityId);

        /// <inheritdoc />
        [return: NotNull]
        public abstract IEnumerable<TEntity> GetAll();

        /// <inheritdoc />
        public abstract bool Add(TEntity entity);

        #endregion

        #region Protected Methods

        /// <summary>
        /// This method is the implementation of the <see cref="IPersistence{TEntity}.Load(Stream)"/>.
        /// </summary>
        protected abstract TEntity Load([NotNull] Stream source);

        /// <summary>
        /// This method is the implementation of the <see cref="IPersistence{TEntity}.Save(Stream)"/>.
        /// </summary>
        protected abstract void Save([NotNull] TEntity entity, [NotNull] Stream destination);

        /// <summary>
        /// This method is the implementation of the <see cref="IPersistence{TEntity}.CanLoad(Stream)"/>.
        /// </summary>
        protected virtual bool CanLoad([NotNull] Stream source)
        {
            if (!source.CanRead)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This method is the implementation of the <see cref="IPersistence{TEntity}.CanSave(TEntity, Stream)"/>.
        /// </summary>
        protected virtual bool CanSave([NotNull] TEntity entity, [NotNull] Stream destination)
        {
            if (!destination.CanWrite)
            {
                return false;
            }

            return true;
        }

        protected static T LoadEntity<T, TV>(IEntityMapper<T, TV> entityMapper, Stream source) 
            where T : TEntity
            where TV : class
        {
            ArgumentNullException.ThrowIfNull(entityMapper);
            ArgumentNullException.ThrowIfNull(source);

            var deserializeObject = XmlSerializerHelper.Deserialize<TV>(source);
            var loadedEntity = entityMapper.MapBack(deserializeObject);

            Debug.Assert(loadedEntity != null);            
            return loadedEntity;
        }

        protected static void SaveEntity<T, TV>(IEntityMapper<T, TV> entityMapper, T entity, Stream destination)
            where T : TEntity
            where TV : class
        {
            ArgumentNullException.ThrowIfNull(entityMapper);
            ArgumentNullException.ThrowIfNull(entity);
            ArgumentNullException.ThrowIfNull(destination);

            var serializeObject = entityMapper.Map(entity);
            XmlSerializerHelper.Serialze(serializeObject, destination);
        }

        #endregion
    }
}


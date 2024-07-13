using WorkingTimeRecorder.Core.Models.Entities;

namespace WorkingTimeRecorder.Core.Persistences
{
    /// <summary>
    /// The persister.
    /// </summary>
    public interface IEntityPersistence<TEntity> : IEntityRepository<TEntity>
        where TEntity : Entity
    {
        #region Methods

        /// <summary>
        /// Checks whether preconditions for loading an entity are satisfied or not.
        /// </summary>
        /// <param name="source">The stream to read.</param>
        /// <returns><c>true</c> if the preconditions are satisfied, otherwise, <c>false</c>.</returns>
        bool CanLoad(Stream source);

        /// <summary>
        /// Loads an entity from the given stream.
        /// </summary>
        /// <param name="source">The stream to read.</param>
        /// <returns>The loaded entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        TEntity Load(Stream source);

        /// <summary>
        /// Checks whether preconditions for saving an entity are satisfied or not.
        /// </summary>
        /// <param name="entity">The enity to save.</param>
        /// <param name="destination">The stream to write.</param>
        /// <returns><c>true</c> if the preconditions are satisfied, otherwise, <c>false</c>.</returns>
        bool CanSave(TEntity entity, Stream destination);

        /// <summary>
        /// Saves an entity to the target stream.
        /// </summary>
        /// <param name="entity">The enity to save.</param>
        /// <param name="destination">The stream to write.</param>
        /// <exception cref="ArgumentNullException"><paramref name="destination"/> is <c>null</c>.</exception>
        void Save(TEntity entity, Stream destination);

        #endregion
    }
}

using WorkingTimeRecorder.Core.Models.Entities;

namespace WorkingTimeRecorder.Core.Mappers
{
    /// <summary>
    /// Responsible for projecting an entity model onto another object.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    /// <typeparam name="T">The type of mapping object.</typeparam>
    public interface IEntityMapper<TEntity, T>
        where TEntity : Entity
        where T : class
    {
        /// <summary>
        /// Projects the given entity onto another object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The mapped object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is <c>null</c>.</exception>
        T Map(TEntity entity);

        /// <summary>
        /// Projects the given object onto the entity.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        TEntity MapBack(T source);
    }
}

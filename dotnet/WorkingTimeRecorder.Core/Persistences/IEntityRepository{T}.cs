using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using WorkingTimeRecorder.Core.Models.Entities;

namespace WorkingTimeRecorder.Core.Persistences
{
    /// <summary>
    /// The entity repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Gets an entity with the specified ID.
        /// </summary>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The instance of the entity with <paramref name="entityId"/> if found, otherwise, returns <c>null</c>.</returns>
        TEntity? GetById(string entityId);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The all entities.</returns>
        [return: NotNull]
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns><c>true</c> if the entity is added successfully, otherwise, <c>false</c>.</returns>
        bool Add(TEntity entity);
    }
}

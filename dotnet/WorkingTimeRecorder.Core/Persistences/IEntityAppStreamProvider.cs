using WorkingTimeRecorder.Core.Models.Entities;

namespace WorkingTimeRecorder.Core.Persistences
{
    /// <summary>
    /// Responsible for providing a stream to read or write the entity data as appllication data.
    /// </summary>
    public interface IEntityAppStreamProvider<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Gets an entity type.
        /// </summary>
        string EntityType { get; }

        /// <summary>
        /// Creates a stream to load the entity data.
        /// </summary>
        /// <returns>The stream to read.</returns>
        /// <remarks>Must be called using the try statement, as an exception may occur.</remarks>
        Stream CreateLoadStream();

        /// <summary>
        /// Creates a stream to save the entity data.
        /// </summary>
        /// <returns>The stream to write.</returns>
        /// <remarks>Must be called using the try statement, as an exception may occur.</remarks>
        Stream CreateSaveStream();
    }
}

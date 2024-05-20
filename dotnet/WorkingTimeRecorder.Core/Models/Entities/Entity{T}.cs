namespace WorkingTimeRecorder.Core.Models.Entities
{
    /// <summary>
    /// The entity.
    /// </summary>
    public abstract class Entity<TId>
    {
        /// <summary>
        /// Gets an entity ID.
        /// </summary>
        public abstract TId Id { get; protected set; }

        /// <summary>
        /// Sets a new entity ID if the new ID is not equal to the current ID.
        /// </summary>
        /// <param name="newId">The new ID to set.</param>
        /// <param name="idComparer">The ID equality comparer.</param>
        /// <returns><c>true</c> if <paramref name="newId"/> was set, otherwise, <c>false</c>.</returns>
        protected bool SetId(TId newId, IEqualityComparer<TId> idComparer = null)
        {
            var comparer = idComparer ?? EqualityComparer<TId>.Default;
            if (comparer.Equals(this.Id, newId))
            {
                return false;
            }

            this.Id = newId;
            return true;
        }
    }
}

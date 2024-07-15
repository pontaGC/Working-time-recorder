namespace WorkingTimeRecorder.Core.Persistences
{
    /// <summary>
    /// Responsible for persistencing the model used in application.
    /// </summary>
    public interface IApplicationPersistence
    {
        /// <summary>
        /// Loads all entities.
        /// </summary>
        void LoadAllEntities();

        /// <summary>
        /// Saves all entities.
        /// </summary>
        void SaveAllEntities();
    }
}

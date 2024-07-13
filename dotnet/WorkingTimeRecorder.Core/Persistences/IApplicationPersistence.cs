namespace WorkingTimeRecorder.Core.Persistences
{
    /// <summary>
    /// Responsible for persistencing the model used in application.
    /// </summary>
    public interface IApplicationPersistence
    {
        /// <summary>
        /// Saves all entities.
        /// </summary>
        void SaveAllEntities();
    }
}

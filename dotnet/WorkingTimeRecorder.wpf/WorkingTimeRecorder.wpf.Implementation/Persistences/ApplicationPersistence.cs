﻿using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Persistences;

namespace WorkingTimeRecorder.wpf.Implementation.Persistences
{
    internal sealed class ApplicationPersistence : IApplicationPersistence
    {
        #region Fields

        // HACK:
        // Improved design that does not change the constructor
        // even if the number of Entity types increases

        private readonly IEntityAppStreamProvider<TaskCollection> taskCollectionAppStreamProvider;
        private readonly IEntityPersistence<TaskCollection> taskCollectionPersistence;

        #endregion

        #region Constructors

        public ApplicationPersistence(
            IEntityAppStreamProvider<TaskCollection> taskCollectionAppStreamProvider,
            IEntityPersistence<TaskCollection> taskCollectionPersistence)
        {
            this.taskCollectionAppStreamProvider = taskCollectionAppStreamProvider;
            this.taskCollectionPersistence = taskCollectionPersistence;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public void LoadAllEntities()
        {
            try
            {
                this.LoadTaskCollection();
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void SaveAllEntities()
        {
            try
            {
                this.SaveTaskCollection();
            }
            catch 
            {
                throw;
            }
        }

        #endregion

        #region Private Methods

        private void LoadTaskCollection()
        {
            using(var stream = this.taskCollectionAppStreamProvider.CreateLoadStream())
            {
                if (this.taskCollectionPersistence.CanLoad(stream))
                {
                    this.taskCollectionPersistence.Load(stream);
                }
            }
        }

        private void SaveTaskCollection()
        {
            using(var stream = this.taskCollectionAppStreamProvider.CreateSaveStream())
            {
                foreach(var task in this.taskCollectionPersistence.GetAll())
                {
                    if (this.taskCollectionPersistence.CanSave(task, stream))
                    {
                        this.taskCollectionPersistence.Save(task, stream);
                    }
                }
            }
        }

        #endregion
    }
}
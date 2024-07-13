using WorkingTimeRecorder.Core.Injectors;
using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Persistences;

using WorkingTimeRecorder.wpf.Implementation.Persistences;
using WorkingTimeRecorder.wpf.Implementation.Persistences.Tasks;

namespace WorkingTimeRecorder.wpf.Implementation
{
    /// <inheritdoc />
    public sealed class WTRImplDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.Register<IApplicationPersistence, ApplicationPersistence>(InstanceLifeStyle.Singleton);

            RegisterTaskCollectionPersitence(container);
        }

        private static void RegisterTaskCollectionPersitence(IIoCContainer container)
        {
            container.Register<IEntityAppStreamProvider<TaskCollection>, TaskCollectionEntityAppStreamProvider>(InstanceLifeStyle.Singleton);
            container.Register<IEntityPersistence<TaskCollection>, TaskCollectionPersistence>(InstanceLifeStyle.Singleton);
        }
    }
}

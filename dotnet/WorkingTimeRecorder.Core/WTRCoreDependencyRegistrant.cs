using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.Core.Injectors;
using WorkingTimeRecorder.Core.Mappers.Models;
using WorkingTimeRecorder.Core.Mappers;
using WorkingTimeRecorder.Core.Models.Tasks.Serializable;
using WorkingTimeRecorder.Core.Models.Tasks;

namespace WorkingTimeRecorder.Core
{
    /// <inheritdoc />
    public sealed class WTRCoreDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.RegisterInstance<IWTRSvc>(WTRSvc.Instance);

            RegisterMappers(container);

            // Internal accessibility instances
            container.RegisterInstance(WTRSvc.Instance.LoggerRegistrar);
        }

        private static void RegisterMappers(IIoCContainer container)
        {
            container.Register<IEntityMapper<TaskItem, TaskItemV>, TaskItemMapper>(InstanceLifeStyle.Singleton);
            container.Register<IEntityMapper<TaskCollection, TaskCollectionV>, TaskCollectionMapper>(InstanceLifeStyle.Singleton);
        }
    }
}

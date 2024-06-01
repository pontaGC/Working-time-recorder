using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.Core.Injectors;

namespace WorkingTimeRecorder.Core
{
    /// <inheritdoc />
    public sealed class WTRCoreDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.RegisterInstance<IWTRSystem>(WTRSystem.Instance);

            // Internal accessibility instances
            container.RegisterInstance(WTRSystem.Instance.LoggerRegistrar);
        }
    }
}

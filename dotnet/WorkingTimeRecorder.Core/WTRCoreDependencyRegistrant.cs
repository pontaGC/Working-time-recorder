using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.Core.Injectors;
using WorkingTimeRecorder.Core.Languages;

namespace WorkingTimeRecorder.Core
{
    /// <inheritdoc />
    public sealed class WTRCoreDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.RegisterInstance<IWTRSystem>(WTRSystem.Instance);
            container.RegisterInstance(WTRSystem.Instance.CultureSetter);
        }
    }
}

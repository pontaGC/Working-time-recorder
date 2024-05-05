
using WorkingTimeRecorder.Core.Shared;
using WorkingTImeRecorder.Core.Injectors;

namespace WorkingTimeRecorder.Core
{
    /// <inheritdoc />
    public sealed class WTRCoreDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.RegisterInstance<IWTRSystem>(WTRSystem.Instance);
        }
    }
}

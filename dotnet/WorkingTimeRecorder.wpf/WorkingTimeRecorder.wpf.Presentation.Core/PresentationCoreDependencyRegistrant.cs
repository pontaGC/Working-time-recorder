using WorkingTimeRecorder.Core;
using WorkingTimeRecorder.Core.Injectors;

namespace WorkingTimeRecorder.wpf.Presentation.Core
{
    /// <inheritdoc />
    public sealed class PresentationCoreDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.RegisterAll<IModuleInitializer, ModuleInitializer>();
        }
    }
}

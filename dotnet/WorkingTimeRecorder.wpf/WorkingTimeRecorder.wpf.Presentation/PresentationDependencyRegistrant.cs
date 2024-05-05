using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTImeRecorder.Core.Injectors;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    public sealed class PresentationDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.Register<IMainWindowFactory, MainWindowFactory>(InstanceLifeStyle.Singleton);
        }
    }
}

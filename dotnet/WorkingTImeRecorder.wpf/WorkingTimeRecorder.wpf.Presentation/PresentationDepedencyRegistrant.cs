using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTImeRecorder.Core.Injectors;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    public sealed class PresentationDepedencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.Register<IMainWindowFactory, MainWindowFactory>(InstanceLifeStyle.Singleton);
        }
    }
}

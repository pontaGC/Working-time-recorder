using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.Core.Injectors;
using WorkingTimeRecorder.wpf.Presentation.Core.Icons;
using WorkingTimeRecorder.wpf.Presentation.Icons;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    public sealed class PresentationDependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IIoCContainer container)
        {
            container.Register<IMainWindowFactory, MainWindowFactory>(InstanceLifeStyle.Singleton);
            container.Register<IDialogs, Dialogs.Dialogs>(InstanceLifeStyle.Singleton);
            container.RegisterInstance<IIconService>(IconService.Instance);
        }
    }
}

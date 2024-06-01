using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.Core.Injectors;
using WorkingTimeRecorder.wpf.Presentation.Core.Icons;
using WorkingTimeRecorder.wpf.Presentation.Icons;
using SharedLibraries.Logging;
using WorkingTimeRecorder.wpf.Presentation.Logging;
using WorkingTimeRecorder.wpf.Presentation.Core.OutputWindows;
using WorkingTimeRecorder.wpf.Presentation.OutputWindows;

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

            RegisterOutputWindow(container);

            container.RegisterAll<ILoggerProvider, OutputWindowLoggerProvider>(InstanceLifeStyle.Transient);
        }

        private void RegisterOutputWindow(IIoCContainer container)
        {
            container.Register<IOutputWindowController, OutputWindowController>(InstanceLifeStyle.Singleton);
            container.Register<IOutputWindowRegistrar, OutputWindowController>(InstanceLifeStyle.Singleton);
        }
    }
}

using System.Windows;
using WorkingTimeRecorder.wpf.Presentation;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTImeRecorder.Core.Injectors;

namespace WorkingTImeRecorder.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = ContainerFactory.Create();
            RegisterDependencies(container, new PresentationDepedencyRegistrant());

            var mainWindowFactory = container.Resolve<IMainWindowFactory>();
            var mainWindow = mainWindowFactory.Create();
            mainWindow.ShowDialog();
        }

        private static void RegisterDependencies(IIoCContainer container, IDependenyRegistrant registrant)
        {
            registrant.Register(container);
        }
    }

}

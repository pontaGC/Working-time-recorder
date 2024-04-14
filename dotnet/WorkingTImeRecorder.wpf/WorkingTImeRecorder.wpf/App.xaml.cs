using System.Windows;
using WorkingTimeRecorder.wpf.Presentation.Core;

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
            var mainWindowFactory = new MainWindowFactoryMock();
            var mainWindow = mainWindowFactory.Create();
            mainWindow.ShowDialog();
        }

        private class MainWindowFactoryMock : IMainWindowFactory
        {
            public Window Create()
            {
                throw new NotImplementedException();
            }
        }
    }

}

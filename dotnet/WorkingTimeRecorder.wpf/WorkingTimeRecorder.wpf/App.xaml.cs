using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Threading;

using Microsoft.Extensions.Configuration;
using WorkingTimeRecorder.Core.Extensions;
using WorkingTimeRecorder.wpf.Presentation;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTimeRecorder.Core.Injectors;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.Core;
using WorkingTimeRecorder.Core.Languages;
using SharedLibraries.Extensions;

namespace WorkingTimeRecorder.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Constructors

        static App()
        {
            Config = ConfigurationFactory.Create();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an application configuration.
        /// </summary>
        public static IConfiguration Config { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            this.RegisterAppUnhandledExceptionHandler();

            var container = ContainerFactory.Create();
            RegisterDependencies(container, new WTRCoreDependencyRegistrant());
            RegisterDependencies(container, new PresentationDependencyRegistrant());

            var wtrSystem = container.Resolve<IWTRSystem>();
            SetCulture(wtrSystem.CultureSetter);

            var mainWindowFactory = container.Resolve<IMainWindowFactory>();
            var mainWindow = mainWindowFactory.Create();
            mainWindow.ShowDialog();
        }

        private static void RegisterDependencies(IIoCContainer container, IDependenyRegistrant registrant)
        {
            registrant.Register(container);
        }

        private static void SetCulture(ICultureSetter cultureSetter)
        {
            var appCulture = Config.GetCulture() ?? CultureNameConstants.DefaultCultureName; ;
            cultureSetter.SetCulture(appCulture);
        }

        #region Unhandled exception handlers

        private void RegisterAppUnhandledExceptionHandler()
        {
            // Unhandled exception caused in main thread (UI thread as WPF)
            DispatcherUnhandledException += this.OnAppDispatcherUnhandledException;

            // Unhandled exception caused in working threads
            TaskScheduler.UnobservedTaskException += this.OnUnobservedTaskException;

            AppDomain.CurrentDomain.UnhandledException += this.OnCurrentDomainUnhandledException;
        }

        private void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var localizer = WTRSystem.Instance.LanguageLocalizer;

            MessageBox.Show(
                GetUnhandledErrorMessage(localizer, e.Exception),
                localizer.LocalizeAppTitle(),
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            e.Handled = true;

            Environment.Exit(1);
        }

        private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            var localizer = WTRSystem.Instance.LanguageLocalizer;
            var exception = e.Exception.InnerException;

            MessageBox.Show(
                GetUnhandledErrorMessage(localizer, exception),
                localizer.LocalizeAppTitle(),
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            e.SetObserved();

            Environment.Exit(1);
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var localizer = WTRSystem.Instance.LanguageLocalizer;
            var exception = e.ExceptionObject as Exception;

            MessageBox.Show(
                GetUnhandledErrorMessage(localizer, exception),
                localizer.LocalizeAppTitle(),
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            Environment.Exit(1);
        }

        private static string GetUnhandledErrorMessage(ILanguageLocalizer localizer, [AllowNull]Exception ex)
        {
            var mainMessage = localizer.Localize(
                "WorkingTimeRecorder.UnhandledExceptionOccuurred",
                "The unexpected error has occurred.");
            if (string.IsNullOrEmpty(ex?.Message))
            {
                return mainMessage;
            }

            return $"{mainMessage}\r\n{ex.GetMessageWithInnerEx()}";
        }

        #endregion

        #endregion
    }

}

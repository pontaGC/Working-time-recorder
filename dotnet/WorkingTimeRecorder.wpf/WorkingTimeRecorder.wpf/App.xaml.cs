using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;

using SharedLibraries.Extensions;
using SharedLibraries.Logging;

using Microsoft.Extensions.Configuration;

using WorkingTimeRecorder.Core.Extensions;
using WorkingTimeRecorder.wpf.Presentation;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTimeRecorder.Core.Injectors;
using WorkingTimeRecorder.Core;
using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.Core.Configurations;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Fields

        private readonly IIoCContainer container;

        #endregion

        #region Constructors

        static App()
        {
            Config = ConfigurationFactory.Create(
                (exception) =>
                {
                    MessageBox.Show(
                        exception.GetMessageWithInnerEx(Environment.NewLine),
                        WTRTextKeys.DefaultTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Stop);
                });

            if (Config is null)
            {
                // Do not execute application.
                Environment.Exit(-1);
            }
        }

        public App()
        {
            this.container = ContainerFactory.Create();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an application configuration.
        /// </summary>
        public static IConfiguration Config { get; private set; }

        #endregion

        #region Methods

        #region Startup

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {         
            this.RegisterAppUnhandledExceptionHandler();

            RegisterDependencies(this.container, new WTRCoreDependencyRegistrant());
            RegisterDependencies(this.container, new PresentationDependencyRegistrant());

            RegisterLoggers(container);

            var appSettings = Config.GetAppSettings(this.ShowAppSettingsReadError);
            if (appSettings is null)
            {
                Environment.Exit(1);
                return;
            }

            var wtrSvc = this.container.Resolve<IWTRSvc>();
            SetAppSettings(appSettings, wtrSvc);

            var mainWindowFactory = container.Resolve<IMainWindowFactory>();
            var mainWindow = mainWindowFactory.Create();
            mainWindow.ShowDialog();
        }

        private static void RegisterDependencies(IIoCContainer container, IDependenyRegistrant registrant)
        {
            registrant.Register(container);
        }

        private static void RegisterLoggers(IIoCContainer container)
        {
            var loggerRegistrar = container.Resolve<ILoggerRegistrar>();

            foreach(var loggerProvider in container.GetAllInstances<ILoggerProvider>())
            {
                loggerRegistrar.Register(loggerProvider);
            }
        }

        private void ShowAppSettingsReadError(Exception errorException)
        {
            // Tries to load culture only in order to localize message
            try
            {
                var culture = Config.GetCulture();
                var cultureSetter = this.container.Resolve<ICultureSetter>();
                cultureSetter.SetCulture(culture);
            }
            catch { }

            var wtrSvc = this.container.Resolve<IWTRSvc>();
            var dialogs = this.container.Resolve<IDialogs>();

            var errorMessageFormat = wtrSvc.LanguageLocalizer.Localize(
                "WorkingTimeRecorder.appsettings.FailedToRead",
                "Failed to load appsettings.json. Unable to start application.\n\n{0}");
            var errorMessage = string.Format(
                CultureInfo.InvariantCulture,
                errorMessageFormat,
                errorException.GetMessageWithInnerEx(Environment.NewLine));

            dialogs.MessageBox.Show(
                errorMessage,
                wtrSvc.LanguageLocalizer.LocalizeAppTitle(),
                MessageBoxButtons.OK,
                MessageBoxImages.Error);
        }

        private static void SetAppSettings(AppSettings appSettings, IWTRSvc wtrSvc)
        {
            SetCulture(appSettings, wtrSvc.CultureSetter);
            SetManHoursPerPersonDay(appSettings, wtrSvc.LoggerCollection, wtrSvc.LanguageLocalizer);
        }

        private static void SetCulture(AppSettings appSettings, ICultureSetter cultureSetter)
        {
            var appCulture = appSettings.Culture ?? CultureNameConstants.DefaultCultureName;
            cultureSetter.SetCulture(appCulture);
        }

        private static void SetManHoursPerPersonDay(
            AppSettings appSettings,
            ILoggerCollection loggerCollection,
            ILanguageLocalizer languageLocalizer)
        {
            var personDay = appSettings.PersonDay;
            if (ManHoursConstants.MinimumPersonDay < personDay && personDay <= ManHoursConstants.MaxPersonDay)
            {
                Tasks.PersonDay = Math.Round(personDay, ManHoursConstants.NumberOfDecimalPoints);
                return;
            }

            Tasks.PersonDay = ManHoursConstants.DefaultPersonDay;

            // Notify of using default man-hours per person day
            var messageFormat = languageLocalizer.Localize(
                "WorkingTimeRecorder.appsettings.InvalidPersonDayFormat",
                "PersonDay value ({0}) in appsettings.json is invalid. The default value of man-hours per person day ({1}) is used.");
            var errorMessage = string.Format(
                CultureInfo.InvariantCulture,
                messageFormat,
                personDay,
                ManHoursConstants.DefaultPersonDay);

            var logger = loggerCollection.Resolve(LogConstants.OutputWindow);
            logger.Log(Severity.Warning, errorMessage);
        }

        #endregion

        #region Exit

        /// <inheritdoc />
        protected override void OnExit(ExitEventArgs e)
        {
            this.SaveAppSettngs();
            base.OnExit(e);
        }

        private void SaveAppSettngs()
        {
            var appSettings = Config.GetAppSettings(e => { });
            if (appSettings is null)
            {
                appSettings = new AppSettings()
                {
                    IsModified = true,
                    Culture = CultureNameConstants.DefaultCultureName,
                    PersonDay = ManHoursConstants.DefaultPersonDay,
                };
            }
            else
            {
                this.SetAppSettingsToSave(appSettings);
            }

            if (!appSettings.IsModified)
            {
                return;
            }

            try
            {
                var appSettingsFilePath = GetAppSettingsFilePath();
                appSettings.SaveFile(appSettingsFilePath, GetAppSettingsJsonSerializerOptions());
            }
            catch (AppSettingsSaveFailureException e)
            {
                var wtrSvc = this.container.Resolve<IWTRSvc>();
                MessageBox.Show(
                    e.GetMessageWithInnerEx(Environment.NewLine),
                    wtrSvc.LanguageLocalizer.LocalizeAppTitle(),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SetAppSettingsToSave(AppSettings appSettings)
        {
            var isModified = false;
            var wtrSvc = this.container.Resolve<IWTRSvc>();

            // Sets culture
            var nextCultureName = wtrSvc.CultureSetter.NextCultureName;
            if (string.IsNullOrEmpty(appSettings.Culture))
            {
                // Sets default culture just in case
                appSettings.Culture = nextCultureName ?? CultureNameConstants.DefaultCultureName;
                isModified = true;
            }
            else if (!string.IsNullOrEmpty(nextCultureName) && appSettings.Culture != nextCultureName)
            {
                appSettings.Culture = nextCultureName;
                isModified = true;
            }

            // Sets man-hours per person-day
            if (appSettings.PersonDay != Tasks.PersonDay)
            {
                appSettings.PersonDay = Tasks.PersonDay;
                isModified = true;
            }

            // Sets modified flag
            appSettings.IsModified = isModified;
        }

        private static JsonSerializerOptions GetAppSettingsJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
        }

        private static string GetAppSettingsFilePath()
        {
            var exeAssembly = Assembly.GetExecutingAssembly();
            var directoryPath = Path.GetDirectoryName(exeAssembly.Location);
            return Path.Combine(directoryPath, "appsettings.json");
        }

        #endregion

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
            var localizer = WTRSvc.Instance.LanguageLocalizer;

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
            var localizer = WTRSvc.Instance.LanguageLocalizer;
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
            var localizer = WTRSvc.Instance.LanguageLocalizer;
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

            return $"{mainMessage}\r\n{ex.GetMessageWithInnerEx(Environment.NewLine)}";
        }

        #endregion

        #endregion
    }

}

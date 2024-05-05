using System.Windows;

using Microsoft.Extensions.Configuration;
using WorkingTimeRecorder.Core.Extensions;
using WorkingTimeRecorder.wpf.Presentation;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTImeRecorder.Core.Injectors;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.Core;
using WorkingTimeRecorder.Core.Languages;

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

        #endregion
    }

}

using System.Windows;
using System.Windows.Input;

using Prism.Commands;

using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.AppSettings.PersonDay;
using WorkingTimeRecorder.wpf.Presentation.Core.Menus;

namespace WorkingTimeRecorder.wpf.Presentation.MainMenuItems
{
    /// <summary>
    /// The menu item view-model of the man-hours person day setting.
    /// </summary>
    internal sealed class ManHoursPersonDaySettingMenuItemViewModel : MenuItemViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManHoursPersonDaySettingMenuItemViewModel" /> class.
        /// </summary>
        /// <param name="wtrSvc">The wtr service.</param>
        public ManHoursPersonDaySettingMenuItemViewModel(IWTRSvc wtrSvc)
        {
            this.Header = wtrSvc.LanguageLocalizer.Localize(WTRTextKeys.PersonDayMenuItem, WTRTextKeys.DefaultPersonDayMenuItem);
            this.Command = new DelegateCommand(this.ExecuteManHoursPersonDaySetting);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public override ICommand Command { get; }

        #endregion

        #region Methods

        private void ExecuteManHoursPersonDaySetting()
        {
            var settingViewModel = new ManHoursPersonDaySettingViewModel();
            var settingView = new ManHoursPersonDaySettingView()
            {
                Owner = Application.Current.MainWindow,
                DataContext = settingViewModel,
            };

            settingView.ShowDialog();
        }

        #endregion
    }
}

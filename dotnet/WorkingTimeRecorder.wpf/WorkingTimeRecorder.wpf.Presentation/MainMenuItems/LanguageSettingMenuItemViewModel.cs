using System.Windows;
using System.Windows.Input;

using Prism.Commands;

using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.AppSettings;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.wpf.Presentation.Core.Menus;

namespace WorkingTimeRecorder.wpf.Presentation.MainMenuItems
{
    /// <summary>
    /// The language setting menu item view model.
    /// </summary>
    internal sealed class LanguageSettingMenuItemViewModel : MenuItemViewModelBase
    {
        #region Fields

        private readonly IWTRSystem wtrSytem;
        private readonly IDialogs dialogs;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageSettingMenuItemViewModel"/> class.
        /// </summary>
        /// <param name="wtrSystem">The wtr system.</param>
        /// <param name="dialogs">The dialogs.</param>
        public LanguageSettingMenuItemViewModel(IWTRSystem wtrSystem, IDialogs dialogs)
        {
            this.wtrSytem = wtrSystem;
            this.dialogs = dialogs;

            this.Header = wtrSystem.LanguageLocalizer.Localize(WTRTextKeys.LanguageSettingMenuItem, WTRTextKeys.DefaultLanguageSettingMenuItem);
            this.Command = new DelegateCommand(this.ExecuteLanguageSetting);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public override ICommand Command { get; }

        #endregion

        #region Methods

        private void ExecuteLanguageSetting()
        {
            var settingViewModel = new LanguageSettingViewModel(this.wtrSytem, this.dialogs);
            var settingWindow = new LanguageSettingView()
            {
                Owner = Application.Current.MainWindow,
                DataContext = settingViewModel,
            };

            settingWindow.ShowDialog();
        }

        #endregion
    }
}

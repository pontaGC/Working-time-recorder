using System.Diagnostics.CodeAnalysis;

using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.wpf.Presentation.Core.Menus;

namespace WorkingTimeRecorder.wpf.Presentation.MainMenuItems
{
    /// <summary>
    /// The main menu item builder.
    /// </summary>
    internal sealed class MainMenuItemBuilder : IMenuItemBuilder
    {
        #region Fields

        private readonly IWTRSvc wtrSvc;
        private readonly IDialogs dialogs;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuItemBuilder"/> class.
        /// </summary>
        /// <param name="wtrSvc">The wtr service.</param>
        /// <param name="dialogs">The dialogs.</param>
        public MainMenuItemBuilder(IWTRSvc wtrSvc, IDialogs dialogs)
        {
            this.wtrSvc = wtrSvc;
            this.dialogs = dialogs;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        [return: NotNull]
        public IEnumerable<MenuItemViewModelBase> Build()
        {
            var result = new List<MenuItemViewModelBase>();

            result.Add(this.BuildSettingsMenuItem());

            return result;
        }

        private EmptyCommandMenuItemViewModel BuildSettingsMenuItem()
        {
            var rootSettingItem = new EmptyCommandMenuItemViewModel()
            {
                Header = this.wtrSvc.LanguageLocalizer.Localize(WTRTextKeys.SettingsMenuItem, WTRTextKeys.DefaultSettingsMenuItem),
            };

            // Language
            rootSettingItem.SubItems.Add(new LanguageSettingMenuItemViewModel(this.wtrSvc, this.dialogs));

            // Man-hours per person day
            rootSettingItem.SubItems.Add(new ManHoursPersonDaySettingMenuItemViewModel(this.wtrSvc));

            return rootSettingItem;
        }

        #endregion
    }
}

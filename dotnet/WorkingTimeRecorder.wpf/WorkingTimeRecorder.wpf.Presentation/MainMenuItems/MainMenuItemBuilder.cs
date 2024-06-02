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

        private readonly IWTRSystem wtrSystem;
        private readonly IDialogs dialogs;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuItemBuilder"/> class.
        /// </summary>
        /// <param name="wtrSystem">The wtr system.</param>
        /// <param name="dialogs">The dialogs.</param>
        public MainMenuItemBuilder(IWTRSystem wtrSystem, IDialogs dialogs)
        {
            this.wtrSystem = wtrSystem;
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
                Header = this.wtrSystem.LanguageLocalizer.Localize(WTRTextKeys.SettingsMenuItem, WTRTextKeys.DefaultSettingsMenuItem),
            };

            // Language
            rootSettingItem.SubItems.Add(new LanguageSettingMenuItemViewModel(this.wtrSystem, this.dialogs));

            // Man-hours per person day
            rootSettingItem.SubItems.Add(new ManHoursPersonDaySettingMenuItemViewModel(this.wtrSystem));

            return rootSettingItem;
        }

        #endregion
    }
}

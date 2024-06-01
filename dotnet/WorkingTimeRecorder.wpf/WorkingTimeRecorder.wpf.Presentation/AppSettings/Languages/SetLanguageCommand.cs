using System.Windows;

using Prism.Commands;

using WorkingTimeRecorder.Core.Extensions;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation.AppSettings
{
    /// <summary>
    /// The command to set language.
    /// </summary>
    internal sealed class SetLanguageCommand : DelegateCommandBase
    {
        #region Fields

        private readonly IDialogs dialogs;
        private readonly IWTRSystem wtrSystem;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetLanguageCommand"/> class.
        /// </summary>
        /// <param name="wtrSystem">The WTR system.</param>
        /// <param name="dialogs">The dialogs.</param>
        public SetLanguageCommand(IWTRSystem wtrSystem, IDialogs dialogs)
        {
            this.wtrSystem = wtrSystem;
            this.dialogs = dialogs;
        }

        #endregion

        #region Events

        /// <summary>
        /// The event occurrs when execution this command is completed.
        /// </summary>
        public event EventHandler Completed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets selected culture name.
        /// </summary>
        public string SelectedCultureName { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override bool CanExecute(object parameter)
        {
            var currentCultureName = this.wtrSystem.LanguageLocalizer.CurrentCulture.Name;
            return currentCultureName != this.SelectedCultureName;
        }

        /// <inheritdoc />
        protected override void Execute(object parameter)
        {
            this.wtrSystem.CultureSetter.NextCultureName = this.SelectedCultureName;

            var ownerWindow = parameter as Window;
            this.ShowNotifyNeedToReboot(ownerWindow);

            this.Completed?.Invoke(this, EventArgs.Empty);
        }

        private MessageBoxResults ShowNotifyNeedToReboot(Window? ownerWindow)
        {
            var message = this.wtrSystem.LanguageLocalizer.Localize(
                "WorkingTimeRecorder.LanguageSetting.NeedToRestartToolToDisplaySelectedLanguage",
                "A reboot is required to change the display language.");
            return this.dialogs.MessageBox.Show(
                ownerWindow,
                message,
                this.wtrSystem.LanguageLocalizer.LocalizeAppTitle(),
                MessageBoxButtons.OK,
                MessageBoxImages.None);
        }

        #endregion
    }
}

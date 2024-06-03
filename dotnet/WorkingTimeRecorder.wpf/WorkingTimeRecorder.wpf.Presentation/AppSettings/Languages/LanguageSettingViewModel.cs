using System.Collections.ObjectModel;
using System.Windows.Input;

using Prism.Commands;

using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation.AppSettings
{
    /// <summary>
    /// The language setting view-model.
    /// </summary>
    internal class LanguageSettingViewModel : ViewModelBase
    {
        #region Fields

        private bool? dialogResult = null;
        private string selectedCultureName;
        private readonly ObservableCollection<string> cultureNames;
        private readonly SetLanguageCommand setLanguageCommand;
        private readonly DelegateCommand cancelCommand;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageSettingViewModel"/> class.
        /// </summary>
        /// <param name="wtrSvc">The wtr service.</param>
        /// <param name="dialogs">The dialogs.</param>
        public LanguageSettingViewModel(IWTRSvc wtrSvc, IDialogs dialogs)
        {
            this.cultureNames = new ObservableCollection<string>(CultureNameConstants.AllNames);
            this.CultureNames = new ReadOnlyObservableCollection<string>(this.cultureNames);

            this.setLanguageCommand = new SetLanguageCommand(wtrSvc, dialogs);
            this.setLanguageCommand.Completed += this.OnSetLanguageCompleted;
            this.cancelCommand = new DelegateCommand(this.Cancel);

            this.SelectedCultureName = wtrSvc.LanguageLocalizer.CurrentCulture.Name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating the dialog result of the window.
        /// </summary>
        public bool? DialogResult
        {
            get => this.dialogResult;
            set => this.SetProperty(ref this.dialogResult, value);
        }

        /// <summary>
        /// Gets or sets selected culture name.
        /// </summary>
        public string SelectedCultureName
        {
            get => this.selectedCultureName;
            set
            {
                if (this.SetProperty(ref this.selectedCultureName, value))
                {
                    this.setLanguageCommand.SelectedCultureName = value;
                    this.setLanguageCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Gets the selectable culture names.
        /// </summary>
        public ReadOnlyObservableCollection<string> CultureNames { get; }

        /// <summary>
        /// Gets a set language command.
        /// </summary>
        public ICommand SetLanguageCommand => this.setLanguageCommand;

        /// <summary>
        /// Gets a cancel command.
        /// </summary>
        public ICommand CancelCommand => this.cancelCommand;

        #endregion

        #region Private Methods

        private void OnSetLanguageCompleted(object? sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel()
        {
            this.DialogResult = false;
        }

        #endregion
    }
}

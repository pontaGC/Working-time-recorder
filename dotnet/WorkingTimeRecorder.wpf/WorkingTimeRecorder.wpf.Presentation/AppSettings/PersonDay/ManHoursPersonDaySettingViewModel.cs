using System.Windows.Input;

using Prism.Commands;

using SharedLibraries.Utils;

using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.Core.Rules.Property;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.wpf.Presentation.AppSettings.PersonDay
{
    /// <summary>
    /// The view-model of the man-hours per person day setting.
    /// </summary>
    internal sealed class ManHoursPersonDaySettingViewModel : ValidationViewModelBase<ManHoursPersonDaySettingViewModel>
    {
        #region Fields

        private readonly IWTRSvc wtrSvc;

        // Backing stores of properties
        private bool? dialogResult = null;
        private string inputPersonDay;
        private readonly SetPersonDayCommand setPersonDayCommand;
        private readonly DelegateCommand cancelCommand;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManHoursPersonDaySettingViewModel"/> class.
        /// </summary>
        /// <param name="wtrSvc">The wtr service.</param>
        public ManHoursPersonDaySettingViewModel(IWTRSvc wtrSvc)
        {
            this.wtrSvc = wtrSvc;

            this.setPersonDayCommand = new SetPersonDayCommand(this.PropertyErrorContainer, [nameof(this.InputPersonDay)]);
            this.setPersonDayCommand.Completed += this.OnSetPersonDayCommandCompleted;
            this.cancelCommand = new DelegateCommand(this.Cancel);

            this.InputPersonDay = Tasks.PersonDay.ToString(this.wtrSvc.LanguageLocalizer.CurrentCulture);

            this.RegisterRules();
            this.ValidateAllProperties();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating the dialog result of the window.
        /// </summary>
        public bool? DialogResult
        {
            get => this.dialogResult;
            set => this.SetProperty(ref this.dialogResult, value, false);
        }

        /// <summary>
        /// Gets or sets the person day text user inputs.
        /// </summary>
        public string InputPersonDay
        {
            get => this.inputPersonDay;
            set
            {
                if (this.SetProperty(ref this.inputPersonDay, value))
                {
                    this.setPersonDayCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Gets a command that executes setting a new man-hours per person day.
        /// </summary>
        public ICommand SetPersonDayCommand => this.setPersonDayCommand;

        /// <summary>
        /// Gets a cancel command.
        /// </summary>
        public ICommand CancelCommand => this.cancelCommand;

        #endregion

        #region Mehtods

        private void OnSetPersonDayCommandCompleted(object? sender, EventArgs e)
        {
            this.Affirm();
        }

        /// <summary>
        /// Closes the setting view as the affirmative result user selected.
        /// </summary>
        private void Affirm()
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Closes the setting view as the cancel result user selected.
        /// </summary>
        private void Cancel()
        {
            this.DialogResult = false;
        }

        private void RegisterRules()
        {
            this.RegisterInputPersonDayRules();
        }

        private void RegisterInputPersonDayRules()
        {
            var languageLocalizer = this.wtrSvc.LanguageLocalizer;
            var propertyName = nameof(this.InputPersonDay);

            this.AddRule(
                new RequiredTextFieldPropertyRule<ManHoursPersonDaySettingViewModel>(                    
                    languageLocalizer,
                    propertyName,
                    x => x.InputPersonDay));
            this.AddRule(
                new DoubleParsePropertyRule<ManHoursPersonDaySettingViewModel>(
                    languageLocalizer,
                    propertyName,
                    x => x.InputPersonDay));
            this.AddRule(
                new RangePropertyRule<ManHoursPersonDaySettingViewModel, double>(
                    languageLocalizer,
                    propertyName,
                    x =>
                    {
                        if (NumberParseHelper.TryDoubleParse(x.InputPersonDay, out var personDay))
                        {
                            return personDay;
                        }

                        return double.MinValue;
                    },
                    false,
                    ManHoursConstants.MinimumPersonDay,
                    true,
                    ManHoursConstants.MaxPersonDay
                    ));
        }

        #endregion
    }
}

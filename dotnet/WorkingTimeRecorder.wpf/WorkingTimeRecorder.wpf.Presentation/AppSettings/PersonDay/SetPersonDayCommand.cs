using Prism.Commands;

using SharedLibraries.Extensions;
using SharedLibraries.Rules;
using SharedLibraries.Utils;

using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.wpf.Presentation.Core;

namespace WorkingTimeRecorder.wpf.Presentation.AppSettings.PersonDay
{
    /// <summary>
    /// The command to set new man-hours per person day.
    /// </summary>
    internal sealed class SetPersonDayCommand : DelegateCommandBase
    {
        #region Fields

        private readonly IPropertyErrorContainer<string> errorContainer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetPersonDayCommand"/> class.
        /// </summary>
        /// <param name="errorContainer">The error container.</param>
        /// <param name="observeProperties">The name collection of the observed properties related to this command.</param>
        public SetPersonDayCommand(IPropertyErrorContainer<string> errorContainer, IEnumerable<string> observeProperties)
        {
            ArgumentNullException.ThrowIfNull(errorContainer);

            this.errorContainer = errorContainer;
            this.ObserveProperties = observeProperties.ToArraySafe();
        }

        #endregion

        #region Events

        /// <summary>
        /// The event occurrs when execution of this command is completed.
        /// </summary>
        public event EventHandler? Completed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a name collection of the observed properties related to this command.
        /// </summary>
        public IEnumerable<string> ObserveProperties { get; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override bool CanExecute(object parameter)
        {
            if (this.HasObservePropertyErrors())
            {
                return false;
            }

            var cmdParam = parameter as CommandParameter;
            if (!TryGetNewPersonDay(cmdParam, out var newValue))
            {
                return false;
            }

            var currentValue = TaskCollection.PersonDay;
            return currentValue != newValue;
        }

        /// <inheritdoc />
        protected override void Execute(object parameter)
        {
            var cmdParam = parameter as CommandParameter;
            if (!TryGetNewPersonDay(cmdParam, out var newValue))
            {
                return;
            }

            // Sets a new value of man-hours per person day
            var oldValue = TaskCollection.PersonDay;
            TaskCollection.PersonDay = newValue;

            TaskCollection.RaisePersonDayChangedEvent(this, new PersonDayChangedEventArgs(oldValue, newValue));

            // Notify completion
            this.Completed?.Invoke(this, EventArgs.Empty);
        }

        private static bool TryGetNewPersonDay(CommandParameter? commandParameter, out double newValue)
        {
            var newValueString = commandParameter?.Parameter as string;
            return NumberParseHelper.TryDoubleParse(newValueString, out newValue);
        }

        private bool HasObservePropertyErrors()
        {
            foreach(var propertyName in this.ObserveProperties)
            {
                if (this.errorContainer.GetErrors(propertyName).Any())
                {
                    return true;
                }
            }

            return false;
        }

        #endregion


    }
}

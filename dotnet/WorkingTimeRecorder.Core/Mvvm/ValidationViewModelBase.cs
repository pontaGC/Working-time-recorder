using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using SharedLibraries.Rules;

namespace WorkingTimeRecorder.Core.Mvvm
{
    /// <summary>
    /// The view-model with validation of properties.
    /// This imeplments <see cref="INotifyPropertyChanged"/> and <see cref="INotifyDataErrorInfo"/>.
    /// </summary>
    /// <typeparam name="T">The type of the implementation of this class.</typeparam>
    public abstract class ValidationViewModelBase<T> : ViewModelBase, INotifyDataErrorInfo
        where T : ValidationViewModelBase<T>
    {
        #region Fields

        private readonly PropertyRuleContainer<T, string> propertyRuleContainer;

        private event EventHandler<DataErrorsChangedEventArgs>? errorsChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationViewModelBase{T}"/> class.
        /// </summary>
        protected ValidationViewModelBase()
        {
            this.propertyRuleContainer = new PropertyRuleContainer<T, string>(this.RaiseErrorsChanged);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public bool HasErrors => this.propertyRuleContainer.HasErrors;

        /// <summary>
        /// Gets an observable when errors are changed. 
        /// </summary>
        public IObservable<string> ErrorsChanged
        {
            get
            {
                return Observable.FromEventPattern<DataErrorsChangedEventArgs>(
                    h => this.errorsChanged += h,
                    h => this.errorsChanged -= h)
                    .Select(x => x.EventArgs.PropertyName ?? string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected IPropertyErrorContainer<string> PropertyErrorContainer => this.propertyRuleContainer;

        #endregion

        #region Explict interface implementation

        /// <inheritdoc />
        event EventHandler<DataErrorsChangedEventArgs>? INotifyDataErrorInfo.ErrorsChanged
        {
            add => this.errorsChanged += value;
            remove => this.errorsChanged -= value;
        }

        /// <inheritdoc />
        IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName)
        {
            return this.propertyRuleContainer.GetErrors(propertyName);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the property and notify property changed
        /// </summary>
        /// <param name="backingStore">The backing field of the property.</param>
        /// <param name="value">The setting value.</param>
        /// <param name="validates">The value indicating whether to validate a property.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns><c>true</c> if the value was changed, <c>false</c> if the existing value matched the desired value.</returns>
        protected bool SetProperty<TProperty>(ref TProperty backingStore, TProperty value, bool validates = true, [CallerMemberName] string? propertyName = null)
        {
            if (validates)
            {
                return this.SetProperty(ref backingStore, value, () => { }, validates, propertyName);
            }

            return base.SetProperty(ref backingStore, value, () => { }, propertyName);
        }

        /// <summary>
        /// Sets the property and notify property changed
        /// </summary>
        /// <param name="backingStore">The backing field of the property.</param>
        /// <param name="value">The setting value.</param>
        /// <param name="onChanged">The action that is called after the property value has been changed.</param>
        /// <param name="validates">The value indicating whether to validate a property.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns><c>true</c> if the value was changed, <c>false</c> if the existing value matched the desired value.</returns>
        protected bool SetProperty<TProperty>(ref TProperty backingStore, TProperty value, Action onChanged, bool validates = true, [CallerMemberName] string? propertyName = null)
        {
            if (validates)
            {
                var onChangedWithValidation = new Action(
                    () => {
                        onChanged?.Invoke();
                        this.ValidateProperty(propertyName);
                    });
                return base.SetProperty(ref backingStore, value, onChangedWithValidation, propertyName);
            }

            return base.SetProperty(ref backingStore, value, onChanged, propertyName);
        }

        /// <summary>
        /// Gets the validation errors for a specified property.
        /// </summary>
        /// <param name="propertyName">The name of property.</param>
        /// <returns>The validation errors for the property.</returns>
        protected IEnumerable<string> GetErrors(string? propertyName)
        {
            // Guarantees not null
            return this.propertyRuleContainer.GetErrors(propertyName);
        }

        /// <summary>
        /// Called when the errors have changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void RaiseErrorsChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Assert(
                !string.IsNullOrEmpty(propertyName) && this.GetType().GetRuntimeProperty(propertyName) != null,
                $"The property {propertyName} does not exist in {this.GetType().Name}");
            this.errorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Adds a new <see cref="IPropertyRule{T}"/>.
        /// </summary>
        /// <param name="rule">The rule to execute.</param>
        /// <returns><c>true</c> if adding the given rule is successful, otherwise, <c>false</c>.</returns>
        protected bool AddRule(IPropertyRule<T> rule)
        {
            return this.propertyRuleContainer.AddRule(rule);
        }

        /// <summary>
        /// Adds a new property rule.
        /// </summary>
        /// <param name="propertyName">The name of the property the rules applies to.</param>
        /// <param name="validateProperty">The rule to validate the property.</param>
        /// <param name="getError">The error function if rule fails.</param>
        /// <returns><c>true</c> if adding the given rule is successful, otherwise, <c>false</c>.</returns>
        protected bool AddRule(string? propertyName, Func<T, bool> validateProperty, Func<T, string> getError)
        {
            if (string.IsNullOrEmpty(propertyName) || validateProperty is null || getError is null)
            {
                return false;
            }

            var propertyRule = new DelegatePropertyRule<T>(
                propertyName,
                validateProperty,
                x => getError);

            return this.AddRule(propertyRule);
        }

        /// <summary>
        /// Removes all rules of a specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property the rules applies to.</param>
        /// <returns><c>true</c> if removing the property rules is successful, otherwise, <c>false</c>.</returns>
        protected bool RemoveRule(string? propertyName)
        {
            return this.propertyRuleContainer.RemoveRule(propertyName);
        }

        /// <summary>
        /// Validates a specified property.
        /// </summary>
        /// <param name="propertyName">The name of a property.</param>
        /// <returns><c>true</c> if no error is found, otherwise, <c>false</c>.</returns>
        protected bool ValidateProperty(string? propertyName)
        {
            return this.propertyRuleContainer.ValidateProperty((T)this, propertyName);
        }

        /// <summary>
        /// Validates all the properties with data annotations.
        /// </summary>
        /// <returns><c>true</c> if no error is found, otherwise, <c>false</c>.</returns>
        protected bool ValidateAllProperties()
        {
            this.propertyRuleContainer.ClearErrors(true);
            return this.propertyRuleContainer.ValidateAll((T)this);
        }

        #endregion
    }
}

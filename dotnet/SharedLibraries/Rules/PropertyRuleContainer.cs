using System.Diagnostics.CodeAnalysis;

using SharedLibraries.Extensions;

namespace SharedLibraries.Rules
{
    /// <summary>
    /// The property rule container.
    /// </summary>
    /// <typeparam name="T">The type of an object which has the property to validate.</typeparam>
    /// <typeparam name="TError">The type of the error object.</typeparam>
    public class PropertyRuleContainer<T, TError> : IPropertyErrorContainer<TError>
    {
        #region Fields

        private readonly Action<string> errorsChanged;

        private readonly Dictionary<string, List<TError>> errors = new Dictionary<string, List<TError>>();
        private readonly Dictionary<string, HashSet<IPropertyRule<T>>> rules = new Dictionary<string, HashSet<IPropertyRule<T>>>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyRuleContainer{T, TError}"/> class.
        /// </summary>
        /// <param name="errorsChanged">The event when the validation error in a property is found.</param>
        /// <exception cref="ArgumentNullException"><paramref name="errorsChanged"/> is <c>null</c>.</exception>
        public PropertyRuleContainer(Action<string> errorsChanged)
        {
            ArgumentNullException.ThrowIfNull(errorsChanged);
            this.errorsChanged = errorsChanged;
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public bool HasErrors => this.errors.Values.Count > 0;

        #endregion

        #region Public Methods

        /// <inheritdoc />
        [return: NotNull]
        public IEnumerable<TError> GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !this.errors.ContainsKey(propertyName))
            {
                return Enumerable.Empty<TError>();
            }

            return this.errors[propertyName] ?? Enumerable.Empty<TError>();
        }

        /// <inheritdoc />
        public void SetErrors(string? propertyName, IEnumerable<TError> errors, bool suspendErrorsChanged)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            var errorList = errors.ToListSafe();
            if (this.errors.ContainsKey(propertyName))
            {
                this.errors[propertyName] = errorList;
            }
            else
            {
                this.errors.Add(propertyName, errorList);
            }

            if (!suspendErrorsChanged)
            {
                this.errorsChanged.Invoke(propertyName);
            }
        }

        /// <inheritdoc />
        public void ClearErrors(string? propertyName, bool suspendErrorsChanged)
        {
            if (string.IsNullOrEmpty(propertyName) || !this.errors.ContainsKey(propertyName))
            {
                return;
            }

            this.SetErrors(propertyName, new List<TError>(), suspendErrorsChanged);
        }

        /// <inheritdoc />
        public void ClearErrors(bool suspendErrorsChanged)
        {
            foreach(var propertyName in this.rules.Keys)
            {
                this.SetErrors(propertyName, new List<TError>(), suspendErrorsChanged);
            }
        }

        /// <summary>
        /// Adds a new <see cref="IPropertyRule{T}"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property the rules applies to.</param>
        /// <param name="rule">The rule to execute.</param>
        /// <returns><c>true</c> if adding the given rule is successful, otherwise, <c>false</c>.</returns>
        public bool AddRule(IPropertyRule<T> rule)
        {
            if (rule is null)
            {
                return false;
            }
            
            if (this.rules.TryGetValue(rule.PropertyName, out var propertyRules))
            {
                return propertyRules.Add(rule);
            }

            this.rules.Add(rule.PropertyName, new HashSet<IPropertyRule<T>>() { rule });
            return true;
        }

        /// <summary>
        /// Removes all rules of a specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property the rules applies to.</param>
        /// <returns><c>true</c> if removing the property rules is successful, otherwise, <c>false</c>.</returns>
        public bool RemoveRule(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return false;
            }

            this.rules[propertyName] = new HashSet<IPropertyRule<T>>();
            return true;
        }

        /// <summary>
        /// Validates a specified property.
        /// </summary>
        /// <param name="obj">The object which has a property.</param>
        /// <param name="propertyName">The name of a property.</param>
        /// <returns><c>true</c> if no error is found, otherwise, <c>false</c>.</returns>
        public bool ValidateProperty(T obj, string? propertyName)
        {
            if (obj is null || string.IsNullOrEmpty(propertyName))
            {
                // There can not be any errors for the property which does not exist 
                return true;
            }

            if (!this.rules.TryGetValue(propertyName, out var propertyRules))
            {
                // Unnecessary validatation
                return true;
            }

            var newErrors = new List<TError>();
            foreach(var rule in propertyRules)
            {
                if (rule.Apply(obj))
                {
                    // Passed
                    continue;
                }
                
                newErrors.Add((TError)rule.ErrorExecute.Invoke(obj));
            }

            this.SetErrors(propertyName, newErrors, false);
            return newErrors.Count > 0;
        }

        /// <summary>
        /// Validates a specified property which has rules.
        /// </summary>
        /// <param name="obj">The object which has the properties.</param>
        /// <returns><c>true</c> if no error is found, otherwise, <c>false</c>.</returns>
        public bool ValidateAll(T obj)
        {
            if (obj is null)
            {
                return true;
            }

            var hasError = false;
            foreach(var properyName in this.rules.Keys)
            {
                if (this.ValidateProperty(obj, properyName))
                {
                    // Passed
                    continue;
                }

                hasError = true;
            }

            return hasError;
        }

        #endregion
    }
}
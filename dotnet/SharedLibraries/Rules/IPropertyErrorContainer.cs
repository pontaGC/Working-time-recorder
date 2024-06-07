using System.Diagnostics.CodeAnalysis;

namespace SharedLibraries.Rules
{
    /// <summary>
    /// The property error container.
    /// </summary>
    /// <typeparam name="TError">The type of error object.</typeparam>
    public interface IPropertyErrorContainer<TError>
    {
        /// <summary>
        /// Gets a value indicating whether a error is found or not.
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Gets the validation errors for a specified property.
        /// </summary>
        /// <param name="propertyName">The name of property.</param>
        /// <returns>The validation errors for the property.</returns>
        [return: NotNull]
        IEnumerable<TError> GetErrors(string? propertyName);

        /// <summary>
        /// Sets the validation errors for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of property.</param>
        /// <param name="errors">The new validation errors.</param>
        /// <param name="suspendErrorsChanged">The value indicating whether to suspend execution of the errors changed action.</param>
        void SetErrors(string? propertyName, IEnumerable<TError> errors, bool suspendErrorsChanged = false);

        /// <summary>
        /// Clears the errors for a property.
        /// </summary>
        /// <param name="propertyName">The name of property.</param>
        /// <param name="suspendErrorsChanged">The value indicating whether to suspend execution of the errors changed action.</param>
        void ClearErrors(string? propertyName, bool suspendErrorsChanged = false);

        /// <summary>
        /// Clears all errors.
        /// </summary>
        /// <param name="suspendErrorsChanged">The value indicating whether to suspend execution of the errors changed action.</param>
        void ClearErrors(bool suspendErrorsChanged = false);
    }
}

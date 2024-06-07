using SharedLibraries.Rules;

using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Rules.Property
{
    /// <summary>
    /// The property rule to validate whether the text is <c>null</c> or an empty string, consits only of white-space characters.
    /// </summary>
    /// <typeparam name="T">The type of an object which has the property to validate.</typeparam>
    public class RequiredTextFieldPropertyRule<T> : IPropertyRule<T>
    {
        private readonly ILanguageLocalizer languageLocalizer;
        private readonly Func<T, string> getProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredTextFieldPropertyRule{T}"/> class.
        /// </summary>
        /// <param name="languageLocalizer">The language localizer.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="getProperty">The function to get the property value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="languageLocalizer"/> or <paramref name="propertyName"/> or <paramref name="getProperty"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is an empty string.</exception>
        public RequiredTextFieldPropertyRule(
            ILanguageLocalizer languageLocalizer,
            string propertyName,
            Func<T, string> getProperty)
        {
            ArgumentNullException.ThrowIfNull(languageLocalizer);
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            ArgumentNullException.ThrowIfNull(getProperty);

            this.languageLocalizer = languageLocalizer;
            this.PropertyName = propertyName;
            this.getProperty = getProperty;

            this.ErrorExecute = obj => GetErrorMessage();
        }

        /// <summary>
        /// Gets a value indicating whether to allow that the text consists only of white-space characters.
        /// The default value is <c>false</c>.
        /// </summary>
        public bool AllowOnlyWhitespace { get; set; } = false;

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public Func<T, object>? ErrorExecute { get; }

        /// <inheritdoc />
        public bool Apply(T @object)
        {
            var value = this.getProperty(@object);
            return this.AllowOnlyWhitespace
                ? !string.IsNullOrEmpty(value)
                : !string.IsNullOrWhiteSpace(value);
        }

        private string GetErrorMessage()
        {
            return this.languageLocalizer.Localize(ValidationErrorKeys.TextFieldRequired, ValidationErrorKeys.DefaultTextFieldRequired);
        }
    }
}

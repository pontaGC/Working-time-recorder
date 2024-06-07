using System.Globalization;

using SharedLibraries.Rules;

using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Rules.Property
{
    /// <summary>
    /// The property rule to validate whether the text can be converted to double.
    /// </summary>
    /// <typeparam name="T">The type of an object which has the property to validate.</typeparam>
    public class DoubleParsePropertyRule<T> : IPropertyRule<T>
    {
        private readonly ILanguageLocalizer languageLocalizer;
        private readonly Func<T, string> getProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleParsePropertyRule{T}"/> class.
        /// </summary>
        /// <param name="languageLocalizer">The language localizer.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="getProperty">The function to get the property value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="languageLocalizer"/> or <paramref name="propertyName"/> or <paramref name="getProperty"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is an empty string.</exception>
        public DoubleParsePropertyRule(
            ILanguageLocalizer languageLocalizer,
            string propertyName,
            Func<T, string> getProperty)
        {
            ArgumentNullException.ThrowIfNull(languageLocalizer);
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            ArgumentNullException.ThrowIfNull(getProperty);

            this.languageLocalizer = languageLocalizer;
            this.getProperty = getProperty;
            this.PropertyName = propertyName;
            this.ErrorExecute = obj => this.GetErrorMessage();
        }

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public Func<T, object> ErrorExecute { get; }

        /// <summary>
        /// Gets or sets a culture.
        /// The default value is <c>InvariantCulture</c>.
        /// </summary>
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        /// <inheritdoc />
        public bool Apply(T @object)
        {
            var value = this.getProperty(@object);
            var formatProvider = this.Culture ?? CultureInfo.InvariantCulture;
            return double.TryParse(value, NumberStyles.Number, formatProvider, out _);
        }

        private string GetErrorMessage()
        {
            return this.languageLocalizer.Localize(ValidationErrorKeys.DoubleParse, ValidationErrorKeys.DefaultDoubleParse);
        }
    }
}

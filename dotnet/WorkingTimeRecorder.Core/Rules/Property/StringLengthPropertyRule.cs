using System.Globalization;
using SharedLibraries.Rules;

using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Rules.Property
{
    /// <summary>
    /// The property rule to validate whether a length of text is between minimum and maximum length.
    /// </summary>
    /// <typeparam name="T">The type of an object which has the property to validate.</typeparam>
    public class StringLengthPropertyRule<T> : IPropertyRule<T>
    {
        private readonly ILanguageLocalizer languageLocalizer;
        private readonly Func<T, string> getProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangePropertyRule{T, TComparable}"/> class.
        /// </summary>
        /// <param name="languageLocalizer">The language localizer.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="getProperty">The function to get the property value.</param>
        /// <param name="minLength">The minimum length of text.</param>
        /// <param name="maxLength">The maximum length of text.</param>
        /// <exception cref="ArgumentNullException"><paramref name="languageLocalizer"/> or <paramref name="propertyName"/> or <paramref name="getProperty"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is an empty string.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="minLength"/> is negative.
        /// -or-
        /// <paramref name="maxLength"/> is less than or equal to <paramref name="minLength"/>.
        /// </exception>
        public StringLengthPropertyRule(
            ILanguageLocalizer languageLocalizer,
            string propertyName,
            Func<T, string> getProperty,
            int minLength,
            int maxLength)
        {
            ArgumentNullException.ThrowIfNull(languageLocalizer);
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            ArgumentNullException.ThrowIfNull(getProperty);
            ArgumentOutOfRangeException.ThrowIfNegative(minLength);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxLength, minLength);

            this.languageLocalizer = languageLocalizer;
            this.PropertyName = propertyName;
            this.getProperty = getProperty;
            this.MinLength = minLength;
            this.MaxLength = maxLength;

            this.ErrorExecute = obj => this.GetErrorMessage(obj);
        }

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public Func<T, object> ErrorExecute { get; }

        /// <summary>
        /// Gets a minimum length.
        /// </summary>
        public int MinLength { get; }

        /// <summary>
        /// Gets a maximum length.
        /// </summary>
        public int MaxLength { get; }

        /// <inheritdoc />
        public bool Apply(T @object)
        {
            var text = this.getProperty(@object) ?? string.Empty;
            var textLength = text.Length;
            return this.MinLength <= textLength && textLength <= this.MaxLength;
        }

        protected virtual string GetErrorMessage(T @object)
        {
            var messageFormat = this.languageLocalizer.Localize(ValidationErrorKeys.OverStringLength, ValidationErrorKeys.DefaultOverStringLength);
            return string.Format(CultureInfo.InvariantCulture, messageFormat, this.MinLength, this.MaxLength);
        }
    }
}

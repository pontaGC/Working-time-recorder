using SharedLibraries.Rules;

using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Rules.Property
{
    /// <summary>
    /// The range property rule.
    /// </summary>
    /// <typeparam name="T">The type of an object which has the property to validate.</typeparam>
    /// <typeparam name="TComparable">The type of the property to compare.</typeparam>
    public class RangePropertyRule<T, TComparable> : IPropertyRule<T>
        where TComparable : IComparable
    {
        #region Fields

        private readonly ILanguageLocalizer languageLocalizer;
        private readonly Func<T, TComparable> getProperty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RangePropertyRule{T, TComparable}"/> class.
        /// </summary>
        /// <param name="languageLocalizer">The language localizer.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="getProperty">The function to get the property value.</param>
        /// <param name="minIsExclusive">The value indicating that the comparable value are equal to <c>MinValue</c>.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxIsExclusive">The value indicating that the comparable value are equal to <c>MaxValue</c>.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="languageLocalizer"/> or <paramref name="propertyName"/> or <paramref name="getProperty"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is an empty string.</exception>
        /// <exception cref="ArgumentException"><paramref name="minValue"/> or <paramref name="maxValue"/> is invalid value.</exception>
        public RangePropertyRule(
            ILanguageLocalizer languageLocalizer,
            string propertyName,
            Func<T, TComparable> getProperty,
            bool minIsExclusive,
            TComparable minValue,
            bool maxIsExclusive,
            TComparable maxValue)
        {
            ArgumentNullException.ThrowIfNull(languageLocalizer);
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            ArgumentNullException.ThrowIfNull(getProperty);

            if (!MinValueAndMaxValueAreOk(minIsExclusive, minValue, maxIsExclusive, maxValue))
            {
                throw new ArgumentException($"The minimum value ({minValue}) and max value ({maxValue}) is invalid");
            }

            this.languageLocalizer = languageLocalizer;
            this.getProperty = x => getProperty.Invoke(x);
            this.PropertyName = propertyName;
            this.MinIsExclusive = minIsExclusive;
            this.MinValue = minValue;
            this.MaxIsExclusive = maxIsExclusive;
            this.MaxValue = maxValue;
            this.ErrorExecute = obj => this.GetErrorMessage(obj);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Specifies whether validation should fail for values that are equal to <c>MinValue</c>.
        /// </summary>
        public bool MinIsExclusive { get; }

        /// <summary>
        /// Specifies whether validation should fail for values that are equal to <c>MaxValue</c>.
        /// </summary>
        public bool MaxIsExclusive { get; }

        /// <summary>
        /// Gets a minimum value.
        /// </summary>
        public TComparable MinValue { get; }

        /// <summary>
        /// Gets a maximum value.
        /// </summary>
        public TComparable MaxValue { get; }

        /// <inheritdoc />
        public string PropertyName { get; }

        /// <inheritdoc />
        public Func<T, object> ErrorExecute { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public bool Apply(T @object)
        {
            var newValue = this.getProperty.Invoke(@object);
            return this.IsMinRangeOk(newValue) && this.IsMaxRangeOk(newValue);
        }

        #endregion

        #region Protected Methods

        protected virtual string GetErrorMessage(T @object)
        {
            var messageFormat = this.languageLocalizer.Localize(ValidationErrorKeys.OverRange, ValidationErrorKeys.DefaultOverRange);
            return string.Format(messageFormat, this.MinValue.ToString(), this.MaxValue.ToString());
        }

        #endregion

        #region Private Methods

        private static bool MinValueAndMaxValueAreOk(bool minIsExclusive, TComparable minValue, bool maxIsExclusive, TComparable maxValue)
        {
            var compared = minValue.CompareTo(maxValue);
            if (compared > 0)
            {
                return false;
            }

            if (compared == 0 && (minIsExclusive || maxIsExclusive))
            {
                return false;
            }

            return true;
        }

        private bool IsMinRangeOk(TComparable value)
        {
            return this.MinIsExclusive ? this.MinValue.CompareTo(value) < 0 : this.MinValue.CompareTo(value) <= 0;
        }

        private bool IsMaxRangeOk(TComparable value)
        {
            return this.MaxIsExclusive ? this.MaxValue.CompareTo(value) > 0 : this.MaxValue.CompareTo(value) >= 0;
        }

        #endregion
    }
}

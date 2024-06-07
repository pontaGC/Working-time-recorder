namespace SharedLibraries.Rules
{
    /// <summary>
    /// The property rule specified by delegate.
    /// </summary>
    /// <typeparam name="T">The type of an object which has the property to validate.</typeparam>
    public class DelegatePropertyRule<T> : DelegateRule<T>, IPropertyRule<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatePropertyRule{T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="validateProperty">The validation rule.</param>
        /// <param name="getError">The function to get error.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> or <paramref name="validateProperty"/> or <paramref name="getError"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is an empty string.</exception>
        public DelegatePropertyRule(
            string propertyName,
            Func<T, bool> validateProperty,
            Func<T, object> getError)
            : base(validateProperty, getError)
        {
            ArgumentException.ThrowIfNullOrEmpty(propertyName);

            this.PropertyName = propertyName;
        }

        /// <inheritdoc />
        public string PropertyName { get; }
    }
}

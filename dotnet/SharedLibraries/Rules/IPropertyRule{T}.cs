namespace SharedLibraries.Rules
{
    /// <summary>
    /// The validation rule for a property.
    /// </summary>
    /// <typeparam name="T">The type of an object which has the property to validate.</typeparam>
    public interface IPropertyRule<T> : IRule<T>
    {
        /// <summary>
        /// Gets a name of property.
        /// </summary>
        string PropertyName { get; }
    }
}

using System.Diagnostics.CodeAnalysis;

namespace SharedLibraries.Rules
{
    /// <summary>
    /// The validation rule for an object.
    /// </summary>
    /// <typeparam name="T">The type of the target object to the rule apply to.</typeparam>
    public interface IRule<T>
    {
        /// <summary>
        /// Gets an error execution if the rule fails.
        /// The return value is usually an error message.
        /// </summary>
        [NotNull]
        Func<T, object> ErrorExecute { get; }

        /// <summary>
        /// Applies the validation rule to a property.
        /// </summary>
        /// <param name="object">The object which has the property to validate.</param>
        /// <returns><c>true</c> if <c>value</c> is valid, otherwise, <c>false</c>.</returns>
        bool Apply(T @object);
    }
}

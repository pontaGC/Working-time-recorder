using System.Diagnostics.CodeAnalysis;

namespace SharedLibraries.Rules
{
    /// <summary>
    /// The delegate rule.
    /// </summary>
    /// <typeparam name="T">The type of the target object to the rule apply to.</typeparam>
    public class DelegateRule<T> : IRule<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateRule{T}"/> class.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="errorExecute">The error execution if the rule fails.</param>
        /// <exception cref="ArgumentNullException"><c>rule</c> or <c>errorExecute</c> is <c>null</c>.</exception>
        public DelegateRule(Func<T, bool> rule, Func<T, object> errorExecute)
        {
            ArgumentNullException.ThrowIfNull(rule);
            ArgumentNullException.ThrowIfNull(errorExecute);

            this.Rule = rule;
            this.ErrorExecute = errorExecute;
        }

        /// <summary>
        /// Gets a rule function.
        /// </summary>
        protected Func<T, bool> Rule { get; }

        /// <inheritdoc />
        [NotNull]
        public Func<T, object>? ErrorExecute { get; }

        /// <inheritdoc />
        public bool Apply(T @object)
        {
            return this.Rule.Invoke(@object);
        }
    }
}

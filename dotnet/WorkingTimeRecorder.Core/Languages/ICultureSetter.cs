namespace WorkingTimeRecorder.Core.Languages
{
    /// <summary>
    /// The culture setter.
    /// </summary>
    public interface ICultureSetter
    {
        /// <summary>
        /// Gets or sets the next culture name to display the language user selected after rebooting the tool.
        /// </summary>
        string NextCultureName { get; set; }

        /// <summary>
        /// Sets a new culture.
        /// </summary>
        /// <param name="cultureName">The name of culture to set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cultureName"/> is an empty string.</exception>
        /// <exception cref="NotSupportedException"><paramref name="cultureName"/> is not supported culture.</exception>
        void SetCulture(string cultureName);
    }
}

namespace WorkingTimeRecorder.Core.Languages
{
    /// <summary>
    /// The culture information in this app.
    /// </summary>
    public interface IAppCultureInfo : IFormatProvider
    {
        /// <summary>
        /// Gets a culture name.
        /// </summary>
        string Name { get; }
    }
}

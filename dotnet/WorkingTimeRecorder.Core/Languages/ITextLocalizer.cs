namespace WorkingTimeRecorder.Core.Languages
{
    /// <summary>
    /// Responsible for localizing a text.
    /// </summary>
    public interface ITextLocalizer
    {
        /// <summary>
        /// Gets a culture name indicating the localized language.
        /// </summary>
        string CultureName { get; }

        /// <summary>
        /// Gets a locaized string corresponding to the given text ID.
        /// </summary>
        /// <param name="textId">The ID of the text resource.</param>
        /// <returns>The localized string if the <c>textId</c> is found, otherwise, an empty string.</returns>
        string Localize(string textId);

        /// <summary>
        /// Tries to get a locaized string corresponding to the given text ID.
        /// </summary>
        /// <param name="textId">The ID of the text resource.</param>
        /// <param name="localizedText">The localized string if the <c>textId</c> is found, otherwise, an empty string.</param>
        /// <returns><c>true</c> if getting localized text is successful, otherwise, <c>false</c>.</returns>
        bool TryLocalize(string textId, out string localizedText);
    }
}

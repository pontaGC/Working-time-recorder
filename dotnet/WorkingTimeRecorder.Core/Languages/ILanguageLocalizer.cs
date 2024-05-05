using System.Diagnostics.CodeAnalysis;

namespace WorkingTimeRecorder.Core.Languages
{
    /// <summary>
    /// The language localization.
    /// </summary>
    public interface ILanguageLocalizer
    {
        /// <summary>
        /// Gets an information of the current culture.
        /// </summary>
        IAppCultureInfo CurrentCulture { get; }

        /// <summary>
        /// Gets a locaized string corresponding to the given text ID.
        /// </summary>
        /// <param name="textId">The ID of the text resource.</param>
        /// <returns>The localized string if the <c>textId</c> is found, otherwise, an empty string.</returns>
        [return: NotNull]
        string Localize(string textId);

        /// <summary>
        /// Gets a locaized string corresponding to the given text ID.
        /// </summary>
        /// <param name="textId">The ID of the text resource.</param>
        /// <param name="defaultText">The text to display if the text ID is not found.</param>
        /// <returns>The localized string if the <c>textId</c> is found, otherwise, the <c>defaultText</c>.</returns>
        [return: NotNull]
        string Localize(string textId, string defaultText);
    }
}

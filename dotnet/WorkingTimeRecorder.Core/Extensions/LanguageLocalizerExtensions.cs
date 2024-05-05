using System.Diagnostics;

using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ILanguageLocalizer"/>.
    /// </summary>
    public static class LanguageLocalizerExtensions
    {
        /// <summary>
        /// Gets the localized text of the title in this app.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text of the title in this app.</returns>
        [DebuggerStepThrough]
        public static string LocalizeAppTitle(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.Title, CommonTextKeys.DefaultTitle);
        }
    }
}

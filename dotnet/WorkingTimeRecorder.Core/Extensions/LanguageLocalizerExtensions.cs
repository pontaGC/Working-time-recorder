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
            return @this.Localize(WTRTextKeys.Title, WTRTextKeys.DefaultTitle);
        }

        /// <summary>
        /// Gets the localized text of "OK" key.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text.</returns>
        [DebuggerStepThrough]
        public static string LocalizeOk(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.Ok, CommonTextKeys.DefaultOk);
        }

        /// <summary>
        /// Gets the localized text of "Cancel" key.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text.</returns>
        [DebuggerStepThrough]
        public static string LocalizeCancel(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.Cancel, CommonTextKeys.DefaultCancel);
        }

        /// <summary>
        /// Gets the localized text of "Yes" key.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text.</returns>
        [DebuggerStepThrough]
        public static string LocalizeYes(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.Yes, CommonTextKeys.DefaultYes);
        }

        /// <summary>
        /// Gets the localized text of "No" key.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text.</returns>
        [DebuggerStepThrough]
        public static string LocalizeNo(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.No, CommonTextKeys.DefaultNo);
        }

        /// <summary>
        /// Gets the localized text of "Information" key.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text.</returns>
        [DebuggerStepThrough]
        public static string LocalizeInformation(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.Information, CommonTextKeys.DefaultInformation);
        }

        /// <summary>
        /// Gets the localized text of "Warning" key.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text.</returns>
        [DebuggerStepThrough]
        public static string LocalizeWarning(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.Warning, CommonTextKeys.DefaultWarning);
        }

        /// <summary>
        /// Gets the localized text of "Error" key.
        /// </summary>
        /// <param name="this">The instance.</param>
        /// <returns>The localized text.</returns>
        [DebuggerStepThrough]
        public static string LocalizeError(this ILanguageLocalizer @this)
        {
            return @this.Localize(CommonTextKeys.Error, CommonTextKeys.DefaultError);
        }
    }
}

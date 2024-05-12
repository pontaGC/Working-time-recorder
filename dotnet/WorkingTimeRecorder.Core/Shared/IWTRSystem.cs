using System.Diagnostics.CodeAnalysis;
using WorkingTimeRecorder.Core.Languages;

namespace WorkingTimeRecorder.Core.Shared
{
    /// <summary>
    /// The singleton services in WTR (Working time recorder) system.
    /// </summary>
    public interface IWTRSystem
    {
        #region Globalization

        /// <summary>
        /// Gets a language localization.
        /// </summary>
        [NotNull]
        ILanguageLocalizer LanguageLocalizer { get; }

        #endregion
    }
}

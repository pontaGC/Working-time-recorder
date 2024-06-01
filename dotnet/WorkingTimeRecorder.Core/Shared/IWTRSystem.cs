﻿using System.Diagnostics.CodeAnalysis;

using SharedLibraries.Logging;

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

        /// <summary>
        /// Gets a culture setter.
        /// </summary>
        ICultureSetter CultureSetter { get; }

        #endregion

        #region Logging

        /// <summary>
        /// Gets a collection of loggers.
        /// </summary>
        ILoggerCollection LoggerCollection { get; }

        #endregion
    }
}

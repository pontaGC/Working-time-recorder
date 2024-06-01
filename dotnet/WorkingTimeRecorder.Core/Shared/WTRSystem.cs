﻿using SharedLibraries.Logging;

using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Languages.Internal;

namespace WorkingTimeRecorder.Core.Shared
{
    /// <summary>
    /// The implementation of the <see cref="IWTRSystem"/>.
    /// </summary>
    public sealed class WTRSystem : IWTRSystem
    {
        #region Singleton instance constructors

        static WTRSystem()
        {
        }

        private WTRSystem() { }

        public static WTRSystem Instance { get; } = new WTRSystem();

        #endregion

        #region Globalization

        /// <inheritdoc />
        public ILanguageLocalizer LanguageLocalizer => this.lazyLanguageLocalizer.Value;

        /// <inheritdoc />
        public ICultureSetter CultureSetter => this.lazyLanguageLocalizer.Value;

        private readonly Lazy<LanguageLocalizer> lazyLanguageLocalizer = new Lazy<LanguageLocalizer>(() => new LanguageLocalizer());

        #endregion

        #region Logging

        /// <inheritdoc />
        public ILoggerCollection LoggerCollection => this.lazyLoggerCollection.Value;

        /// <summary>
        /// Gets a logger registrar.
        /// </summary>
        internal ILoggerRegistrar LoggerRegistrar => this.lazyLoggerCollection.Value;

        private readonly Lazy<LoggerCollection> lazyLoggerCollection = new Lazy<LoggerCollection>();

        #endregion
    }
}

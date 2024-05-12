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
        public ILanguageLocalizer LanguageLocalizer => lazyLanguageLocalizer.Value;

        /// <summary>
        /// Gets a culture setter.
        /// </summary>
        internal ICultureSetter CultureSetter => lazyLanguageLocalizer.Value;

        private static readonly Lazy<LanguageLocalizer> lazyLanguageLocalizer = new Lazy<LanguageLocalizer>(() => new LanguageLocalizer());

        #endregion
    }
}

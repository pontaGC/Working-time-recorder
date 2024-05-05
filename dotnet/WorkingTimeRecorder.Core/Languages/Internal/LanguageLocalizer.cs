using System.Globalization;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using SharedLibraries.Extensions;

namespace WorkingTimeRecorder.Core.Languages.Internal
{
    /// <summary>
    /// The language localizer.
    /// </summary>
    internal sealed class LanguageLocalizer : ILanguageLocalizer, ICultureSetter
    {
        #region Fields

        private readonly object localizationLock = new object();
        private IAppCultureInfo appCultureInfo;
        private ITextLocalizer currentTextLocalizer;

        #endregion

        #region Constructors

        internal LanguageLocalizer()
        {
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public IAppCultureInfo CurrentCulture
        {
            get
            {
                lock (this.localizationLock)
                {
                    if (this.appCultureInfo is null)
                    {
                        this.appCultureInfo = new AppCultureInfo(CultureInfo.CreateSpecificCulture(CultureNameConstants.DefaultCultureName));
                        Debug.Fail("The culture information has not been initialized.");
                    }

                    return this.appCultureInfo;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        [return: NotNull]
        public string Localize(string textId)
        {
            var textLocalizer = GetCurrentLocalizer();
            return textLocalizer.Localize(textId);
        }

        /// <inheritdoc />
        [return: NotNull]
        public string Localize(string textId, string defaultText)
        {
            var textLocalizer = GetCurrentLocalizer();
            if (textLocalizer.TryLocalize(textId, out var localizedText))
            {
                return localizedText;
            }

            return defaultText ?? textId ?? string.Empty;
        }

        /// <inheritdoc />
        public void SetCulture(string cultureName)
        {
            ArgumentException.ThrowIfNullOrEmpty(cultureName);

            var newCultureName = CultureNameConstants.AllNames.First(n => n.EqulasOrdinalIgnoreCase(cultureName));
            if (newCultureName is null)
            {
                throw new NotSupportedException($"Found the unknown culture: {cultureName}");
            }

            lock (this.localizationLock)
            {
                if (UpdateCultureIfNeeded(newCultureName))
                {
                    this.currentTextLocalizer = new TextLocalizer(newCultureName);
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets a current text localizer in this app.
        /// </summary>
        [return: NotNull]
        private ITextLocalizer GetCurrentLocalizer()
        {
            lock (this.localizationLock)
            {
                if (this.currentTextLocalizer is null)
                {
                    this.currentTextLocalizer = new TextLocalizer(CultureNameConstants.DefaultCultureName);
                    Debug.Fail("The localizer has not been initialized.");
                }

                return this.currentTextLocalizer;
            }
        }

        private bool UpdateCultureIfNeeded(string newCultureName)
        {
            if (CultureInfo.DefaultThreadCurrentCulture is not null
                && CultureInfo.DefaultThreadCurrentCulture.Name.EqulasOrdinalIgnoreCase(newCultureName))
            {
                // Unnecessary updating
                return false;
            }

            var newCulture = CultureInfo.CreateSpecificCulture(newCultureName);
            CultureInfo.CurrentCulture = newCulture;
            CultureInfo.CurrentUICulture = newCulture;
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;

            this.appCultureInfo = new AppCultureInfo(newCulture);

            return true;
        }

        #endregion
    }
}
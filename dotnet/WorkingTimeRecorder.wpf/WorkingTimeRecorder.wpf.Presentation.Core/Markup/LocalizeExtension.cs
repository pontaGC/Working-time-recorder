using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Markup
{
    /// <summary>
    /// WPF Mark-up extension to convert the given text ID to the localized text corresponding to it.
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public sealed class LocalizeExtension : MarkupExtension
    {
        #region Fields

        private static readonly ILanguageLocalizer LanguageLocalizer = WTRSystem.Instance.LanguageLocalizer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizeExtension"/> class.
        /// </summary>
        public LocalizeExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizeExtension"/> class.
        /// </summary>
        /// <param name="textId">The text key.</param>
        public LocalizeExtension(string textId)
        {
            this.Id = textId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateExtension" /> class.
        /// </summary>
        /// <param name="textId">The text key.</param>
        /// <param name="defaultText">The text corresponding to the text key.</param>
        public LocalizeExtension(string textId, string defaultText)
            : this(textId)
        {
            this.DefaultText = defaultText;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Text key to lookup the text.
        /// </summary>
        [ConstructorArgument("textId")]
        public string Id { get; set; }

        /// <summary>
        /// Text Corresponding to the text key.
        /// </summary>
        [ConstructorArgument("defaultText")]
        public string DefaultText { get; set; }

        #endregion

        #region Markup extension

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return this.Id ?? this.DefaultText;
            }

            return LanguageLocalizer.Localize(this.Id, this.DefaultText);
        }

        #endregion
    }
}
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

using WorkingTimeRecorder.wpf.Presentation.Core.Icons;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Markup
{
    /// <summary>
    /// WPF Mark-up extension to getting status icon.
    /// </summary>
    [MarkupExtensionReturnType(typeof(UIElement))]
    public sealed class IconExtension : MarkupExtension
    {
        #region Fields

        private static IIconService iconService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IconExtension"/> class.
        /// </summary>
        public IconExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IconExtension"/> class.
        /// </summary>
        /// <param name="key">The icon's identifier.</param>
        public IconExtension(string key)
        {
            this.Key = key;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an icon's identifier (=Key).
        /// </summary>
        [ConstructorArgument("key")]
        public string Key { get; set; }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Registers the required service.
        /// </summary>
        /// <param name="iconServiceInstance">The icon service.</param>
        internal static void Register(IIconService iconServiceInstance)
        {
            Debug.Assert(iconServiceInstance != null, $"{nameof(iconServiceInstance)} must not be NULL.");
            iconService = iconServiceInstance;
        }

        #endregion

        #region Markup extension

        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this mark-up extension. 
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the mark-up extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue();
        }

        private UIElement? ProvideValue()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return null;
            }

            return iconService.GetIcon(Key);
        }

        #endregion
    }
}

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation.Dialogs.MessageBox
{
    /// <summary>
    /// The converters the message box image kind to the actual image.
    /// </summary>
    [ValueConversion(typeof(MessageBoxImage), typeof(ImageSource))]
    internal sealed class MessageBoxImageConverter : IValueConverter
    {
        #region Methods

        /// <summary>
        /// Converts the value indicating message box image to the actual image.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property. Can be NULL.</param>
        /// <param name="parameter">The converter parameter. Can be NULL.</param>
        /// <param name="culture">The culture to use in the converter. Can be NULL.</param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value is MessageBoxImages, $"The type of {nameof(value)} must be {nameof(MessageBoxImages)}.");

            var convertedValue = Convert((MessageBoxImages)value);
            return convertedValue ?? DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        /// <summary>
        /// Converts the message box image kind to the actual image.
        /// </summary>
        /// <param name="imageKind">The kind of message box image.</param>
        /// <returns>A converted image source.</returns>
        private static ImageSource? Convert(MessageBoxImages imageKind)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return null;
            }

            switch (imageKind)
            {
                case MessageBoxImages.Error:
                    //return iconService.GetIcon(IconConstants.Status.CriticalError);
                    return null;

                case MessageBoxImages.Warning:
                    //return iconService.GetIcon(IconConstants.Status.Warning);
                    return null;

                case MessageBoxImages.Information:
                    //return iconService.GetIcon(IconConstants.Status.Information);
                    return null;

                case MessageBoxImages.Question:
                    //return iconService.GetIcon(IconConstants.Status.Help);
                    return null;

                default:
                    return null;
            }
        }

        #endregion
    }
}

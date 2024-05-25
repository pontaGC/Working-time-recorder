using System.Globalization;
using System.Windows.Data;
using System.Windows;

using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation.Dialogs.MessageBox
{
    /// <summary>
    /// The converters the message box image kind to the image visibility.
    /// </summary>
    [ValueConversion(typeof(MessageBoxImages), typeof(Visibility))]
    internal class MessageBoxImageVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageKind = (MessageBoxImages)value;
            if (imageKind == MessageBoxImages.None)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

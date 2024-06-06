using System.Globalization;
using System.Windows;
using System.Windows.Data;

using SharedLibraries.Logging;

namespace WorkingTimeRecorder.wpf.Presentation.OutputWindows
{
    /// <summary>
    /// The converters the log severity kind to the image visibility.
    /// </summary>
    [ValueConversion(typeof(Severity), typeof(Visibility))]
    internal sealed class OutputWindowSeverityVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var severity = (Severity)value;
            if (severity == Severity.None)
            {
                return VisibilityBoxes.Hidden;
            }

            return VisibilityBoxes.Visible;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Converters
{
    [ValueConversion(typeof(bool), typeof(bool[]))]
    public class MultiBooleanConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is null)
            {
                return DependencyProperty.UnsetValue;
            }

            foreach (var value in values)
            {
                if (value is not bool boolean || !boolean)
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return Array.Empty<object>();
        }
    }
}

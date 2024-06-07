using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Converters
{
    /// <summary>
    /// The command parameter converter.
    /// </summary>
    /// <remarks>Please store UI element to the <c>parameter</c> arg  to get owner window.</remarks>
    [ValueConversion(typeof(object), typeof(CommandParameter))]
    public class CommandParameterConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var uiElement = parameter as UIElement;
            var ancestorWindow = Window.GetWindow(uiElement);

            return new CommandParameter(ancestorWindow, value);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

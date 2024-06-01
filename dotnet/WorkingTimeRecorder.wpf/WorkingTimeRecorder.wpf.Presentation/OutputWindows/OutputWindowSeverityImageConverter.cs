using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using SharedLibraries.Logging;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Icons;
using WorkingTimeRecorder.wpf.Presentation.Icons;

namespace WorkingTimeRecorder.wpf.Presentation.OutputWindows
{
    /// <summary>
    /// The converters the message box image kind to the actual image.
    /// </summary>
    [ValueConversion(typeof(Severity), typeof(UIElement))]
    internal sealed class OutputWindowSeverityImageConverter : IValueConverter
    {
        #region Fields

        private readonly IIconService iconService = IconService.Instance;

        #endregion

        #region Methods

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var severity = (Severity)value;
            var severityImage = this.Convert(severity);
            return severityImage is null ? DependencyProperty.UnsetValue : severityImage;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        private UIElement? Convert(Severity severity)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return null;
            }

            switch (severity)
            {
                case Severity.Information:
                    return this.iconService.GetIcon(IconKeys.StatusInformation);

                case Severity.Warning:
                    return this.iconService.GetIcon(IconKeys.StatusWarning);

                case Severity.Error:
                case Severity.Alert:
                case Severity.Fatal:
                    return this.iconService.GetIcon(IconKeys.StatusError);

                default:
                    return null;
            }
        }

        #endregion
    }
}

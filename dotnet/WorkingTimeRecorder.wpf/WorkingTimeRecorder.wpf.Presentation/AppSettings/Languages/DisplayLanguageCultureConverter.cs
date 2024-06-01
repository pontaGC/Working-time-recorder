using System.Globalization;
using System.Windows;
using System.Windows.Data;

using WorkingTimeRecorder.Core.Languages;

namespace WorkingTimeRecorder.wpf.Presentation.AppSettings
{
    /// <summary>
    /// The display language - culture name converter, e.g. display language: "English", culture name: "en-US".
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    internal sealed class DisplayLanguageCultureConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cultureName = value as string ?? string.Empty;
            if (CultureNameConstants.DisplayLanguages.TryGetValue(cultureName, out var displayLanguage))
            {
                return displayLanguage;
            }

            return DependencyProperty.UnsetValue;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var displayLanguage = value as string ?? string.Empty;
            foreach(var cultureName in CultureNameConstants.DisplayLanguages.Keys)
            {
                if (displayLanguage == CultureNameConstants.DisplayLanguages[cultureName])
                {
                    return cultureName;
                }
            }

            return CultureNameConstants.DefaultCultureName;
        }
    }
}

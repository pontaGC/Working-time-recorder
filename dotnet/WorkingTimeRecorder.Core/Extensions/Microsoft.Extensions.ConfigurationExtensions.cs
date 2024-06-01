using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;
using WorkingTimeRecorder.Core.Configurations;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IConfiguration"/>.
    /// </summary>
    public static class MsftExtConfigurationExtensions
    {
        /// <summary>
        /// Gets an app settings.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="showError">The action to show reading appsettings error.</param>
        /// <returns>
        /// The app settings onto which the appsettings.json is projected.
        /// Returns <c>null</c>, if reading appsettings.json fails.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="showError"/> is <c>null</c>.</exception>
        [DebuggerStepThrough]
        public static AppSettings GetAppSettings(this IConfiguration config, Action<Exception> showError)
        {
            ArgumentNullException.ThrowIfNull(showError);

            // Binded appsettings
            try
            {
                return config.GetSection("appsettings").Get<AppSettings>();
            }
            catch(Exception ex)
            {
                showError.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets a culture.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="configKey">The culture key.</param>
        /// <returns>The culture value in the configuration.</returns>
        [DebuggerStepThrough]
        [return:NotNull]
        public static string? GetCulture(this IConfiguration config, string configKey = AppSettingsKeys.Culture)
        {
            return config.GetSection("appsettings")[configKey];
        }

        ///// <summary>
        ///// Gets a man-hours per person-day in appsettings.json.
        ///// </summary>
        ///// <param name="config">The config.</param>
        ///// <param name="personDayString">The raw value of person day.</param>
        ///// <param name="configKey">The person day key.</param>
        ///// <returns>The man-day value in the configuration if the value of 'PersonDay' is valid, otherwise, <c>null</c> .</returns>
        //[DebuggerStepThrough]
        //public static double? GetPersonDay(this IConfiguration config, out string personDayString, string configKey = AppSettingsKeys.PersonDay)
        //{
        //    personDayString = config[configKey];
        //    var appSettings = config.GetAppSettings();
        //    return appSettings.PersonDay;
        //    //if (double.TryParse(personDayString, out var personDay))
        //    //{
        //    //    return personDay;
        //    //}

        //    //return null;
        //}

    }
}

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IConfiguration"/>.
    /// </summary>
    public static class MsftExtConfigurationExtensions
    {
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
            return config[configKey];
        }

        /// <summary>
        /// Gets a man-hours per person-day in appsettings.json.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="configKey">The person-day key.</param>
        /// <param name="personDayString">The raw value of person day.</param>
        /// <returns>The man-day value in the configuration if the value of 'PersonDay' is valid, otherwise, <c>null</c> .</returns>
        [DebuggerStepThrough]
        public static double? GetPersonDay(this IConfiguration config, out string personDayString, string configKey = AppSettingsKeys.PersonDay)
        {
            personDayString = config[configKey];
            if (double.TryParse(personDayString, out var personDay))
            {
                return personDay;
            }

            return null;
        }
    }
}

using System.Diagnostics;
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
        /// <param name="cultureKey">The culture key.</param>
        /// <returns>The culture value in the configuration.</returns>
        [DebuggerStepThrough]
        public static string? GetCulture(this IConfiguration config, string cultureKey = AppSettingsKeys.Culture)
        {
            return config[cultureKey];
        }
    }
}

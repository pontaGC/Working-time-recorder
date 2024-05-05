using Microsoft.Extensions.Configuration;

namespace WorkingTimeRecorder.wpf
{
    /// <summary>
    /// Responsible for creating the application configuration.
    /// </summary>
    internal class ConfigurationFactory
    {
        /// <summary>
        /// Creates an application configuration.
        /// </summary>
        /// <returns>An instance of the configuration.</returns>
        public static IConfiguration Create()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}

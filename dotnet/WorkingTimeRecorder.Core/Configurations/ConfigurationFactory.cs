using Microsoft.Extensions.Configuration;

namespace WorkingTimeRecorder.Core.Configurations
{
    /// <summary>
    /// Responsible for creating the application configuration.
    /// </summary>
    public sealed class ConfigurationFactory
    {
        /// <summary>
        /// Creates an application configuration.
        /// </summary>
        /// <param name="showError">The action to show configuration build error.</param>
        /// <returns>An instance of the configuration.</returns>
        public static IConfiguration Create(Action<Exception> showError)
        {
            ArgumentNullException.ThrowIfNull(showError);

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            try
            {
                var config = builder.Build();
                return config;
            }
            catch(Exception ex)
            {
                showError.Invoke(ex);
                return null;
            }
        }
    }
}

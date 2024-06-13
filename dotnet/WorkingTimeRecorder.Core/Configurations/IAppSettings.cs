using System.Text.Json;

namespace WorkingTimeRecorder.Core.Configurations
{
    /// <summary>
    /// The app settings.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Gets or sets a culture name in this application.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the man-hours per person day.
        /// </summary>
        public double PersonDay { get; set; }

        /// <summary>
        /// Saves app settings as json file.
        /// </summary>
        /// <param name="filePath">The file path to save.</param>
        /// <param name="serializerOptions">The json serializer options.</param>
        /// <exception cref="AppSettingsSaveFailureException">See <c>InnerException</c> for details of the error.</exception>
        void SaveFile(string filePath, JsonSerializerOptions serializerOptions);
    }
}

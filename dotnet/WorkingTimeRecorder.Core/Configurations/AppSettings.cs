using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Configuration;

using SharedLibraries.System.IO;

using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Configurations
{
    /// <summary>
    /// The model in appsettings.json.
    /// </summary>
    [Serializable]
    public sealed class AppSettings : IAppSettings
    {
        [JsonIgnore]
        private readonly IWTRSystem wtrSystem = WTRSystem.Instance;

        /// <summary>
        /// Gets or sets a value indicating whether changes of app settings must be saved or not.
        /// </summary>
        [JsonIgnore]
        public bool IsModified { get; set; } = false;

        /// <inheritdoc />
        [JsonPropertyName("Culture")]
        [ConfigurationKeyName("Culture")]
        public string Culture { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("PersonDay")]
        [ConfigurationKeyName("PersonDay")]
        public double PersonDay { get; set; }

        /// <summary>
        /// Saves app settings as json file.
        /// </summary>
        /// <param name="filePath">The file path to save.</param>
        /// <param name="serializerOptions">The json serializer options.</param>
        /// <exception cref="AppSettingsSaveFailureException">See <c>InnerException</c> for details of the error.</exception>
        public void SaveFile(string filePath, JsonSerializerOptions serializerOptions)
        {
            try
            {
                var serializeObject = new AppSettingsSerializeObject() { AppSettings = this, };
                using (var fileStream = FileSystem.File.OpenOrCreate(filePath, FileAccess.Write))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    using (var streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
                    {
                        var jsonString = JsonSerializer.Serialize(serializeObject, serializerOptions);
                        streamWriter.Write(jsonString);
                    }
                }
            }
            catch (Exception exception)
            {
                const string MessageKey = "WorkingTimeRecorder.appsettings.FailedSaveFile";
                var message = this.wtrSystem.LanguageLocalizer.Localize(MessageKey, MessageKey);
                throw new AppSettingsSaveFailureException(message, exception);
            }
        }

        private sealed class AppSettingsSerializeObject
        {
            [JsonPropertyName("appsettings")]
            public AppSettings AppSettings { get; set; }
        }
    }
}

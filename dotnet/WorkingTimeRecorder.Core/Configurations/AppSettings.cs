﻿using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Configuration;

using SharedLibraries.Extensions;
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
        private readonly IWTRSvc wtrSvc = WTRSvc.Instance;

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

        /// <inheritdoc />
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
                var message = this.wtrSvc.LanguageLocalizer.Localize(MessageKey, MessageKey);
                throw new AppSettingsSaveFailureException(message, exception);
            }

            try
            {
                // TODO: Search the following bug cause for details.
                // When executing json serializing under certain conditions,
                // there are times when there are one many right bracket

                var fileContent = FileSystem.File.ReadAllText(filePath);
                if (FixExtraCurlyBracketIfNeeded(fileContent, out var fixedFileContent))
                {
                    FileSystem.File.WriteAllText(filePath, fixedFileContent);
                }
            }
            catch (Exception exception)
            {
                const string MessageKey = "WorkingTimeRecorder.appsettings.FailedSaveFile";
                var message = this.wtrSvc.LanguageLocalizer.Localize(MessageKey, MessageKey);
                throw new AppSettingsSaveFailureException(message, exception);
            }
        }

        private static bool FixExtraCurlyBracketIfNeeded(string? fileContent, out string fixedContent)
        {
            fixedContent = fileContent ?? string.Empty;

            var leftBracketCount = fileContent.CountCharacter('{');
            var rightBracketCount = fileContent.CountCharacter('}');
            if (leftBracketCount < rightBracketCount)
            {
                fixedContent = fileContent.RemoveEndOnce("}");
                return true;
            }

            return false;
        }

        private sealed class AppSettingsSerializeObject
        {
            [JsonPropertyName("appsettings")]
            public AppSettings AppSettings { get; set; }
        }
    }
}

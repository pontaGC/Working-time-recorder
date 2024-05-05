using System.Text.Json.Serialization;

namespace WorkingTimeRecorder.Core.Languages.Internal
{
    [Serializable]
    internal class LanguageResourceFile
    {
        [JsonPropertyName("Item")]
        public IList<LanguageTextItem> TextItems { get; set; }
    }

    [Serializable]
    internal class LanguageTextItem
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }
    }
}

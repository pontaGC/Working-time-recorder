namespace WorkingTimeRecorder.Core.Languages
{
    /// <summary>
    /// The culture name constant parameters.
    /// </summary>
    public static class CultureNameConstants
    {
        public const string English = "en-US";
        public const string Japanese = "ja-JP";

        public static readonly string DefaultCultureName = English;
        public static readonly IReadOnlyCollection<string> AllNames =
        [
            English,
            Japanese,
        ];

        /// <summary>
        /// Culture name - display language table whose key is culture name,
        /// e.g. Key: "en-US", Value: "English".
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> DisplayLanguages = new Dictionary<string, string>()
        {
            { English, "English" },
            { Japanese, "日本語" },
        };
    }
}

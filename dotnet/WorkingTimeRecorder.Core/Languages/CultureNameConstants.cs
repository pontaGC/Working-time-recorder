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
    }
}

namespace WorkingTimeRecorder.Core.Shared
{
    /// <summary>
    /// The icon keys.
    /// The icon key is a resource file name without file extension.
    /// </summary>
    public static class IconKeys
    {
        public static readonly IReadOnlyCollection<string> AllKeys =
        [
            StatusError, StatusHelp, StatusInformation, StatusWarning,
        ];

        public const string StatusError = "StatusError";
        public const string StatusHelp = "StatusHelp";
        public const string StatusInformation = "StatusInformation";
        public const string StatusWarning = "StatusWarning";
    }
}

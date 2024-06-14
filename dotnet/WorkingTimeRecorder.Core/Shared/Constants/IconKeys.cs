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
            Add,
            Delete,
            StatusError, StatusHelp, StatusInformation, StatusWarning,
        ];

        public const string Add = "Add";
        public const string Delete = "Delete";

        public const string StatusError = "StatusError";
        public const string StatusHelp = "StatusHelp";
        public const string StatusInformation = "StatusInformation";
        public const string StatusWarning = "StatusWarning";
    }
}

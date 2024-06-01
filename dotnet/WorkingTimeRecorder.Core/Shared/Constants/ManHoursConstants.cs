namespace WorkingTimeRecorder.Core.Shared
{
    /// <summary>
    /// The constant parameters for man-hours.
    /// </summary>
    public static class ManHoursConstants
    {
        public const double DefaultPersonDay = 7.5;
        public const double MinimumPersonDay = 0.00;
        public const double MaxPersonDay = 24.00;

        public const int NumberOfDecimalPoints = 2;
        public const string StringFormat = "F2";
    }
}

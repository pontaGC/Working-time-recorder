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
    }
}

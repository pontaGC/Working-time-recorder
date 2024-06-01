namespace WorkingTimeRecorder.Core.Configurations
{
    /// <summary>
    /// The exception can occurs when saving app settings to external data storage.
    /// </summary>
    [Serializable]
    public class AppSettingsSaveFailureException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsSaveFailureException"/> class.
        /// </summary>
        public AppSettingsSaveFailureException()
            : base ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsSaveFailureException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AppSettingsSaveFailureException(string? message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsSaveFailureException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public AppSettingsSaveFailureException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}

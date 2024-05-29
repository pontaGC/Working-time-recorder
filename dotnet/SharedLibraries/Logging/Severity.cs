namespace SharedLibraries.Logging
{

    /// <summary>
    /// The log severity.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// The simple log without severity.
        /// </summary>
        None = -1,

        /// <summary>
        /// The information.
        /// </summary>
        Information = 0,

        /// <summary>
        /// The warning.
        /// </summary>
        Warning,

        /// <summary>
        /// The error that can continue processing.
        /// </summary>
        Error,

        /// <summary>
        /// The error that cannot continue processing.
        /// </summary>
        Alert,

        /// <summary>
        /// The fatal error for the application.
        /// </summary>
        Fatal,
    }
}

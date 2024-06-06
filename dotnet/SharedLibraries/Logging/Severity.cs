using System.Diagnostics;

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

    /// <summary>
    /// Static log severity boxed values.
    /// </summary>
    public static class LogSeverityBoxes
    {
        public static readonly object None = Severity.None;
        public static readonly object Information = Severity.Information;
        public static readonly object Warning = Severity.Warning;
        public static readonly object Error = Severity.Error;
        public static readonly object Alert = Severity.Alert;
        public static readonly object Fatal = Severity.Fatal;

        /// <summary>
        /// Gets a pre-boxed value. This method can be used to avoid boxing.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The boxed value.</returns>
        public static object ToObject(this Severity source)
        {
            switch (source)
            {
                case Severity.None:
                    return None;
                case Severity.Information:
                    return Information;
                case Severity.Warning:
                    return Warning;
                case Severity.Error:
                    return Error;
                case Severity.Alert:
                    return Alert;
                case Severity.Fatal:
                    return Fatal;
                default:
                    Debug.Fail($"Found unknown log severity ({source})");
                    return None;
            }
        }
    }
}

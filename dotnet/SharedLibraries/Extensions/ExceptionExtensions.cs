namespace SharedLibraries.Extensions
{
    /// <summary>
    /// Extension methods for an <c>Exception</c>.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets an exception message.
        /// If the given exception has an inner exception,
        /// returns the message that contains the inner exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="stringToSplitExceptionMessage">
        /// The character that delimits the inner exception message.
        /// If you would like to split inner exception message with line break, this parameter may be <see cref="Environment.NewLine"/>.
        /// </param>
        /// <returns>The exception message, if <c>exception</c> is not <c>null</c>, otherwise, retruns an empty string.</returns>
        public static string GetMessageWithInnerEx(this Exception exception, string stringToSplitExceptionMessage)
        {
            if (exception is null)
            {
                return string.Empty;
            }

            return GetInnerExceptionMessageRecursively(exception, stringToSplitExceptionMessage);
        }

        private static string GetInnerExceptionMessageRecursively(Exception exception, string splitString)
        {
            if (exception.InnerException is null)
            {
                return exception.Message;
            }

            return $"{exception.Message}{splitString}{GetInnerExceptionMessageRecursively(exception.InnerException, splitString)}";
        }
    }
}

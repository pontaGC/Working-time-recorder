using System.Diagnostics;

namespace SharedLibraries.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Stream"/> object.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Checks whether or not the source stream is NULL or an empty stream.
        /// </summary>
        /// <param name="source">The source stream to check.</param>
        /// <returns><c>false</c>, if the <c>source</c> stream is <c>null</c> or its <c>Length</c> is 0. Otherwise; <c>true</c>.</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty(this Stream source)
        {
            return source is null || source.Length == 0;
        }
    }
}

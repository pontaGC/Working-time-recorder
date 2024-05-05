using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SharedLibraries.Extensions
{
    /// <summary>
    /// Extension methods for <c>string</c> type.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Splits the given string with whitespace.
        /// </summary>
        /// <param name="source">The string to split.</param>
        /// <returns>A collection of the split string with whitespace.</returns>
        [return: NotNull]
        public static IEnumerable<string> SplitWithWhitespace(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return Enumerable.Empty<string>();
            }

            // Extracts non-whitespace characters
            return Regex.Matches(source, "\\S+").OfType<Match>().Select(m => m.Value);
        }

        /// <summary>
        /// Determines whether two specified string objects have the same value.
        /// This method uses the <c>OrdinalIgnoreCase</c> comparison rule.
        /// </summary>
        /// <param name="source">The first string to compare.</param>
        /// <param name="other">The second string to compare.</param>
        /// <returns><c>true</c> if the given parameters are same; otherwise, returns <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool EqulasOrdinalIgnoreCase(this string source, string other)
        {
            return string.Equals(source, other, StringComparison.OrdinalIgnoreCase);
        }
    }
}
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SharedLibraries.Extensions
{
    /// <summary>
    /// Extension methods for <c>string</c> type.
    /// </summary>
    public static partial class StringExtension
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
        /// Splits the given string with period('.').
        /// </summary>
        /// <param name="source">The string to split.</param>
        /// <returns>A collection of the split string with period.</returns>
        [return: NotNull]
        public static IEnumerable<string> SplitWithPeriod(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return Enumerable.Empty<string>();
            }

            // Extracts non-whitespace characters
            return source.Split('.');
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

        /// <summary>
        /// Removes one target string at end of the given string.
        /// </summary>
        /// <param name="source">The source string to remove.</param>
        /// <param name="removeString">The removing target string.</param>
        /// <returns>A new string after removing the <paramref name="removeString"/> at end.</returns>
        [return: NotNull]
        public static string RemoveEndOnce(this string? source, string removeString)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(removeString))
            {
                return string.Empty;
            }

            var lastIndex = source.LastIndexOf(removeString);
            if (lastIndex < 0)
            {
                // Not found the removing target string
                return source;
            }

            return source.Remove(lastIndex, removeString.Length);
        }
    }
}
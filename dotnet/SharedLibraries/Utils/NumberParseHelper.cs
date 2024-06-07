using System.Globalization;

namespace SharedLibraries.Utils
{
    /// <summary>
    /// The helper to convert to a number string to number type.
    /// This is executed with <c>InvariantCulture</c>.
    /// </summary>
    public static class NumberParseHelper
    {
        private const NumberStyles DoubleNumberStyles = NumberStyles.Float | NumberStyles.AllowThousands;

        /// <summary>
        /// Tries to execute <c>double.TryParse</c>.
        /// </summary>
        /// <param name="s">The number string to convert.</param>
        /// <param name="result">The converted string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>.</returns>
        public static bool TryDoubleParse(string? s, out double result)
        {
            return TryDoubleParse(s, DoubleNumberStyles, out result);
        }

        /// <summary>
        /// Tries to execute <c>double.TryParse</c>.
        /// </summary>
        /// <param name="s">The number string to convert.</param>
        /// <param name="numberStyles">The number styles.</param>
        /// <param name="result">The converted string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>.</returns>
        public static bool TryDoubleParse(string? s, NumberStyles numberStyles, out double result)
        {
            return double.TryParse(s, numberStyles, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Tries to execute <c>int.TryParse</c>.
        /// </summary>
        /// <param name="s">The number string to convert.</param>
        /// <param name="result">The converted string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>.</returns>
        public static bool TryIntParse(string? s, out int result)
        {
            return TryIntParse(s, NumberStyles.Integer, out result);
        }

        /// <summary>
        /// Tries to execute <c>int.TryParse</c>.
        /// </summary>
        /// <param name="s">The number string to convert.</param>
        /// <param name="numberStyles">The number styles.</param>
        /// <param name="result">The converted string.</param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>.</returns>
        public static bool TryIntParse(string? s, NumberStyles numberStyles, out int result)
        {
            return int.TryParse(s, numberStyles, CultureInfo.InvariantCulture,  out result);
        }
    }
}

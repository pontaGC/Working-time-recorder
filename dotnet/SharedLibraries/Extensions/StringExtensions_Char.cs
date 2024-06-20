namespace SharedLibraries.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Counts a specified character in the string.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="targetChar">The count target character.</param>
        /// <returns>The number of <paramref name="targetChar"/> in the string.</returns>
        public static int CountCharacter(this string? s, char targetChar)
        {
            var count = 0;
            foreach(var character in s.AsSpan())
            {
                if (character == targetChar)
                {
                    ++count;
                }
            }

            return count;
        }
    }
}

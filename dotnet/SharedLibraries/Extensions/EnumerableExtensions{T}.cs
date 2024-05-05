using System.Diagnostics;

namespace SharedLibraries.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> object.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Indicates whether a sequence is <c>null</c> or does not contain any element.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable.</typeparam>
        /// <param name="source">The collection to check.</param>
        /// <returns><c>true</c> if <paramref name="source"/> is <c>null</c> or does not contain any element; otherwise, <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source is null || !source.Any();
        }

        /// <summary>
        /// Indicates whether a sequence does not contain any element.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable.</typeparam>
        /// <param name="source">The collection to check.</param>
        /// <returns><c>true</c> if <paramref name="source"/> does not contain any element; otherwise, <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        /// <summary>
        /// Checks whether all elements are unique.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable.</typeparam>
        /// <param name="source">The collection to check.</param>
        /// <param name="comparer">The comparer to determine the elements are equal.</param>
        /// <returns><c>true</c> if all elements in <c>source</c> are equal, otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        [DebuggerStepThrough]
        public static bool AllUnique<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            var hashSet = new HashSet<T>(comparer ?? EqualityComparer<T>.Default);
            foreach(var element in source)
            {
                if (!hashSet.Add(element))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

using System.Diagnostics;
using System.Runtime.CompilerServices;

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

        /// <summary>
        /// Creates an array from a <see cref="IEnumerable{T}"/>. If the sequence is <c>null</c>, creates an empty array.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to create an array from.</param>
        /// <returns>An array that contains the elements from the input sequence, if the <c>source</c> is not <c>null</c>. Otherwise; An empty array, <c>Array.Empty()</c>.</returns>
        [DebuggerStepThrough]
        public static TSource[] ToArraySafe<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                return Array.Empty<TSource>();
            }

            return source.ToArray();
        }

        /// <summary>
        /// Creates a list from a <see cref="IEnumerable{T}"/>. If the sequence is <c>null</c>, creates an empty list.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to create a list from.</param>
        /// <returns>A list that contains the elements from the input sequence, if the <c>source</c> is not <c>null</c>. Otherwise; A empty list.</returns>
        [DebuggerStepThrough]
        public static List<TSource> ToListSafe<TSource>(this IEnumerable<TSource> source)
        {
            return (source ?? Enumerable.Empty<TSource>()).ToList();
        }

        /// <summary>
        /// Invokes the specified action for elements in the sequence.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is <c>null</c>.</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null)
            {
                return;
            }

            ArgumentNullException.ThrowIfNull(action);

            foreach(var element in source)
            {
                action.Invoke(element);
            }
        }
    }
}

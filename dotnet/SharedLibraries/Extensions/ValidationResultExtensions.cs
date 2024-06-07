using System.ComponentModel.DataAnnotations;

namespace SharedLibraries.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ValidationResult" />
    /// </summary>
    public static class ValidationResultExtensions
    {
        /// <summary>
        /// Converts a collection of validation result to the dictionary whose key is member name.
        /// </summary>
        /// <param name="source">The source collection to convert.</param>
        /// <returns>Th converted result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Dictionary<string, List<ValidationResult>> ToMemberNameKeyDictionary(this IEnumerable<ValidationResult> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var result = new Dictionary<string, List<ValidationResult>>();
            foreach (var validationResult in source)
            {
                foreach (var errorPropertyName in validationResult.MemberNames)
                {
                    if (result.ContainsKey(errorPropertyName))
                    {
                        result[errorPropertyName].Add(validationResult);
                    }
                    else
                    {
                        var errors = new List<ValidationResult>() { validationResult };
                        result.Add(errorPropertyName, errors);
                    }
                }
            }

            return result;
        }
    }
}

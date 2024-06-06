namespace SharedLibraries.System
{
    // 
    // Defines Known boxed values.
    // Ref. https://referencesource.microsoft.com/#WindowsBase/Base/MS/Internal/KnownBoxes.cs

    /// <summary>
    /// Static boolean boxed values.
    /// </summary>
    public static class BooleanBoxes
    {
        public static readonly object True = true;
        public static readonly object False = false;

        /// <summary>
        /// Gets a pre-boxed value. This method can be used to avoid boxing.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The boxed value.</returns>
        public static object ToObject(this bool source)
        {
            return source ? True : False;
        }
    }
}

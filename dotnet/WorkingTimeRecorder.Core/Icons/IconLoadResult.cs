namespace WorkingTimeRecorder.Core.Icons
{
    /// <summary>
    /// The icon loading result.
    /// </summary>
    public class IconLoadResult
    {
        private IconLoadResult(bool isFailed, string iconKey, Stream stream)
        {
            this.IsFailed = isFailed;
            this.IconKey = iconKey;
            this.Stream = stream;
        }

        /// <summary>
        /// Gets a value indicating loading icon is failed.
        /// </summary>
        public bool IsFailed { get; }

        /// <summary>
        /// Gets an icon key.
        /// </summary>
        public string IconKey { get; }

        /// <summary>
        /// Gets a icon resource stream.
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Creates the failed result.
        /// </summary>
        /// <param name="iconKey">The icon key.</param>
        /// <returns>An instance.</returns>
        public static IconLoadResult Failed(string iconKey)
        {
            return new IconLoadResult(true, iconKey, null);
        }

        /// <summary>
        /// Creates the successful result.
        /// </summary>
        /// <param name="iconKey">The icon key.</param>
        /// <returns>An instance.</returns>
        public static IconLoadResult Succeeded(string iconKey, Stream stream)
        {
            return new IconLoadResult(false, iconKey, stream);
        }
    }
}

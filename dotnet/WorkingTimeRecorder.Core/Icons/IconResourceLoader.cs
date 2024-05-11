using System.Reflection;
using SharedLibraries.Extensions;
using SharedLibraries.System.IO;

namespace WorkingTimeRecorder.Core.Icons
{
    /// <summary>
    /// Responsible for loading icon resources.
    /// </summary>
    public sealed class IconResourceLoader
    {
        #region Fields

        // Key: Icon key (= resource file name)
        private readonly Dictionary<string, string> resourceNames;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IconResourceLoader"/> class.
        /// </summary>
        public IconResourceLoader()
        {
            var thisAsembly = Assembly.GetExecutingAssembly();
            this.resourceNames = thisAsembly.GetManifestResourceNames().ToDictionary(GetIconKey, name => name);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks whether the definition of the give icon key exists or not.
        /// </summary>
        /// <param name="iconKey">The key to check.</param>
        /// <returns><c>true</c> if the give icon key is found, otherwise, <c>false</c>.</returns>
        public bool Exists(string iconKey)
        {
            if (string.IsNullOrEmpty(iconKey))
            {
                return false;
            }

            return this.resourceNames.ContainsKey(iconKey);
        }

        /// <summary>
        /// Loads the icon resource specified by the given icon key.
        /// </summary>
        /// <param name="iconKey">The icon key.</param>
        /// <returns>The loading result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="iconKey"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="iconKey"/> is an empty string, or is not found in the icon resources.</exception>
        public IconLoadResult Load(string iconKey)
        {
            ArgumentException.ThrowIfNullOrEmpty(iconKey);

            if (!this.resourceNames.TryGetValue(iconKey, out var iconResourceName))
            {
                throw new ArgumentException($"The icon key ({iconKey}) does not exist.", nameof(iconKey));
            }

            var thisAssembly = Assembly.GetExecutingAssembly();
            var stream = thisAssembly.GetManifestResourceStream(iconResourceName);
            if (stream is null || !stream.CanRead)
            {
                return IconLoadResult.Failed(iconKey);
            }

            stream.Position = 0;
            return IconLoadResult.Succeeded(iconKey, stream);
        }

        /// <summary>
        /// Marks the loading icon as completed
        /// </summary>
        /// <param name="iconKey">The key to load icon.</param>
        public void MarkLoadCompleted(string iconKey)
        {
            if (string.IsNullOrEmpty(iconKey))
            {
                return;
            }

            this.resourceNames.Remove(iconKey);
        }

        #endregion

        #region Private Methods

        private static string GetIconKey(string resourceName)
        {
            // resourceName: {assembly name}.Icons.Resources.{icon key}.xaml
            var nameWithoutExtension = FileSystem.File.GetFileNameWithoutExtension(resourceName);
            var iconKey = nameWithoutExtension.SplitWithPeriod().Last();
            return iconKey;
        }

        #endregion
    }
}

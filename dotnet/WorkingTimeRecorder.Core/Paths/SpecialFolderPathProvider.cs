using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.Core.Paths
{
    /// <summary>
    /// The special folder path provider.
    /// </summary>
    public class SpecialFolderPathProvider
    {
        /// <summary>
        /// Gets a local app data folder path.
        /// </summary>
        /// <returns>A local app data folder path.</returns>
        public static string GetLocalAppDataFolderPath()
        {
            var localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppDataFolder, WTRTextKeys.DefaultTitle);
        }
    }
}

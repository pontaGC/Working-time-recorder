using SharedLibraries.Retry;
using SharedLibraries.System.IO;

namespace SharedLibraries.Internals.System.IO
{
    /// <inheritdoc />
    internal class DirectoryImpl : IDirectory
    {
        #region Public Methods

        #region Check

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <inheritdoc />
        public bool IsDirectorySeparator(char value)
        {
            return value == Path.DirectorySeparatorChar || value == Path.AltDirectorySeparatorChar;
        }

        #endregion

        #region Create

        /// <inheritdoc />
        public DirectoryInfo CreateDirectory(string path)
        {
            return RetryHelper.InvokeWithRetry(() => Directory.CreateDirectory(path));
        }

        #endregion

        #region Query

        /// <inheritdoc />
        public DirectoryInfo? GetDirectoryInfo(string path)
        {
            try
            {
                return new DirectoryInfo(path);
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc />
        public IEnumerable<string> EnumerateFiles(string directoryPath, bool recursive)
        {
            return this.EnumerateFiles(directoryPath, "*.*", recursive);
        }

        /// <inheritdoc />
        public IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern, bool recursive)
        {
            var result = RetryHelper.InvokeWithRetry(() => Directory.EnumerateFiles(directoryPath, searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
            return result ?? Enumerable.Empty<string>();
        }

        #endregion

        #endregion
    }
}

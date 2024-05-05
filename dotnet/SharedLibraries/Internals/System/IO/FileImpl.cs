using SharedLibraries.Retry;
using SharedLibraries.System.IO;

namespace SharedLibraries.Internals.System.IO
{
    /// <inheritdoc />
    internal class FileImpl : IFile
    {
        #region Public Methods

        #region Check

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc />
        public bool HasExtension(string filename)
        {
            return Path.HasExtension(filename);
        }

        /// <inheritdoc />
        public bool IsPathFullyQualified(string path)
        {
            return path is not null && Path.IsPathFullyQualified(path);
        }

        #endregion

        #region Create/Open

        /// <inheritdoc />
        public FileStream Create(string path)
        {
            return RetryHelper.InvokeWithRetry(() => File.Create(path));
        }

        /// <inheritdoc />
        public FileStream Open(string filePath, FileAccess access, FileShare share)
        {
            return RetryHelper.InvokeWithRetry(() => File.Open(filePath, FileMode.Open, access, share));
        }

        /// <inheritdoc />
        public FileStream Open(string filePath, FileAccess access)
        {
            return RetryHelper.InvokeWithRetry(() => File.Open(filePath, FileMode.Open, access));
        }

        /// <inheritdoc />
        public FileStream Open(string filePath)
        {
            return RetryHelper.InvokeWithRetry(() => File.Open(filePath, FileMode.Open));
        }

        /// <inheritdoc />
        public FileStream OpenRead(string filePath)
        {
            return RetryHelper.InvokeWithRetry(() => File.OpenRead(filePath));
        }

        #endregion

        #region Query

        /// <inheritdoc />
        public FileInfo? GetFileInfo(string filePath)
        {
            try
            {
                return new FileInfo(filePath);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #endregion
    }
}

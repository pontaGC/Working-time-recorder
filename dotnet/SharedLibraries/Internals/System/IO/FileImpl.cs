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

        /// <inheritdoc />
        public FileStream OpenOrCreate(string filePath)
        {
            return RetryHelper.InvokeWithRetry(() => File.Open(filePath, FileMode.OpenOrCreate));
        }

        /// <inheritdoc />
        public FileStream OpenOrCreate(string filePath, FileAccess access)
        {
            return RetryHelper.InvokeWithRetry(() => File.Open(filePath, FileMode.OpenOrCreate, access));
        }

        /// <inheritdoc />
        public FileStream OpenOrCreate(string filePath, FileAccess access, FileShare share)
        {
            return RetryHelper.InvokeWithRetry(() => File.Open(filePath, FileMode.OpenOrCreate, access, share));
        }

        #endregion

        #region Delete

        /// <inheritdoc />
        public void Delete(string path)
        {
            var fileInfo = new FileInfo(path);
            fileInfo.IsReadOnly = false;
            RetryHelper.InvokeWithRetry(() => fileInfo.Delete());
        }

        #endregion

        #region Copy

        /// <inheritdoc />
        public void Copy(string sourceFilePath, string destFilePath, bool overwrite)
        {
            RetryHelper.InvokeWithRetry(() => File.Copy(sourceFilePath, destFilePath, overwrite));
        }

        #endregion

        #region Modify

        /// <inheritdoc />
        public string AddExtensionIfNotHave(string filename, string extension)
        {
            if (this.HasExtension(filename))
            {
                return filename;
            }

            return filename + extension;
        }

        /// <inheritdoc />
        public string ChangeExtension(string path, string extension)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return Path.ChangeExtension(path, extension);
        }

        /// <inheritdoc />
        public string ChangeFilename(string path, string filename)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(filename))
            {
                return string.Empty;
            }

            var directoryName = this.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directoryName))
            {
                return string.Empty;
            }

            return Path.Combine(directoryName, filename);
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

        /// <inheritdoc />
        public string GetExtension(string filename, bool removesDot)
        {
            if (Path.HasExtension(filename) == false)
            {
                return string.Empty;
            }

            var extension = Path.GetExtension(filename);
            if (removesDot)
            {
                return extension.Replace(".", string.Empty);
            }

            return extension;
        }

        /// <inheritdoc />
        public string GetFileName(string path) => Path.GetFileName(path);

        /// <inheritdoc />
        public string GetDirectoryName(string path, string defaultDirName = "")
        {
            try
            {
                return Path.GetDirectoryName(path) ?? defaultDirName;
            }
            catch
            {
                return defaultDirName;
            }
        }

        /// <inheritdoc />
        public string GetFileNameWithoutExtension(string path) => Path.GetFileNameWithoutExtension(path);

        #endregion

        #endregion
    }
}

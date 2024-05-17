using SharedLibraries.Retry;
using SharedLibraries.System.IO;

namespace SharedLibraries.Internals.System.IO
{
    /// <inheritdoc />
    internal class DirectoryImpl : IDirectory
    {
        #region Fields

        private readonly IFile file;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryImpl"/> class.
        /// </summary>
        /// <param name="file">The file operation.</param>
        public DirectoryImpl(IFile file)
        {
            this.file = file;
        }

        #endregion

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
        public DirectoryInfo Create(string path)
        {
            return RetryHelper.InvokeWithRetry(() => Directory.CreateDirectory(path));
        }

        #endregion

        #region Delete

        /// <inheritdoc />
        public void Delete(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            try
            {
                RetryHelper.InvokeWithRetry(() => Directory.Delete(path));
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void Clean(string directoryPath, bool recursive)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                return;
            }

            foreach (var filePath in this.EnumerateFiles(directoryPath, false))
            {
                this.file.Delete(filePath);
            }

            if (recursive)
            {
                foreach (var subDirPath in this.EnumerateDirectories(directoryPath, false))
                {
                    this.Delete(subDirPath);
                }
            }
        }

        #endregion

        #region Copy

        /// <inheritdoc />
        public IEnumerable<FileInfo> CopyFiles(string sourceDirPath, string destDirPath)
        {
            if (!this.Exists(destDirPath))
            {
                this.Create(destDirPath);
            }

            var copiedFileInfos = new List<FileInfo>();

            foreach (var filePath in this.EnumerateDirectories(sourceDirPath, true))
            {
                var copyingFilePath = Path.Combine(destDirPath, this.file.GetFileName(filePath));
                this.file.Copy(filePath, copyingFilePath, true);
                copiedFileInfos.Add(new FileInfo(copyingFilePath));
            }

            return copiedFileInfos;
        }

        /// <inheritdoc />
        public (bool IsFailed, IEnumerable<FileInfo> Copied) TryCopyFiles(string sourceDirPath, string destDirPath)
        {
            var copiedFileInfos = new List<FileInfo>();

            try
            {
                if (!this.Exists(destDirPath))
                {
                    this.Create(destDirPath);
                }

                foreach (var filePath in this.EnumerateDirectories(sourceDirPath, true))
                {
                    var copyingFilePath = Path.Combine(destDirPath, this.file.GetFileName(filePath));
                    this.file.Copy(filePath, copyingFilePath, true);
                    copiedFileInfos.Add(new FileInfo(copyingFilePath));
                }

                return (false, copiedFileInfos);
            }
            catch
            {
                return (true, copiedFileInfos);
            }
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
        public DirectoryInfo GetParent(string path)
        {
            try
            {
                return RetryHelper.InvokeWithRetry(() => Directory.GetParent(path));
            }
            catch
            {
                throw;
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
            return RetryHelper.InvokeWithRetry(() => Directory.EnumerateFiles(directoryPath, searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
        }

        /// <inheritdoc />
        public IEnumerable<FileInfo> EnumerateFileInfos(string directoryPath, bool recursive)
        {
            return this.EnumerateFileInfos(directoryPath, "*", recursive);
        }

        /// <inheritdoc />
        public IEnumerable<FileInfo> EnumerateFileInfos(string directoryPath, string searchPattern, bool recursive)
        {
            foreach(var filePath in this.EnumerateFiles(directoryPath, searchPattern, recursive))
            {
                yield return new FileInfo(filePath);
            }
        }

        /// <inheritdoc />
        public IEnumerable<string> EnumerateDirectories(string directoryPath, bool recursive)
        {
            return RetryHelper.InvokeWithRetry(() => Directory.EnumerateDirectories(directoryPath, "*.*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
        }

        /// <inheritdoc />
        public IEnumerable<DirectoryInfo> EnumerateDirectoryInfos(string directoryPath, bool recursive)
        {
            foreach(var dirPath in this.EnumerateDirectories(directoryPath, recursive))
            {
                yield return new DirectoryInfo(dirPath);
            }
        }

        #endregion

        #endregion
    }
}

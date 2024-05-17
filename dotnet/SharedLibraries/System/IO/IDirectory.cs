using System.Security;

namespace SharedLibraries.System.IO
{
    /// <summary>
    /// Responsible for operating a directory with retry design.
    /// </summary>
    public interface IDirectory
    {
        #region Check

        /// <summary>
        /// Checks whether a given directory exists.
        /// </summary>
        /// <param name="path">The directory path.</param>
        /// <returns><c>true</c>, if the directory exists, otherwise <c>false</c>.</returns>
        bool Exists(string path);

        /// <summary>
        /// Checks whether the character is the directory separator.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c>, if <c>value</c> is directory separator. Otherwise; <c>false</c>.</returns>
        bool IsDirectorySeparator(char value);

        #endregion

        #region Create

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        /// </summary>
        /// <param name="path">The path of the directory to create.</param>
        /// <exception cref="ArgumentNullException"><c>path</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><para>path is prefixed with, or contains, only a colon character (:).</para> or <para><c>path</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</para></exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="IOException"><para>The directory specified by path is a file.</para> or <para>The network name is not known.</para></exception>
        /// <exception cref="NotSupportedException"><c>path</c> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="UnauthorizedAccessException"><para>The caller does not have the required permission.</para> or <para><c>path</c> specified a file that is read-only.</para> or <para>path specified a file that is hidden.</para></exception>
        /// <return>An object that represents the directory at the specified path. This object is returned regardless of whether a directory at the specified path already exists.</return>
        DirectoryInfo Create(string path);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified directory.
        /// </summary>
        /// <param name="path">The directory path to delete.</param>
        /// <exception cref="ArgumentNullException"><c>path</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><c>path</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="IOException">
        /// A file with the same name and location specified by <c>path</c> exists.
        /// -or-
        /// The directory is the application's current working directory.
        /// -or-
        /// The directory specified by <c>path</c> is not empty.
        /// -or-
        /// The directory is read-only or contains a read-only file
        /// -or-
        /// The directory is being used by another process.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <c>path</c> does not exist or could not be found.
        /// -or-
        /// The specified path is invalid(for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException" />
        /// <remarks>This methods throws exceptions. See the details on <see href="https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.delete?view=netframework-4.8"/>.</remarks>
        void Delete(string path);

        /// <summary>
        /// Deletes the files or sub-directories in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The path to clean.</param>
        /// <param name="recursive">The value indicating deletes also the sub directories.</param>
        /// <exception cref="ArgumentNullException"><c>directoryPath</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><c>path</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        void Clean(string directoryPath, bool recursive);

        #endregion

        #region Copy

        /// <summary>
        /// Flat copy all the files from one directory to another.
        /// </summary>
        /// <param name="sourceDirPath">Path of a source directory.</param>
        /// <param name="destDirPath">Path of a destination directory.</param>
        /// <exception cref="ArgumentNullException"><c>sourceDirPath</c> or <c>destDirPath</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><para><c>sourceDirPath</c> or <c>destDirPath</c> specifies a directory.</para> or <para><c>sourceFileName</c> or <c>destFileName</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</para></exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <exception cref="DirectoryNotFoundException">The path specified in <c>sourceDirPath</c> or <c>destDirPath</c> is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="NotSupportedException"><c>sourceDirPath</c> or <c>destDirPath</c> is in an invalid format.</exception>
        /// <exception cref="UnauthorizedAccessException">https://learn.microsoft.com/en-us/dotnet/api/system.io.file.copy?view=net-8.0#system-io-file-copy(system-string-system-string-system-boolean):~:text=The%20caller%20does,is%20not%20hidden.</exception>
        /// <returns>The collection of the file which was copied sucessfully.</returns>
        IEnumerable<FileInfo> CopyFiles(string sourceDirPath, string destDirPath);

        /// <summary>
        /// Tries to flat copy all the files from one directory to another.
        /// </summary>
        /// <param name="sourceDirPath">Path of a source directory.</param>
        /// <param name="destDirPath">Path of a destination directory.</param>
        /// <returns>The tuple of the value indicating copying all files fails and the collection of the copied file.</returns>
        (bool IsFailed, IEnumerable<FileInfo> Copied) TryCopyFiles(string sourceDirPath, string destDirPath);

        #endregion

        #region Query

        /// <summary>
        /// Gets a directory information.
        /// </summary>
        /// <param name="path">A string specifying the path on which to create the <c>DirectoryInfo</c>.</param>
        /// <returns>The directory information if the <c>path</c> is valid and the caller has the required permission, otherwise, returns <c>null</c>.</returns>
        DirectoryInfo? GetDirectoryInfo(string path);

        /// <summary>
        /// Retrieves the parent directory of the specified path, including both absolute and relative paths.
        /// </summary>
        /// <param name="path">The path for which to retrieve the parent directory.</param>
        /// <returns>The parent directory path, or <c>null</c> if <c>path</c> is the root directory, including the root of a UNC server or share name.</returns>
        /// <exception cref="ArgumentNullException"><c>path</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><c>path</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="IOException">The directory specified by <c>path</c> is read-only.</exception>
        /// <exception cref="NotSupportedException"><c>path</c> is in an invalid format.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="UnauthorizedAccessException"><para>The caller does not have the required permission.</para> or <para><c>path</c> specified a file that is read-only.</para> or <para>path specified a file that is hidden.</para></exception>
        DirectoryInfo GetParent(string path);

        /// <summary>
        /// Enumerates all files in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to be searched.</param>
        /// <param name="recursive">Whether to also perform the action on files in all subdirectories.</param>
        /// <exception cref="ArgumentNullException"><c>directoryPath</c> or <c>searchPattern</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><para><c>directoryPath</c> is a zero-length string, contains only white space, or contains one or more invalid characters</para>.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><c>directoryPath</c> is a file name..</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permissions.</exception>
        /// <returns>An enumerable collection of the file paths found in the specified directory.</returns>
        IEnumerable<string> EnumerateFiles(string directoryPath, bool recursive);

        /// <summary>
        /// Enumerates files in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to be searched.</param>
        /// <param name="searchPattern">The search string to match against the file names.</param>
        /// <param name="recursive">Whether to also perform the action on files in all subdirectories.</param>
        /// <exception cref="ArgumentNullException"><c>directoryPath</c> or <c>searchPattern</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><para><c>directoryPath</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</para> or <para><c>searchPattern</c> does not contain a valid pattern</para></exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><c>directoryPath</c> is a file name..</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permissions.</exception>
        /// <returns>An enumerable collection of the file paths found in the specified directory.</returns>
        IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern, bool recursive);

        /// <summary>
        /// Enumerates all files in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to be searched.</param>
        /// <param name="recursive">Whether to also perform the action on files in all subdirectories.</param>
        /// <exception cref="ArgumentNullException"><c>directoryPath</c> or <c>searchPattern</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><para><c>directoryPath</c> is a zero-length string, contains only white space, or contains one or more invalid characters</para>.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><c>directoryPath</c> is a file name..</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permissions.</exception>
        /// <returns>An enumerable collection of the file paths found in the specified directory.</returns>
        IEnumerable<FileInfo> EnumerateFileInfos(string directoryPath, bool recursive);

        /// <summary>
        /// Enumerates files in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to be searched.</param>
        /// <param name="searchPattern">The search string to match against the file names.</param>
        /// <param name="recursive">Whether to also perform the action on files in all subdirectories.</param>
        /// <exception cref="ArgumentNullException"><c>directoryPath</c> or <c>searchPattern</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><para><c>directoryPath</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</para> or <para><c>searchPattern</c> does not contain a valid pattern</para></exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><c>directoryPath</c> is a file name..</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permissions.</exception>
        /// <returns>An enumerable collection of the file paths found in the specified directory.</returns>
        IEnumerable<FileInfo> EnumerateFileInfos(string directoryPath, string searchPattern, bool recursive);

        /// <summary>
        /// Enumerates subdirectories in the specified directory
        /// </summary>
        /// <param name="directoryPath">The path of the directory to be searched.</param>
        /// <param name="recursive">Whether to also perform the action on all subdirectories.</param>
        /// <returns>An enumerable collection of the directory paths found in the specified directory.</returns>
        /// <exception cref="ArgumentNullException"><c>directoryPath</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><c>directoryPath</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><c>directoryPath</c> is a file name..</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permissions.</exception>
        IEnumerable<string> EnumerateDirectories(string directoryPath, bool recursive);

        /// <summary>
        /// Enumerates subdirectories in the specified directory
        /// </summary>
        /// <param name="directoryPath">The path of the directory to be searched.</param>
        /// <param name="recursive">Whether to also perform the action on all subdirectories.</param>
        /// <returns>An enumerable collection of the directory paths found in the specified directory.</returns>
        /// <exception cref="ArgumentNullException"><c>directoryPath</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><c>directoryPath</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><c>directoryPath</c> is a file name..</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permissions.</exception>
        IEnumerable<DirectoryInfo> EnumerateDirectoryInfos(string directoryPath, bool recursive);

        #endregion
    }
}

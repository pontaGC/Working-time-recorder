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
        DirectoryInfo CreateDirectory(string path);

        #endregion

        #region Query

        /// <summary>
        /// Gets a directory information.
        /// </summary>
        /// <param name="path">A string specifying the path on which to create the <c>DirectoryInfo</c>.</param>
        /// <returns>The directory information if the <c>path</c> is valid and the caller has the required permission, otherwise, returns <c>null</c>.</returns>
        DirectoryInfo? GetDirectoryInfo(string path);

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

        #endregion
    }
}

namespace SharedLibraries.System.IO
{
    /// <summary>
    /// Responsible for operating a file with retry design.
    /// </summary>
    public interface IFile
    {
        #region Check

        /// <summary>
        /// Checks whether a given file exists.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns><c>true</c>, if the file exists, otherwise <c>false</c>.</returns>
        bool Exists(string path);

        /// <summary>
        /// Checks whether or not the file name has a file extension.
        /// </summary>
        /// <param name="filename">The file name to check.</param>
        /// <returns><c>true</c>, if <c>filename</c> includes a file extension. Otherwise; <c>false</c>.</returns>
        bool HasExtension(string filename);

        /// <summary>
        /// Returns a value that indicates whether a file path is fully qualified.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>
        /// <c>true</c> if <c>path</c> is fixed to a specific drive or UNC path.
        /// Otherwise; <c>false</c> if <c>path</c> is relative to the current drive or working directory.
        /// </returns>
        bool IsPathFullyQualified(string path);

        #endregion

        #region Create/Open

        /// <summary>
        /// Creates, or truncates and overwrites, a file in the specified path.
        /// </summary>
        /// <param name="filePath">The path of the file to create.</param>
        /// <returns>The stream of the created file.</returns>
        /// <exception cref="Exception">See <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.file.open?view=net-8.0"/>.</exception>
        FileStream Create(string filePath);

        /// <summary>
        /// Opens a file on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
        /// <see cref="FileMode"/> is <c>Open</c>.
        /// </summary>
        /// <param name="filePath">The file path to create a stream.</param>
        /// <param name="access">
        /// A bitwise combination of the enumeration values that determines how the file can be accessed by the <see cref="FileStream"/> object.
        /// This also determines the values returned by the <c>CanRead</c> and <c>CanWrite</c> properties of the <see cref="FileStream"/> object.
        /// <c>CanSeek</c> is true if path specifies a disk file.
        /// </param>
        /// <param name="share">A bitwise combination of the enumeration values that determines how the file will be shared by processes.</param>
        /// <returns>A file stream contained by the specified <c>filePath</c>.</returns>
        /// <exception>See <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.file.open?view=net-8.0"/>.</exception>
        FileStream Open(string filePath, FileAccess access, FileShare share);

        /// <summary>
        /// Opens a file on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
        /// <see cref="FileMode"/> is <c>Open</c>.
        /// </summary>
        /// <param name="filePath">The file path to create a stream.</param>
        /// <param name="access">
        /// A bitwise combination of the enumeration values that determines how the file can be accessed by the <see cref="FileStream"/> object.
        /// This also determines the values returned by the <c>CanRead</c> and <c>CanWrite</c> properties of the <see cref="FileStream"/> object.
        /// <c>CanSeek</c> is true if path specifies a disk file.
        /// </param>
        /// <returns>A file stream contained by the specified <c>filePath</c>.</returns>
        /// <exception>See <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.file.open?view=net-8.0"/>.</exception>
        FileStream Open(string filePath, FileAccess access);

        /// <summary>
        /// Opens a file on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
        /// <see cref="FileMode"/> is <c>Open</c>.
        /// </summary>
        /// <param name="filePath">The file path to create a stream.</param>
        /// <returns>A file stream contained by the specified <c>filePath</c>.</returns>
        /// <exception>See <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.file.open?view=net-8.0"/>.</exception>
        FileStream Open(string filePath);

        /// <summary>
        /// Opens a file on the specified path as read-only.
        /// </summary>
        /// <param name="filePath">The file path to create a stream.</param>
        /// <returns>A file stream contained by the specified <c>filePath</c>.</returns>
        /// <exception>See <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.file.open?view=net-8.0"/>.</exception>
        FileStream OpenRead(string filePath);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="path">The path of the file to delete.</param>
        /// <exception cref="ArgumentNullException"><c>path</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><c>path</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="IOException">
        /// The specified file is in use.
        /// -or-
        /// There is an open handle on the file, and the operating system is Windows XP or earlier.This open handle can result from enumerating directories and files. </exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="NotSupportedException"><c>path</c> is in an invalid format</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// -or-n
        /// The file is an executable file that is in use.
        /// -or-
        /// <c>path</c> is a directory.
        /// </exception>
        void Delete(string path);

        #endregion

        #region Copy

        /// <summary>
        /// Copies a file from one directory to another.
        /// </summary>
        /// <param name="sourceFilePath">Path of a source file.</param>
        /// <param name="destFilePath">Path of a destination file.</param>
        /// <param name="overwrite">Overwrite option.</param>
        /// <exception cref="ArgumentNullException"><c>sourceFilePath</c> or <c>destFilePath</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><para><c>sourceFilePath</c> or <c>destFilePath</c> specifies a directory.</para> or <para><c>sourceFileName</c> or <c>destFileName</c> is a zero-length string, contains only white space, or contains one or more invalid characters.</para></exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="IOException"><c>destFilePath</c> exists and <c>overwrite</c> is <c>false</c>.-or-An I/O error has occurred.</exception>
        /// <exception cref="FileNotFoundException"><c>sourceFilePath</c> was not found.</exception>
        /// <exception cref="DirectoryNotFoundException">The path specified in <c>sourceFilePath</c> or <c>destFilePath</c> is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="NotSupportedException"><c>sourceFilePath</c> or <c>destinationFilePath</c> is in an invalid format.</exception>
        /// <exception cref="UnauthorizedAccessException">https://learn.microsoft.com/en-us/dotnet/api/system.io.file.copy?view=net-8.0#system-io-file-copy(system-string-system-string-system-boolean):~:text=The%20caller%20does,is%20not%20hidden.</exception>
        void Copy(string sourceFilePath, string destFilePath, bool overwrite);

        #endregion

        #region Modify

        /// <summary>
        /// Adds a file extension if the target filename including it.
        /// </summary>
        /// <param name="filename">The file name adding a file extension to.</param>
        /// <param name="extension">The file extension.</param>
        /// <returns>A file name with <c>extension</c> if <c>filename</c> has no extension. Otherwise; <c>filename</c>.</returns>
        string AddExtensionIfNotHave(string filename, string extension);

        /// <summary>
        /// Changes the extension of a path string.
        /// If <paramref name="extension"/> is set to <c>null</c>, removes an extension from <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path information to modify.</param>
        /// <param name="extension">
        /// The new extension (with or without a leading period). Specify <c>null</c> to remove an existing extension from <c>path</c>.
        /// </param>
        /// <returns>The modified path. If the change fails, returns an empty string.</returns>
        string ChangeExtension(string path, string extension);

        /// <summary>
        /// Changes file name.
        /// If <paramref name="filename"/> is set to <c>null</c>, removes an existing file name.
        /// </summary>
        /// <param name="path">The path information to modify.</param>
        /// <param name="filename">The new file name.</param>
        /// <returns>The modified path. If the change fails, returns an empty string.</returns>
        string ChangeFilename(string path, string filename);

        #endregion

        #region Query

        /// <summary>
        /// Gets a file information.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the new file, or the relative file name. Do not end the path with the directory separator character..</param>
        /// <returns>The file information if the <c>fileName</c> is valid and the caller has the required permission, otherwise, returns <c>null</c>.</returns>
        FileInfo? GetFileInfo(string fileName);

        /// <summary>
        /// Gets a file extension. The default file extension has dot like '.txt'.
        /// To get a file extension without dot like 'txt', sets <paramref name="removesDot"/> to <c>true</c>.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <param name="removesDot">The value indicating removing the dot character from the file extension.</param>
        /// <returns>A file extension, if the file name has a file extension. Otherwise; an empty string.</returns>
        string GetExtension(string filename, bool removesDot = false);

        /// <summary>
        /// Gets the filename from the file path.
        /// </summary>
        /// <param name="path">Path of a file.</param>
        /// <returns>A filename from path</returns>
        string GetFileName(string path);

        /// <summary>
        /// Gets the directory name from the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// A directory name which has the file.
        /// Returns <c>null</c> if path denotes a root directory or is <c>null</c>.
        /// Returns <paramref name="defaultDirName"/> if path does not contain directory information.
        /// </returns>
        string GetDirectoryName(string path, string defaultDirName = "");

        /// <summary>
        /// Gets the filename without extension from the file path.
        /// </summary>
        /// <param name="path">Path of a file.</param>
        /// <returns>A filename without the file extension.</returns>
        string GetFileNameWithoutExtension(string path);

        #endregion
    }
}

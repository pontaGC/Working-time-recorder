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

        #region Query

        /// <summary>
        /// Gets a file information.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the new file, or the relative file name. Do not end the path with the directory separator character..</param>
        /// <returns>The file information if the <c>fileName</c> is valid and the caller has the required permission, otherwise, returns <c>null</c>.</returns>
        FileInfo? GetFileInfo(string fileName);

        #endregion
    }
}

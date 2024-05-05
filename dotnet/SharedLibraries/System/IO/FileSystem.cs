using System.Diagnostics;
using SharedLibraries.Internals.System.IO;

namespace SharedLibraries.System.IO
{
    /// <summary>
    /// The file system.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Gets a file operation service.
        /// </summary>
        public static IFile File { [DebuggerStepThrough] get; } = new FileImpl();

        /// <summary>
        /// Gets a directory operation service.
        /// </summary>
        public static IDirectory Directory { [DebuggerStepThrough] get; } = new DirectoryImpl();
    }
}

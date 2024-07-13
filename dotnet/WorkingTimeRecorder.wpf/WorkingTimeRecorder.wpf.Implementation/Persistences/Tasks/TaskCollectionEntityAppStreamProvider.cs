using SharedLibraries.System.IO;
using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Paths;
using WorkingTimeRecorder.Core.Persistences;
using WorkingTimeRecorder.Core.Shared;

namespace WorkingTimeRecorder.wpf.Implementation.Persistences.Tasks
{
    internal sealed class TaskCollectionEntityAppStreamProvider : IEntityAppStreamProvider<TaskCollection>
    {
        private const string TaskFileName = "Tasks.xml";

        /// <summary>Gets a file path to read or write the task data.</summary>
        private readonly string taskFilePath;
        private readonly IFile file = FileSystem.File;

        public TaskCollectionEntityAppStreamProvider()
        {
            taskFilePath = GetTaskFilePath();
        }

        /// <inheritdoc />
        public string EntityType => TaskConstants.EntityType;

        /// <inheritdoc />
        public Stream CreateLoadStream()
        {
            return file.OpenRead(taskFilePath);
        }

        /// <inheritdoc />
        public Stream CreateSaveStream()
        {
            return file.OpenOrCreate(taskFilePath, FileAccess.Write);
        }

        private static string GetTaskFilePath()
        {
            var folderPath = SpecialFolderPathProvider.GetLocalAppDataFolderPath();
            return Path.Combine(folderPath, TaskFileName);
        }
    }
}

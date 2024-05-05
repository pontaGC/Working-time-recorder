using System.Reflection;
using System.Text.Json;
using SharedLibraries.System.IO;

namespace WorkingTimeRecorder.Core.Languages.Internal
{
    /// <summary>
    /// The implementation of the <see cref="ITextLocalizer"/>.
    /// </summary>
    internal sealed class TextLocalizer : ITextLocalizer
    {
        #region Fields

        private const string ResourceFolderPath = "Languages";
        private readonly Dictionary<string, string> resources;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLocalizer"/> class.
        /// </summary>
        /// <param name="cultureName">The culture name indicating language.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cultureName"/> is an empty string.</exception>
        /// <exception cref="DirectoryNotFoundException">The language resource folder does not exist.</exception>
        public TextLocalizer(string cultureName)
        {
            ArgumentException.ThrowIfNullOrEmpty(cultureName, nameof(CultureName));

            this.CultureName = cultureName;
            this.resources = LoadResources(cultureName);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public string CultureName { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public string Localize(string textId)
        {
            if (string.IsNullOrEmpty(textId))
            {
                return string.Empty;
            }

            if (this.resources.TryGetValue(textId, out var localizedText))
            {
                return localizedText;
            }

            return textId;
        }

        /// <inheritdoc />
        public bool TryLocalize(string textId, out string localizedText)
        {
            localizedText = Localize(textId);
            return !string.IsNullOrEmpty(localizedText);
        }

        #endregion

        #region Private Methods

        private static Dictionary<string, string> LoadResources(string cultureName)
        {
            var resourceFolderPath = CreateResourceFolderFullPath(cultureName);
            if (!FileSystem.Directory.Exists(resourceFolderPath))
            {
                throw new DirectoryNotFoundException($"The language resource folder does not exist: {resourceFolderPath}");
            }

            var allItems = new List<LanguageTextItem>();
            var allResourceFilePaths = FileSystem.Directory.EnumerateFiles(resourceFolderPath, true);
            foreach (var filePath in allResourceFilePaths)
            {
                var resourceFileModel = LoadOneResourceFile(filePath);
                allItems.AddRange(resourceFileModel.TextItems);
            }

            return allItems.ToDictionary(x => x.Id, x => x.Text);
        }

        private static string CreateResourceFolderFullPath(string cultureName)
        {
            var executingAssemblyFilePath = Assembly.GetExecutingAssembly().Location;
            var executingAssemblyFileInfo = FileSystem.File.GetFileInfo(executingAssemblyFilePath);
            if (executingAssemblyFileInfo is null)
            {
                return string.Empty;
            }

            return Path.Combine(executingAssemblyFileInfo.DirectoryName, ResourceFolderPath, cultureName);
        }

        private static LanguageResourceFile LoadOneResourceFile(string resourceFilePath)
        {
            using var fileStream = FileSystem.File.OpenRead(resourceFilePath);
            return JsonSerializer.Deserialize<LanguageResourceFile>(fileStream);
        }

        #endregion
    }
}

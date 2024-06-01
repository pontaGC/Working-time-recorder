using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

using WorkingTimeRecorder.Core.Icons;
using WorkingTimeRecorder.wpf.Presentation.Core.Extensions;
using WorkingTimeRecorder.wpf.Presentation.Core.Icons;

namespace WorkingTimeRecorder.wpf.Presentation.Icons
{
    /// <summary>
    /// The icon service.
    /// </summary>
    internal sealed class IconService : IconServiceBase<UIElement>, IIconService
    {
        #region Fields

        private readonly IconResourceLoader iconResourceLoader = new IconResourceLoader();

        #endregion

        #region Singleton instance constructors

        static IconService()
        {
            Instance = new IconService();
        }

        private IconService()
        {
        }

        internal static IconService Instance { get; }

        #endregion

        #region Protected Methods

        /// <inheritdoc />
        protected override UIElement? GetIcon([NotNull] string iconKey)
        {
            if (this.IconImages.TryGetValue(iconKey, out var iconElement))
            {
                return iconElement.Copy();
            }

            var icon = this.LoadIcon<UIElement>(iconKey);
            if (icon is null)
            {
                return null;
            }

            this.AddOrUpdateIcon(iconKey, icon);
            return icon.Copy();
        }

        #endregion

        #region Private Methods

        private T? LoadIcon<T>(string iconKey)
            where T : DispatcherObject
        {
            if (!this.iconResourceLoader.Exists(iconKey))
            {
                return null;
            }

            Stream? iconResourceStream = null;
            try
            {
                var loaded = this.iconResourceLoader.Load(iconKey);
                if (loaded.IsFailed)
                {
                    return null;
                }

                iconResourceStream = loaded.Stream;
                var icon = XamlReader.Load(iconResourceStream) as T;
                this.iconResourceLoader.MarkLoadCompleted(iconKey);
                return icon;
            }
            finally
            {
                iconResourceStream?.Dispose();
            }
        }

        #endregion
    }
}

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace WorkingTimeRecorder.Core.Icons
{
    /// <summary>
    /// The base implementation of the <see cref="IIconService{TImage}"/>.
    /// </summary>
    public abstract class IconServiceBase<TImage> : IIconService<TImage>
        where TImage : class
    {
        #region Properties

        /// <summary>
        /// Gets a dictionary of the icon images.
        /// </summary>
        protected IDictionary<string, TImage> IconImages { get; } = new ConcurrentDictionary<string, TImage>();

        #endregion

        #region Public Methods

        /// <inheritdoc />
        void IIconService<TImage>.AddOrUpdateIcon(string iconKey, TImage icon)
        {
            ArgumentNullException.ThrowIfNull(nameof(iconKey));
            this.AddOrUpdateIcon(iconKey, icon);
        }

        /// <inheritdoc />
        TImage? IIconService<TImage>.GetIcon(string iconKey)
        {
            ArgumentNullException.ThrowIfNull(nameof(iconKey));
            return this.GetIcon(iconKey);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This method is called by <see cref="IIconService{TImage}.AddOrUpdateIcon(string, TImage)"/>.
        /// </summary>
        protected void AddOrUpdateIcon([NotNull]string iconKey, TImage icon)
        {
            if (this.IconImages.TryGetValue(iconKey, out var registeredIcon))
            {
                this.IconImages[iconKey] = icon;
            }
            else
            {
                this.IconImages.TryAdd(iconKey, icon);
            }
        }

        /// <summary>
        /// This method is called by <see cref="IIconService{TImage}.GetIcon(string)"/>.
        /// </summary>
        protected virtual TImage? GetIcon([NotNull]string iconKey)
        {
            if (this.IconImages.TryGetValue(iconKey, out var icon))
            {
                return icon;
            }

            return null;
        }

        #endregion
    }
}

namespace WorkingTimeRecorder.Core.Icons
{
    /// <summary>
    /// Responsible for providing Icon.
    /// </summary>
    /// <typeparam name="TImage">The type of an icon image.</typeparam>
    public interface IIconService<TImage> where TImage : class
    {
        /// <summary>
        /// Adds a icon's <see cref="TImage"/> and an identifier indicating it.
        /// If the given icon key has been registered, updates the icon.
        /// </summary>
        /// <param name="iconKey">The identifier indicating setting icon.</param>
        /// <param name="icon">The adding icon.</param>
        /// <exception cref="ArgumentNullException">The <c>iconKey</c> is <c>null</c> or an empty string, or The <c>icon</c> is <c>null</c>.</exception>
        void AddOrUpdateIcon(string iconKey, TImage icon);

        /// <summary>
        /// Gets a specified icon's <see cref="TImage"/>.
        /// </summary>
        /// <param name="iconKey">The icon's identifier.</param>
        /// <returns>The <see cref="ImageSource"/> indicating the specified icon, if the icon's resource is found. Otherwise; <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">The <c>iconKey</c> is <c>null</c> or an empty string.</exception>
        TImage? GetIcon(string iconKey);
    }
}

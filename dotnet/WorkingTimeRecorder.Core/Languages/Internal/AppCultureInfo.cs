using System.Globalization;

namespace WorkingTimeRecorder.Core.Languages.Internal
{
    /// <summary>
    /// The app culture information.
    /// </summary>
    internal class AppCultureInfo : IAppCultureInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCultureInfo"/> class.
        /// </summary>
        /// <param name="cultureInfo">The source.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureInfo"/> is <c>null</c>.</exception>
        public AppCultureInfo(CultureInfo cultureInfo)
        {
            ArgumentNullException.ThrowIfNull(cultureInfo);

            this.Source = cultureInfo;
        }

        /// <inheritdoc />
        public string Name => Source.Name;

        internal CultureInfo Source { get; }

        /// <inheritdoc />
        public object? GetFormat(Type? formatType)
        {
            return Source.GetFormat(formatType);
        }
    }
}

using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="UIElement"/>.
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Gets a copy instance of the given source element.
        /// </summary>
        /// <typeparam name="T">The type of source element.</typeparam>
        /// <param name="source">The copy source.</param>
        /// <returns>The copy instance of <c>source</c>.</returns>
        public static T? Copy<T>(this T? source) where T : UIElement
        {
            if (source is null)
            {
                return source;
            }

            using (var ms = new MemoryStream())
            {
                XamlWriter.Save(source, ms);
                ms.Seek(0, SeekOrigin.Begin);
                return XamlReader.Load(ms) as T;
            }
        }
    }
}

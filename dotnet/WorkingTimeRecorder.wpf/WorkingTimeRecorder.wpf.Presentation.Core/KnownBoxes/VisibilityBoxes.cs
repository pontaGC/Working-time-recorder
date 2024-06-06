using System.Diagnostics;

namespace System.Windows
{
    /// <summary>
    /// Static visibility boxed values.
    /// </summary>
    public static class VisibilityBoxes
    {
        public static readonly object Visible = Visibility.Visible;
        public static readonly object Hidden = Visibility.Hidden;
        public static readonly object Collapsed = Visibility.Collapsed;

        /// <summary>
        /// Gets a pre-boxed value. This method can be used to avoid boxing.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The boxed value.</returns>
        public static object ToObject(this Visibility source)
        {
            switch (source)
            {
                case Visibility.Visible:
                    return Visible;
                case Visibility.Hidden:
                    return Hidden;
                case Visibility.Collapsed:
                    return Collapsed;
                default:
                    Debug.Fail($"Found the unknown value: {source}");
                    return Visible;
            }
        }
    }
}

using System.Windows;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Behaviors
{
    /// <summary>
    /// Closing a window behavior.
    /// </summary>
    public sealed class CloseWindowBehavior
    {
        /// <summary>
        /// The window close dependency property.
        /// </summary>
        public static readonly DependencyProperty CloseProperty
            = DependencyProperty.RegisterAttached("Close",
                                                  typeof(bool?),
                                                  typeof(CloseWindowBehavior),
                                                  new UIPropertyMetadata(null, OnClose));

        /// <summary>
        /// Gets a value indicating whether closes the <see cref="Window"/>.
        /// </summary>
        /// <param name="target">The target <see cref="DependencyObject"/>.</param>
        /// <returns>The value value indicating whether closes the <see cref="Window"/>.</returns>
        public static bool? GetClose(DependencyObject target)
        {
            return (bool?)target.GetValue(CloseProperty);
        }

        /// <summary>
        /// Sets a value indicating whether closes the <see cref="Window"/>.
        /// </summary>
        /// <param name="target">The target <see cref="DependencyObject"/>.</param>
        /// <param name="value">The setting value.</param>
        public static void SetClose(DependencyObject target, bool? value)
        {
            target.SetValue(CloseProperty, value);
        }

        private static void OnClose(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is not Window window)
            {
                return;
            }

            var newDialogResult = (bool?)e.NewValue;
            if (window.DialogResult != newDialogResult)
            {
                window.DialogResult = newDialogResult;
                window.Close();
            }
        }
    }
}
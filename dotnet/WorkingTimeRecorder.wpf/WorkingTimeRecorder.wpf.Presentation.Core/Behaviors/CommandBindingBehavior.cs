using System.Windows;
using System.Windows.Input;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Behaviors
{
    /// <summary>
    /// The <see cref="CommandBindingCollection"/> behavior.
    /// </summary>
    public sealed class CommandBindingsBehavior
    {
        #region Properties

        /// <summary>
        /// Represents a binding property for the <see cref="CommandBindingCollection"/>.
        /// </summary>
        public static readonly DependencyProperty CommandBindingsProperty =
            DependencyProperty.RegisterAttached("CommandBindings",
                                                typeof(CommandBindingCollection),
                                                typeof(CommandBindingsBehavior),
                                                new PropertyMetadata(null, OnCommandBindingsChanged));

        #endregion

        #region Methods

        /// <summary>
        /// Gets a collection of the command binding.
        /// </summary>
        /// <param name="target">The target UI element.</param>
        /// <returns>A collection of the command binding.</returns>
        public static CommandBindingCollection GetCommandBindings(UIElement target)
        {
            return (CommandBindingCollection)target.GetValue(CommandBindingsProperty);
        }

        /// <summary>
        /// Sets a new command bindings.
        /// </summary>
        /// <param name="target">The target UI element.</param>
        /// <param name="value">The value to set.</param>
        public static void SetCommandBindings(UIElement target, CommandBindingCollection value)
        {
            target.SetValue(CommandBindingsProperty, value);
        }

        #endregion

        #region Methods

        private static void OnCommandBindingsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is not UIElement uiElement)
            {
                return;
            }

            if (e.NewValue is CommandBindingCollection newBindings)
            {
                uiElement.CommandBindings.Clear();
                uiElement.CommandBindings.AddRange(newBindings);
            }
        }

        #endregion
    }
}
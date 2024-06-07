using System.Windows;

namespace WorkingTimeRecorder.wpf.Presentation.Core
{
    /// <summary>
    /// The command parameter.
    /// </summary>
    public class CommandParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameter"/> class.
        /// </summary>
        /// <param name="ownerWidnow">The owner window.</param>
        /// <param name="parameter">The command parameter.</param>
        public CommandParameter(Window? ownerWidnow, object? parameter)
        {
            this.OwnerWidnow = ownerWidnow;
            this.Parameter = parameter;
        }

        public static readonly CommandParameter Empty = new CommandParameter(null, null);

        /// <summary>
        /// Gets a owner window.
        /// </summary>
        public Window? OwnerWidnow { get; }

        /// <summary>
        /// Gets a command parameter.
        /// </summary>
        public object? Parameter { get; }
    }
}

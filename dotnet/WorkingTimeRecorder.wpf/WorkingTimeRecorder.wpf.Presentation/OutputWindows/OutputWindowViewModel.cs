using System.Collections.ObjectModel;
using WorkingTimeRecorder.Core.Mvvm;

namespace WorkingTimeRecorder.wpf.Presentation.OutputWindows
{
    /// <summary>
    /// Represents an output window view-model.
    /// </summary>
    internal sealed class OutputWindowViewModel : ViewModelBase
    {
        private OutputWindowMessage selectedMessage;

        /// <summary>
        /// Gets or sets a selected message.
        /// </summary>
        public OutputWindowMessage SelectedMessage
        {
            get => selectedMessage;
            set => SetProperty(ref selectedMessage, value);
        }

        /// <summary>
        /// Gets a collection of messages.
        /// </summary>
        public ObservableCollection<OutputWindowMessage> Messages { get; } = new ObservableCollection<OutputWindowMessage>();
    }
}
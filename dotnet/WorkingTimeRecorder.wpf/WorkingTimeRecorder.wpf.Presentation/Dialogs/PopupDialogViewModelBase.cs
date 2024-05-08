using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using WorkingTimeRecorder.Core.Mvvm;

namespace WorkingTimeRecorder.wpf.Presentation.Dialogs
{
    /// <summary>
    /// The base view-model for the popup dialogs.
    /// </summary>
    internal class PopupDialogViewModelBase : ViewModelBase
    {
        #region Fields

        private string title;
        private string text;
        private string affirmativeButtonText;
        private string negativeButtonText;
        private ImageSource dialogIcon;
        private ImageSource messageImage;
        private Visibility negativeButtonVisibility;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a dialog title.
        /// </summary>
        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        /// <summary>
        /// Gets or sets a popup text.
        /// </summary>
        public string Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }

        /// <summary>
        /// Gets an affirmative button text.
        /// </summary>
        public string AffirmativeButtonText
        {
            get => this.affirmativeButtonText;
            set => this.SetProperty(ref this.affirmativeButtonText, value);
        }

        /// <summary>
        /// Gets a negative button text.
        /// </summary>
        public string NegativeButtonText
        {
            get => this.negativeButtonText;
            set => this.SetProperty(ref this.negativeButtonText, value);
        }

        /// <summary>
        /// Gets or sets a dialog icon on the title bar.
        /// </summary>
        public ImageSource DialogIcon
        {
            get => this.dialogIcon;
            set => this.SetProperty(ref this.dialogIcon, value);
        }

        /// <summary>
        /// Gets or sets a image of the message.
        /// </summary>
        public ImageSource MessageImage
        {
            get => this.messageImage;
            set => this.SetProperty(ref this.messageImage, value);
        }

        /// <summary>
        /// Gets or sets a visibility of the negative button.
        /// </summary>
        public Visibility NegativeButtonVisibility
        {
            get => this.negativeButtonVisibility;
            set => this.SetProperty(ref this.negativeButtonVisibility, value);
        }

        /// <summary>
        /// Gets an affirmative action command.
        /// </summary>
        public ICommand AffirmCommand { get; protected set; }

        /// <summary>
        /// Gets a negative action command.
        /// </summary>
        public ICommand DenyCommand { get; protected set; }

        #endregion
    }
}

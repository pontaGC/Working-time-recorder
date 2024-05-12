using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

using MessageBoxResults = WorkingTimeRecorder.wpf.Presentation.Core.Dialogs.MessageBoxResults;

namespace WorkingTimeRecorder.wpf.Presentation.Dialogs.MessageBox
{
    /// <summary>
    /// The view controller for the message dialog.
    /// </summary>
    internal sealed class MessageBoxViewModel : PopupDialogViewModelBase
    {
        #region Fields

        private readonly MessageBoxButtons displayButton;
        private readonly MessageBoxResults? defaultResult;

        private bool? dialogResult;
        private Visibility cancelButtonVisibility;
        private MessageBoxImages messageDialogImage;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxViewModel"/> class.
        /// </summary>
        /// <param name="displayButton">The value that specifies which button or buttons to display.</param>
        /// <param name="defaultResult">The value determines the button is focused by default.</param>
        public MessageBoxViewModel(MessageBoxButtons displayButton, MessageBoxResults? defaultResult)
        {
            this.displayButton = displayButton;
            this.defaultResult = defaultResult;

            this.InitializeButtonsVisibility(displayButton);
            this.InitializeResult(displayButton, defaultResult);

            this.AffirmCommand = new DelegateCommand(this.Affirm);
            this.DenyCommand = new DelegateCommand(this.Deny);
            this.CancelCommand = new DelegateCommand(this.Cancel);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating the affirmative button is default.
        /// </summary>
        public bool IsAffirmativeButtonDefault => !(this.IsNegativeButtonDefault || this.IsCancelButtonDefault);

        /// <summary>
        /// Gets a value indicating the negative button is default.
        /// </summary>
        public bool IsNegativeButtonDefault
        {
            get
            {
                if (this.NegativeButtonVisibility != Visibility.Visible)
                {
                    return false;
                }

                switch (this.defaultResult)
                {
                    case MessageBoxResults.No:
                        return true;

                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating the cancel button is default.
        /// </summary>
        public bool IsCancelButtonDefault
        {
            get
            {
                if (this.CancelButtonVisibility != Visibility.Visible)
                {
                    return false;
                }

                return this.defaultResult == MessageBoxResults.Cancel;
            }
        }

        /// <summary>
        /// Gets or sets a dialog result.
        /// </summary>
        public bool? DialogResult
        {
            get => this.dialogResult;
            set => SetProperty(ref this.dialogResult, value);
        }

        /// <summary>
        /// Gets or sets a message box image.
        /// </summary>
        public MessageBoxImages MessageBoxImage
        {
            get => this.messageDialogImage;
            set => SetProperty(ref this.messageDialogImage, value);
        }

        /// <summary>
        /// Gets a message dialog result.
        /// </summary>
        public MessageBoxResults Result { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating the cancel button's visible state. 
        /// </summary>
        /// <remarks>This value is used when <see cref="MessageBoxButton"/> is <c>YesNoCancel</c>.</remarks>
        public Visibility CancelButtonVisibility
        {
            get => cancelButtonVisibility;
            set => SetProperty(ref cancelButtonVisibility, value);
        }

        /// <summary>
        /// Gets a command that executes cancel action.
        /// </summary>
        /// <remarks>This value is used when <see cref="MessageBoxButton"/> is <c>YesNoCancel</c>.</remarks>
        public ICommand CancelCommand { get; }

        #endregion

        #region Private Methods

        private void Affirm()
        {
            switch (this.displayButton)
            {
                case MessageBoxButtons.OK:
                case MessageBoxButtons.OKCancel:
                    this.Result = MessageBoxResults.OK;
                    break;

                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.YesNo:
                    this.Result = MessageBoxResults.Yes;
                    break;

                default:
                    this.Result = MessageBoxResults.OK;
                    break;
            }

            this.Close();
        }

        private void Deny()
        {
            switch (this.displayButton)
            {
                case MessageBoxButtons.OK:
                case MessageBoxButtons.OKCancel:
                    this.Result = MessageBoxResults.Cancel;
                    break;

                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.YesNo:
                    this.Result = MessageBoxResults.No;
                    break;

                default:
                    this.Result = MessageBoxResults.Cancel;
                    break;
            }

            this.Close();
        }

        private void Cancel()
        {
            this.Result = MessageBoxResults.Cancel;
            this.Close();
        }


        private void Close()
        {
            this.DialogResult = true;
        }

        private void InitializeButtonsVisibility(MessageBoxButtons displayButton)
        {
            // Affirmative button is always visible

            switch (displayButton)
            {
                case MessageBoxButtons.OK:
                    this.NegativeButtonVisibility = Visibility.Collapsed;
                    this.CancelButtonVisibility = Visibility.Collapsed;
                    break;
                case MessageBoxButtons.OKCancel:
                    this.NegativeButtonVisibility = Visibility.Collapsed;
                    this.CancelButtonVisibility = Visibility.Visible;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.NegativeButtonVisibility = Visibility.Visible;
                    this.CancelButtonVisibility = Visibility.Visible;
                    break;
                case MessageBoxButtons.YesNo:
                    this.NegativeButtonVisibility = Visibility.Visible;
                    this.CancelButtonVisibility = Visibility.Collapsed;
                    break;
            }
        }

        private void InitializeResult(MessageBoxButtons displayButton, MessageBoxResults? defaultResult)
        {
            if (defaultResult.HasValue)
            {
                this.Result = defaultResult.Value;
                return;
            }

            switch (displayButton)
            {
                case MessageBoxButtons.OK:
                    this.Result = MessageBoxResults.OK;
                    break;

                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.YesNoCancel:
                    this.Result = MessageBoxResults.Cancel;
                    break;

                case MessageBoxButtons.YesNo:
                    this.Result = MessageBoxResults.No;
                    break;

                default:
                    this.Result = MessageBoxResults.Cancel;
                    break;
            }
        }

        #endregion
    }
}

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using SharedLibraries.Retry;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using MessageBoxResult = WorkingTimeRecorder.wpf.Presentation.Core.Dialogs.MessageBoxResult;

namespace WorkingTimeRecorder.wpf.Presentation.Dialogs.MessageBox
{
    /// <summary>
    /// The view controller for the message dialog.
    /// </summary>
    internal sealed class MessageBoxViewModel : PopupDialogViewModelBase
    {
        #region Fields

        private readonly MessageBoxButtonType displayButton;
        private readonly MessageBoxResult? defaultResult;

        private bool? dialogResult;
        private Visibility cancelButtonVisibility;
        private MessageBoxImageType messageDialogImage;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxViewModel"/> class.
        /// </summary>
        /// <param name="displayButton">The value that specifies which button or buttons to display.</param>
        /// <param name="defaultResult">The value determines the button is focused by default.</param>
        public MessageBoxViewModel(MessageBoxButtonType displayButton, MessageBoxResult? defaultResult)
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
                    case MessageBoxResult.No:
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

                return this.defaultResult == MessageBoxResult.Cancel;
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
        public MessageBoxImageType MessageBoxImage
        {
            get => this.messageDialogImage;
            set => SetProperty(ref this.messageDialogImage, value);
        }

        /// <summary>
        /// Gets a message dialog result.
        /// </summary>
        public MessageBoxResult Result { get; private set; }

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
                case MessageBoxButtonType.OK:
                case MessageBoxButtonType.OKCancel:
                    this.Result = MessageBoxResult.OK;
                    break;

                case MessageBoxButtonType.YesNoCancel:
                case MessageBoxButtonType.YesNo:
                    this.Result = MessageBoxResult.Yes;
                    break;

                default:
                    this.Result = MessageBoxResult.OK;
                    break;
            }

            this.Close();
        }

        private void Deny()
        {
            switch (this.displayButton)
            {
                case MessageBoxButtonType.OK:
                case MessageBoxButtonType.OKCancel:
                    this.Result = MessageBoxResult.Cancel;
                    break;

                case MessageBoxButtonType.YesNoCancel:
                case MessageBoxButtonType.YesNo:
                    this.Result = MessageBoxResult.No;
                    break;

                default:
                    this.Result = MessageBoxResult.Cancel;
                    break;
            }

            this.Close();
        }

        private void Cancel()
        {
            this.Result = MessageBoxResult.Cancel;
            this.Close();
        }


        private void Close()
        {
            this.DialogResult = true;
        }

        private void InitializeButtonsVisibility(MessageBoxButtonType displayButton)
        {
            // Affirmative button is always visible

            switch (displayButton)
            {
                case MessageBoxButtonType.OK:
                    this.NegativeButtonVisibility = Visibility.Collapsed;
                    this.CancelButtonVisibility = Visibility.Collapsed;
                    break;
                case MessageBoxButtonType.OKCancel:
                    this.NegativeButtonVisibility = Visibility.Collapsed;
                    this.CancelButtonVisibility = Visibility.Visible;
                    break;
                case MessageBoxButtonType.YesNoCancel:
                    this.NegativeButtonVisibility = Visibility.Visible;
                    this.CancelButtonVisibility = Visibility.Visible;
                    break;
                case MessageBoxButtonType.YesNo:
                    this.NegativeButtonVisibility = Visibility.Visible;
                    this.CancelButtonVisibility = Visibility.Collapsed;
                    break;
            }
        }

        private void InitializeResult(MessageBoxButtonType displayButton, MessageBoxResult? defaultResult)
        {
            if (defaultResult.HasValue)
            {
                this.Result = defaultResult.Value;
                return;
            }

            switch (displayButton)
            {
                case MessageBoxButtonType.OK:
                    this.Result = MessageBoxResult.OK;
                    break;

                case MessageBoxButtonType.OKCancel:
                case MessageBoxButtonType.YesNoCancel:
                    this.Result = MessageBoxResult.Cancel;
                    break;

                case MessageBoxButtonType.YesNo:
                    this.Result = MessageBoxResult.No;
                    break;

                default:
                    this.Result = MessageBoxResult.Cancel;
                    break;
            }
        }

        #endregion
    }
}

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using WorkingTimeRecorder.Core.Extensions;
using WorkingTimeRecorder.Core.Languages;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

using MessageBoxResult = WorkingTimeRecorder.wpf.Presentation.Core.Dialogs.MessageBoxResult;

namespace WorkingTimeRecorder.wpf.Presentation.Dialogs.MessageBox
{
    /// <summary>
    /// Responsible for displaying message boxes to the user.
    /// </summary>
    internal sealed class MessageBox : IMessageBox
    {
        #region Fields

        private readonly ILanguageLocalizer languageLocalizer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBox"/> class.
        /// </summary>
        /// <param name="languageLocalizer">The language localizer.</param>
        public MessageBox(ILanguageLocalizer languageLocalizer)
        {
            this.languageLocalizer = languageLocalizer;
        }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public MessageBoxResult Show(string text)
        {
            return this.Show(text, Application.Current.MainWindow?.Title ?? CommonTextKeys.DefaultTitle);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(string text, string caption)
        {
            return this.Show(Application.Current.MainWindow, text, caption, MessageBoxButtonType.OK, MessageBoxImageType.None);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(string text, string caption, MessageBoxButtonType button, MessageBoxImageType image)
        {
            return this.Show(Application.Current.MainWindow, text, caption, button, image);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(
            string text,
            string caption,
            MessageBoxButtonType button,
            MessageBoxImageType image,
            MessageBoxResult? defaultResult)
        {
            return this.Show(Application.Current.MainWindow, text, caption, button, image, defaultResult);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(
            string text,
            string caption,
            MessageBoxButtonType button,
            MessageBoxImageType image,
            MessageBoxResult? defaultResult,
            MessageBoxSettings dialogSettings)
        {
            return this.Show(
                Application.Current.MainWindow,
                text,
                caption,
                button,
                image,
                defaultResult,
                dialogSettings);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window owner, string text)
        {
            return this.Show(owner, text, Application.Current.MainWindow?.Title ?? CommonTextKeys.DefaultTitle);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window owner, string text, string caption)
        {
            return this.Show(owner, text, caption, MessageBoxButtonType.OK, MessageBoxImageType.None);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButtonType button, MessageBoxImageType image)
        {
            return this.Show(owner, text, caption, button, image, MessageBoxResult.OK);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButtonType button, MessageBoxImageType image, MessageBoxResult? defaultResult)
        {
            return this.Show(owner, text, caption, button, image, defaultResult, CreateDefaultDialogSettings(button));
        }

        /// <inheritdoc />
        public MessageBoxResult Show(
            Window owner,
            string text,
            string caption,
            MessageBoxButtonType button,
            MessageBoxImageType image,
            MessageBoxResult? defaultResult,
            MessageBoxSettings dialogSettings)
        {
            if (dialogSettings is null)
            {
                throw new ArgumentNullException(nameof(dialogSettings));
            }

            if (owner is null)
            {
                return this.ShowMessageDialog(
                    Application.Current.MainWindow,
                    text,
                    caption,
                    button,
                    image,
                    defaultResult,
                    dialogSettings);
            }

            return this.ShowMessageDialog(owner, text, caption, button, image, defaultResult, dialogSettings);
        }

        #endregion

        #region Private methods

        private MessageBoxResult ShowMessageDialog(Window owner, string text, string caption, MessageBoxButtonType button, MessageBoxImageType image, MessageBoxResult? defaultResult, MessageBoxSettings dialogSettings)
        {
            var viewModel = new MessageBoxViewModel(button, defaultResult)
            {
                Title = caption,
                Text = text,
                MessageBoxImage = image,
                DialogIcon = dialogSettings.DialogIcon,
                AffirmativeButtonText = this.GetAffirmativeButtonText(dialogSettings.AffirmativeButtonText, button),
                NegativeButtonText = this.GetNegativeButtonText(dialogSettings.NegativeButtonText, button),
            };

            var dialogView = new MessageBoxView()
            {
                Owner = owner,
                DataContext = viewModel,
            };

            var beforeCursor = owner?.Cursor;
            try
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                return dialogView.ShowDialog() == true ? viewModel.Result : MessageBoxResult.Cancel;
            }
            finally
            {
                if (beforeCursor is not null && beforeCursor != Cursors.Arrow)
                {
                    Mouse.OverrideCursor = beforeCursor;
                }
            }
        }

        private MessageBoxSettings CreateDefaultDialogSettings(MessageBoxButtonType button)
        {
            return new MessageBoxSettings()
            {
                AffirmativeButtonText = this.GetAffirmativeButtonText(string.Empty, button),
                NegativeButtonText = this.GetNegativeButtonText(string.Empty, button),
                DialogIcon = GetDialogIcon(null),
            };
        }

        private string GetAffirmativeButtonText(string affirmativeText, MessageBoxButtonType button)
        {
            if (string.IsNullOrWhiteSpace(affirmativeText) == false)
            {
                return affirmativeText;
            }

            switch (button)
            {
                case MessageBoxButtonType.OK:
                case MessageBoxButtonType.OKCancel:
                    return this.languageLocalizer.LocalizeOk();

                case MessageBoxButtonType.YesNoCancel:
                case MessageBoxButtonType.YesNo:
                    return this.languageLocalizer.LocalizeYes();

                default:
                    return "OK";
            }
        }

        private string GetNegativeButtonText(string negativeText, MessageBoxButtonType button)
        {
            if (string.IsNullOrWhiteSpace(negativeText) == false)
            {
                return negativeText;
            }

            switch (button)
            {
                case MessageBoxButtonType.OKCancel:
                    return this.languageLocalizer.LocalizeCancel();

                case MessageBoxButtonType.YesNoCancel:
                case MessageBoxButtonType.YesNo:
                    return this.languageLocalizer.LocalizeNo();

                default:
                    return string.Empty;
            }
        }

        private static ImageSource GetDialogIcon(ImageSource? dialogIcon)
        {
            if (dialogIcon is null)
            {
                return Application.Current.MainWindow.Icon;
            }

            return dialogIcon;
        }

        #endregion
    }
}

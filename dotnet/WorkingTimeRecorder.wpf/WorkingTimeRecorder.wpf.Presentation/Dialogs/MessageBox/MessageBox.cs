﻿using System.Windows;
using System.Windows.Input;

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
            return this.Show(text, GetMainWindowTitle());
        }

        /// <inheritdoc />
        public MessageBoxResult Show(string text, string caption)
        {
            return this.Show(Application.Current?.MainWindow, text, caption, MessageBoxButtons.OK, MessageBoxImages.None);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(string text, string caption, MessageBoxButtons button, MessageBoxImages image)
        {
            return this.Show(Application.Current?.MainWindow, text, caption, button, image);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(
            string text,
            string caption,
            MessageBoxButtons button,
            MessageBoxImages image,
            MessageBoxResult? defaultResult)
        {
            return this.Show(Application.Current?.MainWindow, text, caption, button, image, defaultResult);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(
            string text,
            string caption,
            MessageBoxButtons button,
            MessageBoxImages image,
            MessageBoxResult? defaultResult,
            MessageBoxSettings dialogSettings)
        {
            return this.Show(
                Application.Current?.MainWindow,
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
            return this.Show(owner, text, GetMainWindowTitle());
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window owner, string text, string caption)
        {
            return this.Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxImages.None);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButtons button, MessageBoxImages image)
        {
            return this.Show(owner, text, caption, button, image, MessageBoxResult.OK);
        }

        /// <inheritdoc />
        public MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButtons button, MessageBoxImages image, MessageBoxResult? defaultResult)
        {
            return this.Show(owner, text, caption, button, image, defaultResult, CreateDefaultDialogSettings(button));
        }

        /// <inheritdoc />
        public MessageBoxResult Show(
            Window owner,
            string text,
            string caption,
            MessageBoxButtons button,
            MessageBoxImages image,
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
                    Application.Current?.MainWindow,
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

        private MessageBoxResult ShowMessageDialog(Window? owner, string text, string caption, MessageBoxButtons button, MessageBoxImages image, MessageBoxResult? defaultResult, MessageBoxSettings dialogSettings)
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

        private MessageBoxSettings CreateDefaultDialogSettings(MessageBoxButtons button)
        {
            return new MessageBoxSettings()
            {
                AffirmativeButtonText = this.GetAffirmativeButtonText(string.Empty, button),
                NegativeButtonText = this.GetNegativeButtonText(string.Empty, button),
                DialogIcon = Application.Current?.MainWindow?.Icon,
            };
        }

        private string GetAffirmativeButtonText(string affirmativeText, MessageBoxButtons button)
        {
            if (string.IsNullOrWhiteSpace(affirmativeText) == false)
            {
                return affirmativeText;
            }

            switch (button)
            {
                case MessageBoxButtons.OK:
                case MessageBoxButtons.OKCancel:
                    return this.languageLocalizer.LocalizeOk();

                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.YesNo:
                    return this.languageLocalizer.LocalizeYes();

                default:
                    return CommonTextKeys.DefaultOk;
            }
        }

        private string GetNegativeButtonText(string negativeText, MessageBoxButtons button)
        {
            if (string.IsNullOrWhiteSpace(negativeText) == false)
            {
                return negativeText;
            }

            switch (button)
            {
                case MessageBoxButtons.OKCancel:
                    return this.languageLocalizer.LocalizeCancel();

                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.YesNo:
                    return this.languageLocalizer.LocalizeNo();

                default:
                    return string.Empty;
            }
        }

        private static string GetMainWindowTitle(string defaultTitle = CommonTextKeys.DefaultTitle)
        {
            return Application.Current?.MainWindow?.Title ?? defaultTitle;
        }

        #endregion
    }
}
using System.Windows;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Dialogs
{
    /// <summary>
    /// Responsible for displaying message boxes to the user.
    /// </summary>
    public interface IMessageBox
    {
        #region Methods

        #region Synchronization

        /// <summary>
        /// Shows a simple OK message box with a message but no specified caption or icon.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The result (always OK).</returns>
        MessageBoxResult Show(string text);

        /// <summary>
        /// Shows a simple OK message box with a message and caption but no specified icon.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>The result (always OK).</returns>
        MessageBoxResult Show(string text, string caption);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button(s).</param>
        /// <param name="image">The image.</param>
        /// <returns>The result.</returns>
        MessageBoxResult Show(string text, string caption, MessageBoxButtonType button, MessageBoxImageType image);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button(s).</param>
        /// <param name="image">The image.</param>
        /// <param name="defaultResult">The <c>MessageBoxResult</c> value that specifies the default result of the message box.</param>
        /// <returns>The result.</returns>
        MessageBoxResult Show(string text, string caption, MessageBoxButtonType button, MessageBoxImageType image, MessageBoxResult? defaultResult);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button(s).</param>
        /// <param name="image">The image.</param>
        /// <param name="defaultResult">The <c>MessageBoxResult</c> value that specifies the default result of the message box.</param>
        /// <param name="dialogSettings">The dialog settings.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException"><c>dialogSettings</c> is <c>null</c>.</exception>
        MessageBoxResult Show(string text, string caption, MessageBoxButtonType button, MessageBoxImageType image, MessageBoxResult? defaultResult, MessageBoxSettings dialogSettings);

        /// <summary>
        /// Shows a message box belonging to the specified owner window.
        /// </summary>
        /// <param name="owner">Owner window</param>
        /// <param name="text">The text.</param>
        /// <returns>The result.</returns>
        MessageBoxResult Show(Window owner, string text);

        /// <summary>
        /// Shows a message box belonging to the specified owner window.
        /// </summary>
        /// <param name="owner">Owner window</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>The result.</returns>
        MessageBoxResult Show(Window owner, string text, string caption);

        /// <summary>
        /// Shows a message box belonging to the specified owner window.
        /// </summary>
        /// <param name="owner">Owner window</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button(s).</param>
        /// <param name="image">The image.</param>
        /// <returns>The result.</returns>
        MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButtonType button, MessageBoxImageType image);

        /// <summary>
        /// Shows a message box belonging to the specified owner window.
        /// </summary>
        /// <param name="owner">Owner window</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button(s).</param>
        /// <param name="image">The image.</param>
        /// <param name="defaultResult">The <c>MessageBoxResult</c> value that specifies the default result of the message box.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException"><c>dialogSettings</c> is <c>null</c>.</exception>
        MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButtonType button, MessageBoxImageType image, MessageBoxResult? defaultResult);

        /// <summary>
        /// Shows a message box belonging to the specified owner window.
        /// </summary>
        /// <param name="owner">Owner window</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button(s).</param>
        /// <param name="image">The image.</param>
        /// <param name="defaultResult">The <c>MessageBoxResult</c> value that specifies the default result of the message box.</param>
        /// <param name="dialogSettings">The dialog settings.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException"><c>dialogSettings</c> is <c>null</c>.</exception>
        MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButtonType button, MessageBoxImageType image, MessageBoxResult? defaultResult, MessageBoxSettings dialogSettings);

        #endregion

        #endregion
    }
}

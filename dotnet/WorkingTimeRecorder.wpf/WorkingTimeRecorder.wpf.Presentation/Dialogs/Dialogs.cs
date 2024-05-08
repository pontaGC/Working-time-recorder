using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation.Dialogs
{
    /// <inheritdoc />
    internal sealed class Dialogs : IDialogs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dialogs"/> class.
        /// </summary>
        /// <param name="wtrSystem">The WTR system.</param>
        public Dialogs(IWTRSystem wtrSystem)
        {
            this.MessageBox = new MessageBox.MessageBox(wtrSystem.LanguageLocalizer);
        }

        /// <summary>
        /// Gets a message box.
        /// </summary>
        public IMessageBox MessageBox { get; }
    }
}

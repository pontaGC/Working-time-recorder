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
        /// <param name="wtrSvc">The WTR service.</param>
        public Dialogs(IWTRSvc wtrSvc)
        {
            this.MessageBox = new MessageBox.MessageBox(wtrSvc.LanguageLocalizer);
        }

        /// <summary>
        /// Gets a message box.
        /// </summary>
        public IMessageBox MessageBox { get; }
    }
}

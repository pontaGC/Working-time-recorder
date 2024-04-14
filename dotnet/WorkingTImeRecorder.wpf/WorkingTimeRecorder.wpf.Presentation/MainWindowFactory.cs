using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkingTimeRecorder.wpf.Presentation.Core;
using WorkingTImeRecorder.wpf;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <inheritdoc />
    internal sealed class MainWindowFactory : IMainWindowFactory
    {
        /// <inheritdoc />
        public Window Create()
        {
            return new MainWindow();
        }
    }
}

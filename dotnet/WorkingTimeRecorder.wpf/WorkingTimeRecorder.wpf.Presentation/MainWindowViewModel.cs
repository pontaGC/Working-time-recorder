using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <summary>
    /// The view-model of the main window.
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogs dialogs;

        public MainWindowViewModel(IDialogs dialogs)
        {
            this.dialogs = dialogs;
            this.DummyCommand = new DelegateCommand(this.DummyAction);
        }

        public ICommand DummyCommand { get; }

        private void DummyAction()
        {
        }
    }
}

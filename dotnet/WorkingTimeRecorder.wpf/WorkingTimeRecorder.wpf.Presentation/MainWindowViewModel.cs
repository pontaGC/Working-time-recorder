using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;
using WorkingTimeRecorder.wpf.Presentation.Core.Menus;
using WorkingTimeRecorder.wpf.Presentation.MainMenuItems;
using WorkingTimeRecorder.wpf.Presentation.OutputWindows;
using WorkingTimeRecorder.wpf.Presentation.TaskViews;

namespace WorkingTimeRecorder.wpf.Presentation
{
    /// <summary>
    /// The view-model of the main window.
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly IWTRSystem wtrSystem;
        private readonly IDialogs dialogs;

        private OutputWindowViewModel outputWindow;

        private readonly TaskItem dummyItem;
        private readonly TaskItem dummyItem2;
        private readonly TaskItem dummyItem3;

        public MainWindowViewModel(IWTRSystem wtrSystem, IDialogs dialogs)
        {
            this.wtrSystem = wtrSystem;
            this.dialogs = dialogs;

            this.TaskList = new TaskViewModel();
            this.dummyItem = new TaskItem()
            {
                Name = "Dummy task",
            };
            this.dummyItem.ElapsedWorkTime.SetHours(1);
            this.dummyItem.ElapsedWorkTime.SetHours(20);
            var dummyItem = new TaskItemViewModel(this.dummyItem)
            {
                No = 1,
                IsRecordingTarget = true,
            };

            this.dummyItem2 = new TaskItem()
            {
                Name = "Dummy task2",
            };
            this.dummyItem.ElapsedWorkTime.SetHours(1);
            this.dummyItem.ElapsedWorkTime.SetHours(20);
            var dummyItem2vm = new TaskItemViewModel(this.dummyItem2)
            {
                No = 2,
            };

            this.dummyItem3 = new TaskItem()
            {
                Name = "Dummy task3",
            };
            this.dummyItem.ElapsedWorkTime.SetHours(1);
            this.dummyItem.ElapsedWorkTime.SetHours(20);
            var dummyItem3vm = new TaskItemViewModel(this.dummyItem3)
            {
                No = 3,
            };

            this.TaskList.TaskItems.Add(dummyItem);
            this.TaskList.TaskItems.Add(dummyItem2vm);
            this.TaskList.TaskItems.Add(dummyItem3vm);

            this.BuildMainMenuItems();

            this.DummyCommand = new DelegateCommand(this.DummyAction);
        }

        /// <summary>
        /// Gets a task list to record the working time.
        /// </summary>
        public TaskViewModel TaskList { get; }

        /// <summary>
        /// Gets or sets a view-model of the output window.
        /// </summary>
        public OutputWindowViewModel OutputWindow 
        {
            get => this.outputWindow;
            set => this.SetProperty(ref this.outputWindow, value);
        }

        /// <summary>
        /// Gets a collection of the main menu items.
        /// </summary>
        public ObservableCollection<MenuItemViewModelBase> MainMenuItems { get; } = new ObservableCollection<MenuItemViewModelBase>();

        public ICommand DummyCommand { get; }

        private void BuildMainMenuItems()
        {
            // Settings menu items
            var rootSettingItem = new EmptyCommandMenuItemViewModel()
            {
                Header = this.wtrSystem.LanguageLocalizer.Localize(WTRTextKeys.SettingsMenuItem, WTRTextKeys.DefaultSettingsMenuItem),
            };
            rootSettingItem.SubItems.Add(new LanguageSettingMenuItemViewModel(this.wtrSystem, this.dialogs));
            this.MainMenuItems.Add(rootSettingItem);
        }

        private int dummyCount;

        private void DummyAction()
        {
            this.dummyItem.ElapsedWorkTime.IncrementHours();
            this.dummyItem.ElapsedWorkTime.IncrementMinutes();

            var recordingTargets = this.TaskList.TaskItems.Where(x => x.IsRecordingTarget).ToArray();

            var logger = this.wtrSystem.LoggerCollection.Resolve(LogConstants.OutputWindow);
            logger.Log(SharedLibraries.Logging.Severity.Information, dummyCount.ToString());

            ++dummyCount;
        }
    }
}

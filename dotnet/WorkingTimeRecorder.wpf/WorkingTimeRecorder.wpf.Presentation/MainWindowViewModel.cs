using System.Collections.ObjectModel;
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
        private readonly IWTRSvc wtrSvc;
        private readonly IDialogs dialogs;

        private OutputWindowViewModel outputWindow;

        private readonly TaskCollection tasksModel = new TaskCollection();

        private readonly TaskItem dummyItem;
        private readonly TaskItem dummyItem2;
        private readonly TaskItem dummyItem3;

        public MainWindowViewModel(IWTRSvc wtrSvc, IDialogs dialogs)
        {
            this.wtrSvc = wtrSvc;
            this.dialogs = dialogs;

            this.dummyItem = new TaskItem()
            {
                Name = "Dummy task",
            };
            this.dummyItem.ElapsedWorkTime.SetHours(1);
            this.dummyItem.ElapsedWorkTime.SetHours(20);
            this.tasksModel.Items.Add(this.dummyItem);

            this.dummyItem2 = new TaskItem()
            {
                Name = "Dummy task2",
            };
            this.dummyItem2.ElapsedWorkTime.SetHours(1);
            this.dummyItem2.ElapsedWorkTime.SetHours(20);
            this.tasksModel.Items.Add(dummyItem2);

            this.dummyItem3 = new TaskItem()
            {
                Name = "Dummy task3",
            };
            this.dummyItem3.ElapsedWorkTime.SetHours(1);
            this.dummyItem3.ElapsedWorkTime.SetHours(20);
            this.tasksModel.Items.Add(dummyItem3);

            this.TaskList = new TaskListViewModel(this.tasksModel, wtrSvc, dialogs);

            this.BuildMainMenuItems(new MainMenuItemBuilder(this.wtrSvc, this.dialogs));

            this.DummyCommand = new DelegateCommand(this.DummyAction);
        }

        /// <summary>
        /// Gets a task list to record the working time.
        /// </summary>
        public TaskListViewModel TaskList { get; }

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

        private void BuildMainMenuItems(IMenuItemBuilder menuItemBuilder)
        {
            var menuItems = menuItemBuilder.Build();
            this.MainMenuItems.AddRange(menuItems);
        }

        private int dummyCount;

        private void DummyAction()
        {
            this.dummyItem.ElapsedWorkTime.IncrementHours();
            this.dummyItem.ElapsedWorkTime.IncrementMinutes();

            var recordingTargets = this.TaskList.ItemViewModels.Where(x => x.IsRecordingTarget).ToArray();

            var logger = this.wtrSvc.LoggerCollection.Resolve(LogConstants.OutputWindow);
            logger.Log(SharedLibraries.Logging.Severity.Information, dummyCount.ToString());

            ++dummyCount;
        }
    }
}

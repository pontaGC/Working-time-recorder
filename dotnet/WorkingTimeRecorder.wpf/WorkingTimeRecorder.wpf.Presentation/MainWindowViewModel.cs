using System.Collections.ObjectModel;
using System.Windows.Input;

using Prism.Commands;

using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.Core.Persistences;
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
        private readonly IEntityRepository<TaskCollection> taskCollectionRepository;

        private OutputWindowViewModel outputWindow;

        private readonly TaskCollection tasksModel;

        public MainWindowViewModel(IWTRSvc wtrSvc, IDialogs dialogs, IEntityRepository<TaskCollection> taskCollectionRepository)
        {
            this.wtrSvc = wtrSvc;
            this.dialogs = dialogs;
            this.taskCollectionRepository = taskCollectionRepository;

            this.tasksModel = taskCollectionRepository.GetAll().FirstOrDefault();
            if (this.tasksModel is null)
            {
                this.tasksModel = new TaskCollection();
                this.taskCollectionRepository.Add(this.tasksModel);
            }

            this.TaskList = new TaskListViewModel(this.tasksModel, wtrSvc, dialogs);

            this.BuildMainMenuItems(new MainMenuItemBuilder(this.wtrSvc, this.dialogs));
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

        private void BuildMainMenuItems(IMenuItemBuilder menuItemBuilder)
        {
            var menuItems = menuItemBuilder.Build();
            this.MainMenuItems.AddRange(menuItems);
        }
    }
}

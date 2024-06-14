﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

using Prism.Commands;

using SharedLibraries.Extensions;
using SharedLibraries.Logging;

using WorkingTimeRecorder.Core.Models.Tasks;
using WorkingTimeRecorder.Core.Mvvm;
using WorkingTimeRecorder.Core.Shared;
using WorkingTimeRecorder.wpf.Presentation.Core.Dialogs;

namespace WorkingTimeRecorder.wpf.Presentation.TaskViews
{
    /// <summary>
    /// The view-model for task view.
    /// </summary>
    internal class TaskViewModel : ViewModelBase
    {
        #region Fields

        private readonly IWTRSvc wtrSvc;
        private readonly IDialogs dialogs;

        private readonly Tasks tasksModel;
        private readonly DelegateCommand addItemCommand;
        private readonly DelegateCommand<Window> deleteItemCommand;

        private TaskItemViewModel selectedItem;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="wtrSvc">The wtr service.</param>
        /// <param name="dialogs">The dialogs.</param>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> or <paramref name="wtrSvc"/> is <c>null</c>.</exception>
        public TaskViewModel(Tasks model, IWTRSvc wtrSvc, IDialogs dialogs)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(wtrSvc);
            ArgumentNullException.ThrowIfNull(dialogs);

            this.tasksModel = model;
            this.wtrSvc = wtrSvc;
            this.dialogs = dialogs;

            this.ItemViewModels.AddRange(model.Items.Select(CreateTaskItemViewModel));
            this.ItemViewModels.CollectionChanged += this.OnTaskItemViewModelsChanged;

            this.addItemCommand = new DelegateCommand(this.AddItem);
            this.deleteItemCommand = new DelegateCommand<Window>(this.DeleteItem, param => this.CanDeleteItem());

            this.RegisterCommandBindings();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a collection of the command binding.
        /// </summary>
        public CommandBindingCollection CommandBindings { get; } = new CommandBindingCollection();

        /// <summary>
        /// Gets a selected task item.
        /// </summary>
        public TaskItemViewModel SelectedItem 
        {
            get => this.selectedItem;
            set
            {
                if (this.SetProperty(ref this.selectedItem, value))
                {
                    this.deleteItemCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Gets a collection of the task item.
        /// </summary>
        public ObservableCollection<TaskItemViewModel> ItemViewModels { get; } = new ObservableCollection<TaskItemViewModel>();

        /// <summary>
        /// Gets a command to add a new task item.
        /// </summary>
        public ICommand AddItemCommand => this.addItemCommand;

        /// <summary>
        /// Gets a command to delete the selected task item.
        /// </summary>
        public ICommand DeleteItemCommand => this.deleteItemCommand;

        #endregion

        #region Private Methods

        private void AddItem()
        {
            var itemModel = new TaskItem();
            this.tasksModel.Items.Add(itemModel);

            var itemViewModel = CreateTaskItemViewModel(itemModel);
            this.ItemViewModels.Add(itemViewModel);

            this.SelectedItem = itemViewModel;

            this.NotifyAddItem(itemViewModel);
        }

        private void NotifyAddItem(TaskItemViewModel itemViewModel)
        {
            const string MessageFormatKey = "WorkingTimeRecorder.Task.NotifyAddNewItem";
            var messageFormat = this.wtrSvc.LanguageLocalizer.Localize(MessageFormatKey, MessageFormatKey);
            var message = string.Format(CultureInfo.InvariantCulture, messageFormat, itemViewModel.No);

            var outputWindow = this.wtrSvc.LoggerCollection.Resolve(LogConstants.OutputWindow);
            outputWindow.Log(Severity.Information, message);
        }

        private bool CanDeleteItem()
        {
            if (this.ItemViewModels.IsEmpty())
            {
                return false;
            }

            if (this.SelectedItem is null)
            {
                return false;
            }

            return true;
        }

        private void DeleteItem(Window ownerWindow)
        {
            if (!this.ConfirmDeleteItem(ownerWindow))
            {
                // User denied
                return;
            }

            var selectedItemId = this.SelectedItem.Id;
            var deletingItemModel = this.tasksModel.Items.SingleOrDefaultSafe(x => x.Id == selectedItemId);
            if (deletingItemModel is null)
            {
                return;
            }

            if (this.tasksModel.Items.Remove(deletingItemModel))
            {
                var deletingItemViewModel = this.SelectedItem;
                this.DeleteSelecteItemViewModel(deletingItemViewModel);
                this.NotifyDeletedItem(deletingItemViewModel);
            }
        }

        private bool ConfirmDeleteItem(Window ownerWindow)
        {
            var message = this.wtrSvc.LanguageLocalizer.Localize(
                "WorkingTimeRecorder.Task.ConfirmDeleteItem",
                "Deleted items cannot be recovered.\nAre you sure you want to delete the selected item?");
            var messageBoxResult = this.dialogs.MessageBox.Show(
                ownerWindow,
                message,
                ownerWindow.Title,
                MessageBoxButtons.YesNo,
                MessageBoxImages.Question);
            return messageBoxResult == MessageBoxResults.Yes;
        }

        private void DeleteSelecteItemViewModel(TaskItemViewModel deletingItemViewModel)
        {
            this.ItemViewModels.Remove(deletingItemViewModel);
            if (deletingItemViewModel.IsRecordingTarget)
            {
                // Move recording target
                var firstItemVM = this.ItemViewModels.FirstOrDefault();
                if (firstItemVM is not null)
                {
                    firstItemVM.IsRecordingTarget = true;
                }
            }
        }

        private void NotifyDeletedItem(TaskItemViewModel deletedItem)
        {
            const string MessageFormatKey = "WorkingTimeRecorder.Task.NotifyDeleteItem";
            var messageFormat = this.wtrSvc.LanguageLocalizer.Localize(MessageFormatKey, MessageFormatKey);
            var message = string.Format(
                CultureInfo.InvariantCulture,
                messageFormat,
                deletedItem.No,
                deletedItem.Name,
                deletedItem.ManHours.ToString(ManHoursConstants.StringFormat));

            var outputWindow = this.wtrSvc.LoggerCollection.Resolve(LogConstants.OutputWindow);
            outputWindow.Log(Severity.Information, message);
        }

        private TaskItemViewModel CreateTaskItemViewModel(TaskItem itemModel)
        {
            const int StartTaskNo = 1;

            var itemNo = this.ItemViewModels.Any() ? this.ItemViewModels.Max(item => item.No) + 1 : StartTaskNo;
            return new TaskItemViewModel(this.wtrSvc, itemModel)
            {
                No = itemNo,
                IsRecordingTarget = itemNo == StartTaskNo,
            };
        }
        
        private void OnTaskItemViewModelsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.deleteItemCommand.RaiseCanExecuteChanged();
        }

        private void RegisterCommandBindings()
        {
            var deleteCommandBindinds = new CommandBinding(
                ApplicationCommands.Delete,
                (sender, args) => this.deleteItemCommand.Execute(Window.GetWindow(sender as DependencyObject)),
                (sender, args) => args.CanExecute = this.deleteItemCommand.CanExecute(Window.GetWindow(sender as DependencyObject)));
            this.CommandBindings.Add(deleteCommandBindinds);
        }

        #endregion
    }
}

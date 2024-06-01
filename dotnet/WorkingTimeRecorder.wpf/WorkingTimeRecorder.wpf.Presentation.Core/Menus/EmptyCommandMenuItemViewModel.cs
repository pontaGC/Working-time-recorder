using System.Collections.Specialized;
using System.Windows.Input;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Menus
{
    /// <summary>
    /// The empty command menu item view-model.
    /// The menu item can have sub-menu items.
    /// </summary>
    public sealed class EmptyCommandMenuItemViewModel : MenuItemViewModelBase
    {
        private readonly EmptyCommand emptyCommand;

        public EmptyCommandMenuItemViewModel()
        {
            this.emptyCommand = new EmptyCommand()
            {
                HasSubMenuItems = base.SubItems.Where(item => !item.IsSeparator).Any()
            };
            this.SubItems.CollectionChanged += this.OnSubItemsChanged;
        }

        /// <inheritdoc />
        public override ICommand Command => this.emptyCommand;

        private void OnSubItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.emptyCommand.HasSubMenuItems = this.SubItems.Where(item => !item.IsSeparator).Any();
            this.emptyCommand.RaiseCanExecuteChanged();
        }

        private sealed class EmptyCommand : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            internal bool HasSubMenuItems { get; set; }

            bool ICommand.CanExecute(object? parameter)
            {
                return this.HasSubMenuItems;
            }

            void ICommand.Execute(object? parameter)
            {
            }

            internal void RaiseCanExecuteChanged()
            {
                this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

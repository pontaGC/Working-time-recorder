using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WorkingTimeRecorder.Core.Mvvm;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Menus
{
    /// <summary>
    /// The base view-model of the menu item.
    /// </summary>
    /// <remarks>It is recommended to have <see cref="IMenuItemBuilder"/> generate an instance of the imeplementation class.</remarks>
    public abstract class MenuItemViewModelBase : ViewModelBase
    {
        private bool isEnabled = true;
        private string header = string.Empty;
        private Image? icon;
        private Visibility visibility = Visibility.Visible;

        /// <summary>
        /// Gets the value indicating that the separator.
        /// </summary>
        public bool IsSeparator { get; }

        /// <summary>
        /// Gets the item that labels the menu item.
        /// </summary>
        public string Header
        {
            get => this.header;
            set => this.SetProperty(ref this.header, value);
        }

        /// <summary>
        /// Gets the icon of the menu item.
        /// </summary>
        public Image Icon
        {
            get => this.icon;
            set => this.SetProperty(ref this.icon, value);
        }

        /// <summary>
        /// Gets the value indicating the menu item is enable.
        /// </summary>
        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetProperty(ref this.isEnabled, value);
        }

        /// <summary>
        /// Gets the visible state.
        /// </summary>
        public Visibility Visibility
        {
            get => this.visibility;
            set => this.SetProperty(ref this.visibility, value);
        }

        /// <summary>
        /// Gets a command of this menu item.
        /// </summary>
        public abstract ICommand Command { get; }

        /// <summary>
        /// Gets the sub menu items.
        /// </summary>
        public ObservableCollection<MenuItemViewModelBase> SubItems { get; } = new ObservableCollection<MenuItemViewModelBase>();
    }
}

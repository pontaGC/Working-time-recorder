using System.Diagnostics.CodeAnalysis;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Menus
{
    /// <summary>
    /// Responsible for creating the menu items.
    /// </summary>
    public interface IMenuItemBuilder
    {
        /// <summary>
        /// Creates a collection of the menu item.
        /// </summary>
        /// <returns>The collection of the menu item.</returns>
        [return: NotNull]
        IEnumerable<MenuItemViewModelBase> Build();
    }
}

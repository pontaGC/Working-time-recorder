using System.Windows;
using System.Windows.Input;

namespace WorkingTimeRecorder.wpf.Presentation.Core.Controls
{
    /// <summary>
    /// Interaction logic <see cref="OkCancelButtons"/>.xaml.
    /// </summary>
    public partial class OkCancelButtons
    {
        #region Dependency Propeties

        public static readonly DependencyProperty IsOkButtonDefaultProperty
            = DependencyProperty.Register(
                name: nameof(IsOkButtonDefault),
                propertyType: typeof(bool),
                ownerType: typeof(OkCancelButtons),
                typeMetadata: new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty OkCommandProperty
            = DependencyProperty.Register(
                name: nameof(OkCommand),
                propertyType: typeof(ICommand),
                ownerType: typeof(OkCancelButtons),
                typeMetadata: new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty OkCommandParameterProperty
            = DependencyProperty.Register(
                name: nameof(OkCommandParameter),
                propertyType: typeof(object),
                ownerType: typeof(OkCancelButtons),
                typeMetadata: new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CancelCommandProperty
            = DependencyProperty.Register(
                name: nameof(CancelCommand),
                propertyType: typeof(ICommand),
                ownerType: typeof(OkCancelButtons),
                typeMetadata: new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CancelCommandParameterProperty
            = DependencyProperty.Register(
                name: nameof(CancelCommandParameter),
                propertyType: typeof(object),
                ownerType: typeof(OkCancelButtons),
                typeMetadata: new FrameworkPropertyMetadata(null));

        #endregion

        #region Constructors

        public OkCancelButtons()
        {
            this.InitializeComponent();

            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        #endregion

        #region Propeties

        /// <summary>
        /// Gets or sets a value indicating whether the OK button is default button or not.
        /// The default value is <c>true</c>.
        /// </summary>
        public bool IsOkButtonDefault
        {
            get => (bool)this.GetValue(IsOkButtonDefaultProperty);
            set => this.SetValue(IsOkButtonDefaultProperty, value);
        }

        /// <summary>
        /// Gets or sets OK button command.
        /// </summary>
        public ICommand OkCommand
        {
            get => (ICommand)this.GetValue(OkCommandProperty);
            set => this.SetValue(OkCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter of OK button command.
        /// The default value is owner window.
        /// </summary>
        public object OkCommandParameter
        {
            get => this.GetValue(OkCommandParameterProperty);
            set => this.SetValue(OkCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets cancel button command.
        /// </summary>
        public ICommand CancelCommand
        {
            get => (ICommand)this.GetValue(CancelCommandProperty);
            set => this.SetValue(CancelCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter of Cancel button command.
        /// The default value is owner window.
        /// </summary>
        public object CancelCommandParameter
        {
            get => this.GetValue(CancelCommandParameterProperty);
            set => this.SetValue(CancelCommandParameterProperty, value);
        }

        #endregion

        #region Private Methods

        private void OnLoaded(object? obj, RoutedEventArgs args)
        {
            var ancestorWindow = Window.GetWindow(this);

            //  Since this loading is completed after custom data binding is completed,
            //  NULL check is executed not to unbind custom data binding 
            if (this.OkCommandParameter is null)
            {
                this.OkCommandParameter = ancestorWindow;
            }

            if (this.CancelCommandParameter is null)
            {
                this.CancelCommandParameter = ancestorWindow;
            }
        }

        private void OnUnloaded(object? obj, RoutedEventArgs args)
        {
            this.OkCommandParameter = null;
            this.CancelCommandParameter = null;
        }

        #endregion
    }
}

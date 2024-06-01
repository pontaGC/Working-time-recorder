using SharedLibraries.Logging;
using WorkingTimeRecorder.wpf.Presentation.Core.OutputWindows;

namespace WorkingTimeRecorder.wpf.Presentation.OutputWindows
{
    /// <summary>
    /// Responsible for controlling output window.
    /// </summary>
    internal sealed class OutputWindowController : IOutputWindowController, IOutputWindowRegistrar
    {
        #region Fields

        private OutputWindow view;
        private OutputWindowViewModel viewModel;

        private readonly Queue<OutputWindowMessage> messageCache = new Queue<OutputWindowMessage>();
        private readonly SynchronizationContext? synchronizationContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindowController"/> class.
        /// </summary>
        public OutputWindowController()
        {
            this.synchronizationContext = SynchronizationContext.Current;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public void Register(OutputWindow view, OutputWindowViewModel viewModel)
        {
            // This method is called first
            this.view = view;
            this.viewModel = viewModel;

            while (this.messageCache.Any())
            {
                this.viewModel.Messages.Add(this.messageCache.Dequeue());
            }
        }

        /// <inheritdoc />
        public void ShowWindow()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool CloseWindow()
        {
            throw new NotFiniteNumberException();
        }

        /// <inheritdoc />
        public void AppendMessage(Severity severity, string message)
        {
            if (this.viewModel is null)
            {
                this.messageCache.Enqueue(new OutputWindowMessage() { Severity = severity, Text = message, });
                return;
            }

            this.ExecuteUIOperation(
                () =>
                {
                    while (this.messageCache.Any())
                    {
                        this.viewModel.Messages.Add(this.messageCache.Dequeue());
                    }

                    this.viewModel.Messages.Add(new OutputWindowMessage() { Severity = severity, Text = message, });
                });
        }

        /// <inheritdoc />
        public void AppendMessage(string message)
        {
            this.AppendMessage(Severity.None, message);
        }

        /// <inheritdoc />
        public void Clear()
        {
            if (this.viewModel is null)
            {
                return;
            }

            this.ExecuteUIOperation(this.viewModel.Messages.Clear);
        }

        #endregion

        #region Private Methods

        private void ExecuteUIOperation(Action uiOperation)
        {
            if (this.synchronizationContext is not null && this.synchronizationContext != SynchronizationContext.Current)
            {
                this.synchronizationContext.Post(d: (o) => uiOperation(), state: null);
            }
            else
            {
                uiOperation();
            }
        }

        #endregion
    }
}

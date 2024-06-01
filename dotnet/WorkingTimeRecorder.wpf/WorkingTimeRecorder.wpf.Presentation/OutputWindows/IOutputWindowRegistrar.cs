namespace WorkingTimeRecorder.wpf.Presentation.OutputWindows
{
    /// <summary>
    /// The output window registrar.
    /// </summary>
    internal interface IOutputWindowRegistrar
    {
        /// <summary>
        /// Registers the instances of the output window and view-model of it.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view-model.</param>
        void Register(OutputWindow view, OutputWindowViewModel viewModel);
    }
}

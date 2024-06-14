using WorkingTimeRecorder.Core;
using WorkingTimeRecorder.wpf.Presentation.Core.Icons;
using WorkingTimeRecorder.wpf.Presentation.Core.Markup;

namespace WorkingTimeRecorder.wpf.Presentation
{
    internal class ModuleInitializer : IModuleInitializer
    {
        private readonly IIconService iconService;

        public ModuleInitializer(IIconService iconService)
        {
            this.iconService = iconService;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            IconExtension.Register(this.iconService);
        }
    }
}

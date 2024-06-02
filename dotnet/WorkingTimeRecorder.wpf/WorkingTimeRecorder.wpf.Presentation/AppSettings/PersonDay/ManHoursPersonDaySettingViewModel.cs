using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTimeRecorder.Core.Mvvm;

namespace WorkingTimeRecorder.wpf.Presentation.AppSettings.PersonDay
{
    /// <summary>
    /// The view-model of the man-hours per person day setting.
    /// </summary>
    internal sealed class ManHoursPersonDaySettingViewModel : ViewModelBase
    {
        #region Fields

        private bool? dialogResult = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManHoursPersonDaySettingViewModel"/> class.
        /// </summary>
        public ManHoursPersonDaySettingViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating the dialog result of the window.
        /// </summary>
        public bool? DialogResult
        {
            get => this.dialogResult;
            set => this.SetProperty(ref this.dialogResult, value);
        }

        #endregion
    }
}

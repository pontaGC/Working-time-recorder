namespace WorkingTimeRecorder.Core.Models.Tasks
{
    /// <summary>
    /// The readable elapsed work time.
    /// </summary>
    public interface IReadOnlyElapsedWorkTIme
    {
        #region Properties

        /// <summary>
        /// Gets a task ID.
        /// </summary>
        string TaskId { get; }

        /// <summary>
        /// Gets the hours part of the elapsed work time.
        /// </summary>
        uint Hours { get; }

        /// <summary>
        /// Gets the minutes part of the elapsed work time.
        /// </summary>
        uint Miniutes { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a current time in hours. (<c>Hours</c>: 1, <c>Miniutes</c>: 30 -> 1.5)
        /// </summary>
        /// <returns>The current time in hours</returns>
        public double GetTimeInHours()
        {
            const double OneHourMiniutes = 60;
            return this.Hours + this.Miniutes / OneHourMiniutes;
        }

        /// <summary>
        /// Converts a current time to man-hours.
        /// </summary>
        /// <param name="manHoursPerPersonDay">The man-hours per person day.</param>
        /// <returns>
        /// Returns <c>double.NaN</c> if <paramref name="manHoursPerPersonDay"/> is less than or equal to 0,
        /// otherwise, reutrns man-hours.
        /// </returns>
        public double ConvertToManHours(double manHoursPerPersonDay)
        {
            if (manHoursPerPersonDay <= 0)
            {
                return double.NaN;
            }

            return this.GetTimeInHours() / manHoursPerPersonDay;
        }

        #endregion
    }
}

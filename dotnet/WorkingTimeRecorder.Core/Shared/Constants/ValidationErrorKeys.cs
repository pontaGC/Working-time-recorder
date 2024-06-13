namespace WorkingTimeRecorder.Core.Shared
{
    /// <summary>
    /// The validation error message keys.
    /// </summary>
    public static class ValidationErrorKeys
    {
        public const string TextFieldRequired = "WorkingTimeRecorder.Validation.Error.TextFieldRequired";
        public const string DefaultTextFieldRequired = "The Text field is required.";
        public const string DoubleParse = "WorkingTimeRecorder.Validation.Error.DoubleParse";
        public const string DefaultDoubleParse = "Please enter a number.";
        public const string OverRange = "WorkingTimeRecorder.Validation.Error.OverRange";
        public const string DefaultOverRange = "The configurable range is {0} ~ {1}.";
        public const string OverStringLength = "WorkingTimeRecorder.Validation.Error.OverStringLength";
        public const string DefaultOverStringLength = "Please enter the number of characters between {0} and {1}.";
    }
}

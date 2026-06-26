namespace HolidayAssessment.Common
{
    public class InputValidationResult
    {
        public bool IsValid { get; init; }
        public string? ErrorMessage { get; init; }

        #region Factory
        public static InputValidationResult Success() => new() { IsValid = true };
        public static InputValidationResult Fail(string errorMessage) => new() { IsValid = false, ErrorMessage =  errorMessage};
        #endregion
    }
}

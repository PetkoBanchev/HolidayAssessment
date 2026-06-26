using HolidayAssessment.Common;
using HolidayAssessment.Constants;

namespace HolidayAssessment.Validators
{
    public static class YearValidator
    {
        /// <summary>
        /// Returns true if the year is within the supported range
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static InputValidationResult Validate(int year)
        {
            if (year < NagerConstraints.MinYear || year > NagerConstraints.MaxYear)
                return InputValidationResult.Fail($"Year must be between {NagerConstraints.MinYear} and {NagerConstraints.MaxYear}.");

            return InputValidationResult.Success();
        }
    }
}

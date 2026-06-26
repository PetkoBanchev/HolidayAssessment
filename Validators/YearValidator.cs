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
        public static bool Validate(int year)
        {
            return year > NagerConstraints.MinYear && year < NagerConstraints.MaxYear;
        }
    }
}

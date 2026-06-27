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
        public static void Validate(int year)
        {
            if (year < NagerConstraints.MinYear || year > NagerConstraints.MaxYear)
                throw new ArgumentOutOfRangeException($"Year must be between {NagerConstraints.MinYear} and {NagerConstraints.MaxYear}.");
        }
    }
}

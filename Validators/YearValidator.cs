using HolidayAssessment.Constants;

namespace HolidayAssessment.Validators
{
    public static class YearValidator
    {
        public static bool Validate(int year)
        {
            return year < NagerConstraints.MinYear || year > NagerConstraints.MaxYear;
        }
    }
}

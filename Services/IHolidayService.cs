namespace HolidayAssessment.Services
{
    public interface IHolidayService
    {
        Task ImportHolidaysAsync(int year, List<string> countryCodes);
    }
}

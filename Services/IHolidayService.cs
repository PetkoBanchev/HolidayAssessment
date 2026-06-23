using HolidayAssessment.DTOs;

namespace HolidayAssessment.Services
{
    public interface IHolidayService
    {
        Task ImportHolidaysAsync(int year, List<string> countryCodes);

        Task<List<HolidayResponseDto>> GetLastThreeHolidaysAsync(string countryCode);

        Task<List<WeekdayHolidayDto>> GetHolidaysOnWeekdaysAsync(int year, List<String> countryCodes);

        Task<List<CountryHolidayCountDto>> GetNumberOfHolidaysNotOnWeekendsAsync(int year, List<String> countryCodes);

        Task<List<SharedHolidayDto>> GetNumberOfSharedHolidaysAsync(int year, List<string> countryCodes);
    }
}

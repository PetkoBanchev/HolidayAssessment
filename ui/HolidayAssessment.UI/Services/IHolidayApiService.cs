using HolidayAssessment.UI.DTOs;

namespace HolidayAssessment.UI.Services
{
    public interface IHolidayApiService
    {
        Task<List<HolidayResponseDto>> GetLastThreeAsync(string countryCode);

        Task<List<WeekdayHolidayDto>> GetWeekdayHolidaysAsync(int year, List<string> countries);

        Task<List<CountryHolidayCountDto>> GetWeekdayHolidaysCountAsync(int year, List<string> countries);

        Task<List<SharedHolidayDto>> GetSharedHolidaysAsync(int year, string countryA, string countryB);

        Task ImportHolidaysAsync(int year, List<string> countries);
    }
}

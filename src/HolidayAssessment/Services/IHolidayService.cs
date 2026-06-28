using HolidayAssessment.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HolidayAssessment.Services
{
    public interface IHolidayService
    {
        Task ImportHolidaysAsync(int year, List<string> countryCodes);

        Task<List<HolidayResponseDto>> GetLastThreeHolidaysAsync(string countryCode);

        Task<List<WeekdayHolidayDto>> GetWeekdayHolidaysAsync(int year, List<String> countryCodes);

        Task<List<CountryHolidayCountDto>> GetWeekdayHolidaysCountAsync(int year, List<String> countryCodes);

        Task<List<SharedHolidayDto>> GetNumberOfSharedHolidaysAsync(int year, string countryA, string countryB);
    }
}

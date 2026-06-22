using HolidayAssessment.DTOs;

namespace HolidayAssessment.Services
{
    public interface IHolidayService
    {
        Task ImportHolidaysAsync(int year, List<string> countryCodes);

        Task<List<HolidayResponseDto>> GetLastThreeHolidaysAsync(string countryCode);
    }
}

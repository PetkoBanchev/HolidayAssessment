using HolidayAssessment.DTOs;

namespace HolidayAssessment.Clients
{
    public interface INagerApiClient
    {
        Task<List<HolidayApiDto>> GetPublicHolidaysAsync(int year, string countryCode);
    }
}

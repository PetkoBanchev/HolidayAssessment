using HolidayAssessment.DTOs.ApiDTOs;

namespace HolidayAssessment.Clients
{
    public interface INagerApiClient
    {
        Task<List<HolidayApiDto>> GetPublicHolidaysAsync(int year, string countryCode);
        Task<List<CountryApiDto>> GetAvailableCountriesAsync();
    }
}

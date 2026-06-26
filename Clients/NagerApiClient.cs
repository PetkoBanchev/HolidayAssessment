using HolidayAssessment.DTOs.ApiDTOs;

namespace HolidayAssessment.Clients
{
    public class NagerApiClient: INagerApiClient
    {
        private readonly HttpClient _httpClient;

        public NagerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HolidayApiDto>> GetPublicHolidaysAsync(int year, string countryCode)
        {
            var url = $"PublicHolidays/{year}/{countryCode}";

            var result = await _httpClient.GetFromJsonAsync<List<HolidayApiDto>>(url);

            return result ?? new List<HolidayApiDto>();
        }

        public async Task<List<CountryApiDto>> GetAvailableCountriesAsync()
        {
            var url = $"AvailableCountries";

            var result = await _httpClient.GetFromJsonAsync<List<CountryApiDto>>(url);

            return result ?? new List<CountryApiDto>();
        }
    }
}

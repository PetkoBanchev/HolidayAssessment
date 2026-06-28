using HolidayAssessment.DTOs.RequestDTOs;
using HolidayAssessment.UI.DTOs;
using System.Net.Http.Json;
using System.Text.Json;
namespace HolidayAssessment.UI.Services
{
    public class HolidayApiService: IHolidayApiService
    {
        private readonly HttpClient _http;

        public HolidayApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public async Task<List<HolidayResponseDto>> GetLastThreeAsync(string countryCode)
        {
            return await _http.GetFromJsonAsync<List<HolidayResponseDto>>(
                $"api/holidays/{countryCode}/last-three")
                ?? new();
        }
        
        public async Task<List<WeekdayHolidayDto>> GetWeekdayHolidaysAsync(int year, List<string> countries)
        {
            var request = new HolidayQueryRequestDto{Year = year,CountryCodes = countries};
            var response = await _http.PostAsJsonAsync("api/holidays/weekday-holidays", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }

            return await response.Content.ReadFromJsonAsync<List<WeekdayHolidayDto>>() ?? new();
        }
        public async Task<List<CountryHolidayCountDto>> GetWeekdayHolidaysCountAsync(int year, List<string> countries)
        {
            var request = new HolidayQueryRequestDto { Year = year, CountryCodes = countries };
            var response = await _http.PostAsJsonAsync("api/holidays/weekday-holidays-count", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }

            return await response.Content.ReadFromJsonAsync<List<CountryHolidayCountDto>>() ?? new();
        }

        public async Task<List<SharedHolidayDto>> GetSharedHolidaysAsync(int year, string countryA, string countryB)
        {
            throw new NotImplementedException();
        }



        public async Task ImportHolidaysAsync(int year, List<string> countryCodes)
        {
            var request = new ImportHolidayRequestDto
            {
                Year = year,
                CountryCodes = countryCodes
            };

            var response = await _http.PostAsJsonAsync("api/holidays/import", request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }
    }
}

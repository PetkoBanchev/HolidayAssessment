using HolidayAssessment.UI.DTOs;
using System.Net.Http.Json;
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
            //return await _http.GetFromJsonAsync<List<HolidayResponseDto>>(
            //    $"api/holidays/{countryCode}/last-three")
            //    ?? new();
            throw new NotImplementedException();
        }

        public async Task<List<SharedHolidayDto>> GetSharedHolidaysAsync(int year, string countryA, string countryB)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CountryHolidayCountDto>> GetWeekdayHolidayCountsAsync(int year, List<string> countries)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WeekdayHolidayDto>> GetWeekdayHolidaysAsync(int year, List<string> countries)
        {
            throw new NotImplementedException();
        }

        public async Task ImportHolidaysAsync(int year, List<string> countries)
        {
            throw new NotImplementedException();
        }
    }
}

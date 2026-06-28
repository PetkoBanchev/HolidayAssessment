
using static System.Net.WebRequestMethods;

namespace HolidayAssessment.UI.Services
{
    public class CountryApiService : ICountryApiService
    {
        private readonly HttpClient _http;

        public CountryApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }
        public async Task<List<string>> GetCountryCodesAsync()
        {
            return await _http.GetFromJsonAsync<List<string>>(
                $"api/countries/get-countries")
                ?? new();
        }
    }
}

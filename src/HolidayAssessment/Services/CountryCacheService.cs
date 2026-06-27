using HolidayAssessment.Clients;
using Microsoft.Extensions.Caching.Memory;

namespace HolidayAssessment.Services
{
    public class CountryCacheService : ICountryCacheService
    {
        private readonly INagerApiClient _client;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "nager_countries";

        public CountryCacheService(INagerApiClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<List<string>> GetCountryCodesAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<string>? cached))
                return cached!;

            var countries = await _client.GetAvailableCountriesAsync();

            var codes = countries
                .Select(c => c.CountryCode)
                .ToList();

            _cache.Set(CacheKey, codes, TimeSpan.FromHours(24));

            return codes;
        }

        public Task RefreshAsync()
        {
            _cache.Remove(CacheKey);
            return Task.CompletedTask;
        }
    }
}

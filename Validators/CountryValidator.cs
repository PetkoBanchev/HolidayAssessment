using HolidayAssessment.Data;
using HolidayAssessment.Services;
using Microsoft.EntityFrameworkCore;

namespace HolidayAssessment.Validators
{
    public class CountryValidator
    {
        private readonly ICountryCacheService _cache;
        public CountryValidator(ICountryCacheService cache)
        {
            _cache = cache;
        }

        public async Task ValidateCountryCodeAsync(string countryCode)
        {
            var codes = await _cache.GetCountryCodesAsync();
            var exists = codes.Contains(countryCode);

            if (!exists)
                throw new InvalidOperationException($"Invalid country code: {countryCode}");
        }

        public async Task ValidateCountryCodesAsync(List<string> countryCodes)
        {
            var codes = await _cache.GetCountryCodesAsync();

            var invalid = countryCodes
                .Where(c => !codes.Contains(c))
                .ToList();

            if (invalid.Any())
                throw new InvalidOperationException(
                    $"Invalid country codes: {string.Join(", ", invalid)}");
        }
    }
}

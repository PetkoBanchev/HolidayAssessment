using HolidayAssessment.Services;

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
            var normalized = countryCode.ToUpperInvariant();
            var codes = await _cache.GetCountryCodesAsync();
            var exists = codes.Contains(normalized);

            if (!exists)
                throw new InvalidOperationException($"Invalid country code: {countryCode}");
        }

        public async Task ValidateCountryCodesAsync(List<string> countryCodes)
        {
            var codes = await _cache.GetCountryCodesAsync();

            var invalid = countryCodes
                .Where(c => !codes.Contains(c.ToUpperInvariant()))
                .ToList();

            if (invalid.Any())
                throw new InvalidOperationException(
                    $"Invalid country codes: {string.Join(", ", invalid)}");
        }
    }
}

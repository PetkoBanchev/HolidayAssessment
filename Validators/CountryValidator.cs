using HolidayAssessment.Data;
using Microsoft.EntityFrameworkCore;

namespace HolidayAssessment.Validators
{
    public class CountryValidator
    {
        private readonly HolidayDbContext _db;

        public CountryValidator(HolidayDbContext db)
        {
            _db = db;
        }

        public async Task ValidateCountryCodeAsync(string countryCode)
        {
            var exists = await _db.Countries.AnyAsync(c => c.CountryCode == countryCode);

            if (!exists)
                throw new InvalidOperationException($"Invalid country code: {countryCode}");
        }

        public async Task ValidateCountryCodesAsync(List<string> countryCodes)
        {
            foreach (var countryCode in countryCodes)
                await ValidateCountryCodeAsync(countryCode);
        }
    }
}

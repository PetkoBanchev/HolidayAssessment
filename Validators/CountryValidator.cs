using HolidayAssessment.Common;
using HolidayAssessment.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HolidayAssessment.Validators
{
    public class CountryValidator
    {
        private readonly HolidayDbContext _db;

        public CountryValidator(HolidayDbContext db)
        {
            _db = db;
        }

        public async Task<InputValidationResult> ValidateCountryCodeAsync(string countryCode)
        {
            var exists = await _db.Countries.AnyAsync(c => c.CountryCode == countryCode);

            if (exists)
                return InputValidationResult.Success();
            else
                return InputValidationResult.Fail($"Invalid country code: {countryCode}");
        }

        public async Task<InputValidationResult> ValidateCountryCodesAsync(List<string> countryCodes)
        {
            var validCodes = await _db.Countries
                .Where(c => countryCodes.Contains(c.CountryCode))
                .Select(c => c.CountryCode)
                .ToListAsync();

            var invalidCodes = countryCodes.Except(validCodes).ToList();

            if (invalidCodes.Any())
                return InputValidationResult.Fail($"The following country codes are invalid: {string.Join(", ", invalidCodes)}");

            return InputValidationResult.Success();
        }
    }
}

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

        public async Task<bool> ValidateCountryCode(string countryCode)
        {
            return await _db.Countries
                .AnyAsync(c => c.CountryCode == countryCode);
        }
    }
}

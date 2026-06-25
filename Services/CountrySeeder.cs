using HolidayAssessment.Clients;
using HolidayAssessment.Data;
using HolidayAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace HolidayAssessment.Services
{
    public class CountrySeeder
    {
        private readonly HolidayDbContext _db;
        private readonly INagerApiClient _client;
        public CountrySeeder(HolidayDbContext db, INagerApiClient client)
        {
            _db = db;
            _client = client;
        }
        public async Task SeedAsync()
        {
            var existing = await _db.Countries
                .Select(x => x.CountryCode)
                .ToListAsync();

            var countries = await _client.GetAvailableCountriesAsync();

            var newCountries = countries
                .Where(c => !existing.Contains(c.CountryCode))
                .Select(c => new Country
                {
                    CountryCode = c.CountryCode,
                    Name = c.Name
                });

            _db.Countries.AddRange(newCountries);
            await _db.SaveChangesAsync();
        }
    }
}

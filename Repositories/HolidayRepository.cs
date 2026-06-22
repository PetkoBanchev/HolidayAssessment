using HolidayAssessment.Data;
using HolidayAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace HolidayAssessment.Repositories
{
    public class HolidayRepository: IHolidayRepository
    {
        private readonly AppDbContext _context;

        public HolidayRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Holiday> holidays)
        {
            await _context.Holidays.AddRangeAsync(holidays);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Holiday>> GetByCountryAndYearAsync(string countryCode, int year)
        {
            return await _context.Holidays
                .Where(h => h.CountryCode == countryCode && h.Date.Year == year)
                .ToListAsync();
        }

        public async Task<List<Holiday>> GetByCountryAsync(string countryCode)
        {
            return await _context.Holidays
                .Where(h => h.CountryCode == countryCode)
                .ToListAsync();
        }

        public async Task<List<Holiday>> GetByCountriesAsync(List<string> countryCodes, int year)
        {
            return await _context.Holidays
                .Where(h => countryCodes.Contains(h.CountryCode) && h.Date.Year == year)
                .ToListAsync();
        }
    }
}

using HolidayAssessment.Models;

namespace HolidayAssessment.Repositories
{
    public interface IHolidayRepository
    {
        Task AddRangeAsync(IEnumerable<Holiday> holidays);
        Task<List<Holiday>> GetByCountryAsync(string countryCode);
        Task<List<Holiday>> GetByCountryAndYearAsync(string countryCode, int year);
        Task<List<Holiday>> GetByCountriesAndYearAsync(List<string> countryCodes, int year);
    }
}

using HolidayAssessment.Models;

namespace HolidayAssessment.Repositories
{
    public interface IHolidayRepository
    {
        Task AddRangeAsync(IEnumerable<Holiday> holidays);
        Task<List<Holiday>> GetByCountryAndYearAsync(string countryCode, int year);
    }
}

using HolidayAssessment.Clients;
using HolidayAssessment.DTOs;
using HolidayAssessment.Models;
using HolidayAssessment.Repositories;
using System.Linq;

namespace HolidayAssessment.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly INagerApiClient _client;
        private readonly IHolidayRepository _repository;

        public HolidayService(INagerApiClient client, IHolidayRepository repository)
        {
            _client = client;
            _repository = repository;
        }

        public async Task<List<HolidayResponseDto>> GetLastThreeHolidaysAsync(string countryCode)
        {
            var holidays = await _repository.GetByCountryAsync(countryCode);

            return holidays
                .Where(h => h.Date < DateOnly.FromDateTime(DateTime.Today))
                .OrderByDescending(h => h.Date)
                .Take(3)
                .Select(h => new HolidayResponseDto
                {
                    Date = h.Date,
                    Name = h.Name
                })
                .ToList();
        }

        public async Task<List<WeekdayHolidayDto>> GetHolidaysOnWeekdaysAsync(int year, List<String> countryCodes)
        {
            var holidays = await _repository.GetByCountriesAsync(countryCodes, year);

            return holidays
                .Where(h => (h.Date.DayOfWeek != DayOfWeek.Saturday && h.Date.DayOfWeek != DayOfWeek.Sunday))
                .OrderBy (h => h.CountryCode)
                .ThenBy(h => h.Date)
                .Select(h => new WeekdayHolidayDto { Date = h.Date, Name = h.Name, CountryCode = h.CountryCode })
                .ToList();
        }

        public async Task<List<CountryHolidayCountDto>> GetNumberOfHolidaysNotOnWeekendsAsync(int year, List<String> countryCodes)
        {
            var holidays = await _repository.GetByCountriesAsync(countryCodes, year);

            return holidays
                .Where(h => h.Date.DayOfWeek != DayOfWeek.Saturday && h.Date.DayOfWeek != DayOfWeek.Sunday)
                .GroupBy(h => h.CountryCode)
                .Select(g => new CountryHolidayCountDto{CountryCode = g.Key, Count = g.Count()})
                .OrderByDescending(x => x.Count)
                .ToList();
        }

        public async Task<List<SharedHolidayDto>> GetNumberOfSharedHolidaysAsync(int year, List<string> countryCodes)
        {
            var holidaysA = await _repository.GetByCountryAndYearAsync(countryCodes[0], year);
            var holidaysB = await _repository.GetByCountryAndYearAsync(countryCodes[1], year);

            var sharedHolidays = holidaysA
                .IntersectBy(holidaysB.Select(h => h.Date), h => h.Date)
                .Select(h =>
                {
                    var holidayB = holidaysB.First(x => x.Date == h.Date);

                    return new SharedHolidayDto { Date = h.Date, LocalNameA = h.LocalName, LocalNameB = holidayB.LocalName };
                })
                
                .ToList();

            return sharedHolidays;
        }
        
        public async Task ImportHolidaysAsync(int year, List<string> countryCodes)
        {
            foreach (var country in countryCodes)
            {
                var existing = await _repository.GetByCountryAndYearAsync(country, year);

                var existingDates = existing
                    .Select(x => x.Date)
                    .ToHashSet();

                var apiData = await _client.GetPublicHolidaysAsync(year, country);

                var newEntities = apiData
                    .Where(x => !existingDates.Contains(x.Date))
                    .Select(x => new Holiday
                    {
                        Date = x.Date,
                        Name = x.Name,
                        LocalName = x.LocalName,
                        CountryCode = x.CountryCode,
                        Fixed = x.Fixed,
                        Global = x.Global,
                        LaunchYear = x.LaunchYear,
                        Counties = x.Counties is null ? null : string.Join(",", x.Counties),
                        Types = string.Join(",", x.Types)
                    })
                    .ToList();

                await _repository.AddRangeAsync(newEntities);
            }
        }
    }
}

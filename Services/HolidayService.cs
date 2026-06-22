using HolidayAssessment.Clients;
using HolidayAssessment.DTOs;
using HolidayAssessment.Models;
using HolidayAssessment.Repositories;

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

        public async Task ImportHolidaysAsync(int year, List<string> countryCodes)
        {
            foreach (var country in countryCodes)
            {
                var apiData = await _client.GetPublicHolidaysAsync(year, country);

                var entities = apiData.Select(dto => new Holiday
                {
                    Date = dto.Date,
                    LocalName = dto.LocalName,
                    Name = dto.Name,
                    CountryCode = dto.CountryCode,
                    Fixed = dto.Fixed,
                    Global = dto.Global,
                    LaunchYear = dto.LaunchYear,
                    Counties = dto.Counties is null ? null : string.Join(",", dto.Counties),
                    Types = string.Join(",", dto.Types)
                });

                await _repository.AddRangeAsync(entities);
            }
        }          
    }
}

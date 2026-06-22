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

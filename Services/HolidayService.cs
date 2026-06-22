using HolidayAssessment.Clients;
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

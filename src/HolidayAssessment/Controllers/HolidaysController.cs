using HolidayAssessment.DTOs;
using HolidayAssessment.DTOs.RequestDTOs;
using HolidayAssessment.Services;
using HolidayAssessment.Validators;
using Microsoft.AspNetCore.Mvc;

namespace HolidayAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidayService _service;
        private readonly CountryValidator _countryValidator;

        public HolidaysController(IHolidayService service,  CountryValidator validator) 
        {
            _service = service;
            _countryValidator = validator;
        }

        [HttpPost("import")]
        public async Task Import([FromBody] ImportHolidayRequestDto request)
        {
            YearValidator.Validate(request.Year);
            await _countryValidator.ValidateCountryCodesAsync(request.CountryCodes);

            await _service.ImportHolidaysAsync(request.Year, request.CountryCodes);
        }

        [HttpGet("{countryCode}/last-three")]
        public async Task<IEnumerable<HolidayResponseDto>> GetLastThree(string countryCode)
        { 
            await _countryValidator.ValidateCountryCodeAsync(countryCode); 

            var result = await _service.GetLastThreeHolidaysAsync(countryCode);
            return result;
        }

        [HttpPost("weekday-holidays")]
        public async Task<IEnumerable<WeekdayHolidayDto>> GetHolidaysOnWeekdays([FromBody] HolidayQueryRequestDto request)
        {            
            YearValidator.Validate(request.Year);
            await _countryValidator.ValidateCountryCodesAsync(request.CountryCodes);
            
            var result =  await _service.GetWeekdayHolidaysAsync(request.Year, request.CountryCodes);
            return result;
        }

        [HttpPost("weekday-holidays-count")]
        public async Task<IEnumerable<CountryHolidayCountDto>?> GetNumberOfHolidaysOnWeekdays([FromBody] HolidayQueryRequestDto request)
        { 
            YearValidator.Validate(request.Year);
            await _countryValidator.ValidateCountryCodesAsync(request.CountryCodes);

            var result = await _service.GetWeekdayHolidaysCountAsync(request.Year, request.CountryCodes);
            return result;
        }

        [HttpGet("shared-holidays")]
        public async Task<IEnumerable<SharedHolidayDto>> GetNumberOfSharedHolidays([FromQuery] int year, [FromQuery] string countryA, [FromQuery] string countryB)
        {
            YearValidator.Validate(year);
            await _countryValidator.ValidateCountryCodesAsync(new List<string> { countryA, countryB });

            var result = await _service.GetNumberOfSharedHolidaysAsync(year, countryA, countryB);
            return result;
        }
    }
}

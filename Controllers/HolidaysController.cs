using Azure.Core;
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
            try
            {
                YearValidator.Validate(request.Year);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            await _service.ImportHolidaysAsync(request.Year, request.CountryCodes);
        }

        [HttpGet("{countryCode}/last-three")]
        public async Task<IEnumerable<HolidayResponseDto>> GetLastThree(string countryCode)
        {
            try { await _countryValidator.ValidateCountryCodeAsync(countryCode); }
            catch (Exception ex) { }

            var result = await _service.GetLastThreeHolidaysAsync(countryCode);
            return result;
        }

        [HttpPost("holidays-on-weekdays")]
        public async Task<IEnumerable<WeekdayHolidayDto>> GetHolidaysOnWeekdays([FromBody] HolidayQueryRequestDto request)
        {
            try
            {
                YearValidator.Validate(request.Year);
            }
            catch (Exception ex)
            {
                
            }

            try { await _countryValidator.ValidateCountryCodesAsync(request.CountryCodes); }
            catch (Exception ex) { }

            var result =  await _service.GetHolidaysOnWeekdaysAsync(request.Year, request.CountryCodes);
            return result;
        }

        [HttpPost("number-of-holidays-on-weekdays-per-country")]
        public async Task<IEnumerable<CountryHolidayCountDto>?> GetNumberOfHolidaysOnWeekdays([FromBody] HolidayQueryRequestDto request)
        {
            try
            {
                YearValidator.Validate(request.Year);
            }
            catch (Exception ex) 
            {
                   
            }

            try { await _countryValidator.ValidateCountryCodesAsync(request.CountryCodes); }
            catch (Exception ex) { }


            var result = await _service.GetNumberOfHolidaysNotOnWeekendsAsync(request.Year, request.CountryCodes);
            return result;
        }

        [HttpGet("shared-holidays-per-two-countries")]
        public async Task<IEnumerable<SharedHolidayDto>> GetNumberOfSharedHolidays([FromQuery] int year, [FromQuery] string countryA, [FromQuery] string countryB)
        {
            try
            {
                YearValidator.Validate(year);
            }
            catch (Exception ex)
            {
                
            }
            try { await _countryValidator.ValidateCountryCodesAsync(new List<string> { countryA, countryB }); }
            catch (Exception ex) { }

            var result = await _service.GetNumberOfSharedHolidaysAsync(year, countryA, countryB);
            return result;
        }
    }
}

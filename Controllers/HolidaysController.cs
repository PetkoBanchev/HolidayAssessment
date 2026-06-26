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
        public async Task<IActionResult> Import([FromBody] ImportHolidayRequestDto request)
        {
            await _service.ImportHolidaysAsync(request.Year, request.CountryCodes);
            return Ok("Import completed");
        }

        [HttpGet("{countryCode}/last-three")]
        public async Task<ActionResult<List<HolidayResponseDto>>> GetLastThree(string countryCode)
        {
            var countryResult = await _countryValidator.ValidateCountryCodeAsync(countryCode);
            if (!countryResult.IsValid)
                return BadRequest(countryResult.ErrorMessage);

            var result = await _service.GetLastThreeHolidaysAsync(countryCode);
            return Ok(result);
        }

        [HttpPost("holidays-on-weekdays")]
        public async Task<ActionResult<List<WeekdayHolidayDto>>> GetHolidaysOnWeekdays([FromBody] HolidayQueryRequestDto request)
        {
            var yearResult = YearValidator.Validate(request.Year);
            if(!yearResult.IsValid)
                return BadRequest(yearResult.ErrorMessage);

            var countryResult = await _countryValidator.ValidateCountryCodesAsync(request.CountryCodes);
            if (!countryResult.IsValid)
                return BadRequest(countryResult.ErrorMessage);

            var result =  await _service.GetHolidaysOnWeekdaysAsync(request.Year, request.CountryCodes);
            return Ok(result);
        }

        [HttpPost("number-of-holidays-on-weekdays-per-country")]
        public async Task<ActionResult<List<WeekdayHolidayDto>>> GetNumberOfHolidaysOnWeekdays([FromBody] HolidayQueryRequestDto request)
        {
            var yearResult = YearValidator.Validate(request.Year);
            if (!yearResult.IsValid)
                return BadRequest(yearResult.ErrorMessage);

            var countryResult = await _countryValidator.ValidateCountryCodesAsync(request.CountryCodes);
            if (!countryResult.IsValid)
                return BadRequest(countryResult.ErrorMessage);


            var result = await _service.GetNumberOfHolidaysNotOnWeekendsAsync(request.Year, request.CountryCodes);
            return Ok(result);
        }

        [HttpGet("shared-holidays-per-two-countries")]
        public async Task<ActionResult<List<SharedHolidayDto>>> GetNumberOfSharedHolidays([FromQuery] int year, [FromQuery] string countryA, [FromQuery] string countryB)
        {
            var yearResult = YearValidator.Validate(year);
            if (!yearResult.IsValid)
                return BadRequest(yearResult.ErrorMessage);

            var countryResult = await _countryValidator.ValidateCountryCodesAsync(new List<string> { countryA, countryB });
            if (!countryResult.IsValid)
                return BadRequest(countryResult.ErrorMessage);

            var result = await _service.GetNumberOfSharedHolidaysAsync(year, countryA, countryB);
            return Ok(result);
        }
    }
}

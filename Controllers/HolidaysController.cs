using HolidayAssessment.DTOs;
using HolidayAssessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolidayAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidayService _service;

        public HolidaysController(IHolidayService service) 
        {
            _service = service;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import([FromBody] ImportHolidayRequestDto request)
        {
            await _service.ImportHolidaysAsync(request.Year, request.CountryCodes);
            return Ok("Import completed");
        }

        [HttpGet("{countryCode}/last-three")]
        public async Task<IActionResult> GetLastThree(string countryCode)
        {
            var result = await _service.GetLastThreeHolidaysAsync(countryCode);
            return Ok(result);
        }

        [HttpGet("holidays-on-weekdays")]
        public async Task<IActionResult> GetHolidaysOnWeekdays([FromQuery] int year, [FromQuery] List<string> countryCodes)
        {
            var result =  await _service.GetHolidaysOnWeekdaysAsync(year, countryCodes);
            return Ok(result);
        }

        [HttpGet("number-of-honidays-on-weekdays-per-country")]
        public async Task<IActionResult> GetNumberOfHolidaysOnWeekdays([FromQuery] int year, [FromQuery] List<string> countryCodes)
        {
            var result = await _service.GetNumberOfHolidaysNotOnWeekendsAsync(year, countryCodes);
            return Ok(result);
        }

        [HttpGet("shared-holidays-per-two-countries")]
        public async Task<IActionResult> GetNumberOfSharedHolidays([FromQuery] int year, [FromQuery] List<string> countryCodes)
        {
            var result = await _service.GetNumberOfSharedHolidaysAsync(year, countryCodes);
            return Ok(result);
        }
    }
}

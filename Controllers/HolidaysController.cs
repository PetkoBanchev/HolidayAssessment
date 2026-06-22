using HolidayAssessment.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HolidayAssessment.DTOs;
using HolidayAssessment.Services;

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
    }
}

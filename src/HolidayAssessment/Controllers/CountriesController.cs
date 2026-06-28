using HolidayAssessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolidayAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {
        private readonly ICountryCacheService _cache;

        public CountriesController(ICountryCacheService cache)
        {
            _cache = cache;
        }

        [HttpGet("get-countries")]
        public async Task<List<string>> GetCountries()
        {
            return await _cache.GetCountryCodesAsync();
        }
    }
}

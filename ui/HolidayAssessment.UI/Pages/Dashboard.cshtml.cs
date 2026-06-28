using HolidayAssessment.Services;
using HolidayAssessment.UI.DTOs;
using HolidayAssessment.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.WebRequestMethods;

namespace HolidayAssessment.UI.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IHolidayApiService _holidaysApi;
        private readonly ICountryApiService _countryService;

        public DashboardModel(IHolidayApiService holidaysApi, ICountryApiService countryService)
        {
            _holidaysApi = holidaysApi;
            _countryService = countryService;
        }

        public List<SelectListItem> CountryOptions { get; set; } = new();

        // Import
        public List<string> ImportCountries { get; set; } = new();
        public int ImportYear { get; set; } = DateTime.Today.Year;

        // Last 3 holidays
        public string LastThreeCountry { get; set; } = "NL";

        // Weekday holidays
        public List<string> WeekdayCountries { get; set; } = new();
        public int WeekdayYear { get; set; } = DateTime.Today.Year;

        // Weekday holiday count
        public List<string> CountCountries { get; set; } = new();
        public int CountYear { get; set; } = DateTime.Today.Year;

        // Shared holidays
        public string CountryA { get; set; } = "NL";
        public string CountryB { get; set; } = "DE";
        public int SharedYear { get; set; } = DateTime.Today.Year;

        // Results
        public List<HolidayResponseDto> LastThreeHolidays { get; set; } = new();
        public List<WeekdayHolidayDto> WeekdayHolidays { get; set; } = new();
        public List<CountryHolidayCountDto> WeekdayHolidayCounts { get; set; } = new();
        public List<SharedHolidayDto> SharedHolidays { get; set; } = new();

        public async Task OnGetAsync()
        {
            var countries = await _countryService.GetCountryCodesAsync();

            CountryOptions = countries.Select(c => new SelectListItem
            {
                Value = c,
                Text = c
            }).ToList();
        }
    }
}

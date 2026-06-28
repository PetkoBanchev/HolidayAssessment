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
        private readonly ICountryApiService _countryApi;

        public DashboardModel(IHolidayApiService holidaysApi, ICountryApiService countryApi)
        {
            _holidaysApi = holidaysApi;
            _countryApi = countryApi;
        }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
            
        public List<SelectListItem> CountryOptions { get; set; } = new();

        // Import
        [BindProperty] public List<string> ImportCountries { get; set;  } = new();
        
        [BindProperty] public int ImportYear { get; set; } = DateTime.Today.Year;

        // Last 3 holidays
        [BindProperty(SupportsGet = true)] public string LastThreeCountry { get; set; }

        // Weekday holidays
        [BindProperty] public List<string> WeekdayCountries { get; set; } = new();
        
        [BindProperty] public int WeekdayYear { get; set; } = DateTime.Today.Year;

        // Weekday holiday count
        [BindProperty] public List<string> CountCountries { get; set; } = new();
        [BindProperty] public int CountYear { get; set; } = DateTime.Today.Year;

        // Shared holidays
        [BindProperty] public string CountryA { get; set; } = "NL";
        [BindProperty] public string CountryB { get; set; } = "DE";
        [BindProperty] public int SharedYear { get; set; } = DateTime.Today.Year;

        // Results
        public List<HolidayResponseDto> LastThreeHolidays { get; set; } = new();
        public List<WeekdayHolidayDto> WeekdayHolidays { get; set; } = new();
        public List<CountryHolidayCountDto> WeekdayHolidayCounts { get; set; } = new();
        public List<SharedHolidayDto> SharedHolidays { get; set; } = new();

        public async Task OnGetAsync()
        {
            await LoadCountriesAsync();
        }

        public async Task<IActionResult> OnPostImportAsync()
        {
            await LoadCountriesAsync();

            try
            {
                 await _holidaysApi.ImportHolidaysAsync(ImportYear, ImportCountries);

                SuccessMessage = "Holidays imported successfully.";
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnGetLastThreeAsync()
        {
            try
            {
                LastThreeHolidays = await _holidaysApi.GetLastThreeAsync(LastThreeCountry);
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = ex.Message;
            }

            await LoadCountriesAsync();
            return Page();
        }

        private async Task LoadCountriesAsync()
        {
            var countries = await _countryApi.GetCountryCodesAsync();

            CountryOptions = countries
                .Select(c => new SelectListItem(c, c))
                .ToList();
        }
    }
}

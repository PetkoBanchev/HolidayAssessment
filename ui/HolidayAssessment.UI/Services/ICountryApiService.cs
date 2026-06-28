namespace HolidayAssessment.UI.Services
{
    public interface ICountryApiService
    {
        Task<List<string>> GetCountryCodesAsync();
    }
}

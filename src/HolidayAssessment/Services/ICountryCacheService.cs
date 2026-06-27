namespace HolidayAssessment.Services
{
    public interface ICountryCacheService
    {
        Task<List<string>> GetCountryCodesAsync();
        Task RefreshAsync();
    }
}

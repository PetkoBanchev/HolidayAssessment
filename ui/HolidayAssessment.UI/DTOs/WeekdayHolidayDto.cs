namespace HolidayAssessment.UI.DTOs
{
    public class WeekdayHolidayDto
    {
        public DateOnly Date { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
    }
}

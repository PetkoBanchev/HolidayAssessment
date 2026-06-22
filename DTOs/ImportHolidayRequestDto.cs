namespace HolidayAssessment.DTOs
{
    public class ImportHolidayRequestDto
    {
        public int Year { get; set; }

        public List<string> CountryCodes { get; set; } = [];
    }
}

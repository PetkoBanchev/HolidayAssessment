namespace HolidayAssessment.DTOs
{
    public class HolidayApiDto
    {
        public DateOnly Date { get; set; }

        public string LocalName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;

        public bool Fixed { get; set; }

        public bool Global { get; set; }

        public List<string>? Counties { get; set; }

        public int? LaunchYear { get; set; }

        public List<string> Types { get; set; } = [];
    }
}

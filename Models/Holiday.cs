using System.ComponentModel.DataAnnotations;

namespace HolidayAssessment.Models
{
    public class Holiday
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        [MaxLength(100)]
        public string LocalName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2)]
        public string CountryCode { get; set; } = string.Empty;

        public bool Fixed { get; set; }

        public bool Global { get; set; }

        public string? Counties { get; set; }

        public int? LaunchYear { get; set; }

        public string Types { get; set; } = string.Empty;
    }
}

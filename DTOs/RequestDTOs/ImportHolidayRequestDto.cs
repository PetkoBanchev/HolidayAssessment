using HolidayAssessment.Constants;
using System.ComponentModel.DataAnnotations;

namespace HolidayAssessment.DTOs.RequestDTOs
{
    public class ImportHolidayRequestDto
    {
        [Range(NagerConstraints.MinYear, NagerConstraints.MaxYear)]
        public int Year { get; set; }

        [Required]
        [MinLength(1)]
        public List<string> CountryCodes { get; set; } = [];
    }
}

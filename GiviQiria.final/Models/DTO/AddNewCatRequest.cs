using System.ComponentModel.DataAnnotations;

namespace GiviQiria.final.Models.DTO
{
    public class AddNewCatRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public DateOnly? BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Varieties is required")]
        public string? Varieties { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be greater than 0")]
        public double? Weight { get; set; }
    }
}

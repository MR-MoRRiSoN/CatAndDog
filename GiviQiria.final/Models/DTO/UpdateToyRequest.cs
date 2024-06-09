namespace GiviQiria.final.Models.DTO
{
    public class UpdateToyRequest
    {
        public Guid ToyId { get; set; }
        public string? ToyName { get; set; }
        public double? Weight { get; set; }
        public string? Color { get; set; }
    }
}

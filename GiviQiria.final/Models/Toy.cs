namespace GiviQiria.final.Models
{
    public class Toy
    {
        public Guid ToyId { get; set; }
        public string? ToyName { get; set; }
        public double? Weight { get; set; }
        public string? Color { get; set; }
        public Cat? ToyOwner { get; set; }
        public Guid ToyOwnerId { get; set; }

    }
}

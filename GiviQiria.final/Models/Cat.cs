namespace GiviQiria.final.Models
{
    public class Cat
    {
        public Guid CatId { get; set; }
        public string? Name { get; set; }
        public DateOnly? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Varieties { get; set; }
        public double? Weight { get; set; }
        public List<Toy>? CatToys { get; set; }

    }
}

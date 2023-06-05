namespace RateYourMovie.Models
{
    public class Director
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }

    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;


namespace RateYourMovie.Models
{
    
    public class Movie
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [DisplayName("Title")]
        public string Name { get; set; }
        [DisplayName("Directed by")]
        public string DirectorName { get; set; }
        [DisplayName("Created/Last edited")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [DisplayName("Release year")]
        [Range(1887, 2031, ErrorMessage = "Please enter a value between 1888 and 2030")]
        public int ReleaseYear { get; set; }
        public double Rating { get; set; } = (double)0;
        public bool IsDeleted { get; set; } = false;
        public string? PosterUrl { get; set; }
        public string? Description { get; set; }
    }
}

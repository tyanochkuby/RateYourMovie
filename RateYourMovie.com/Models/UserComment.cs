using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using RateYourMovie.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateYourMovie.Models
{
    public class UserComment
    {
        [Key]
        public string RatingId { get; set; } = Guid.NewGuid().ToString();
        [System.ComponentModel.DataAnnotations.Required]
        [DisplayName("Your rating")]
        public int NumberOfStars { get; set; }
        public string? Comment { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        [System.ComponentModel.DataAnnotations.Required]
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public virtual RateYourMovieUser? AspNetUsers { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [ForeignKey("MovieId")]
        public string MovieId { get; set; }
        public virtual Movie? Movie { get; set; }
    }
}

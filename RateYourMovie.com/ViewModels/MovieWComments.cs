using Microsoft.CodeAnalysis.CSharp.Syntax;
using RateYourMovie.Data;
using RateYourMovie.Models;

namespace RateYourMovie.ViewModels
{
    public class MovieWComments : Movie
    {
        public MovieWComments(Movie movie, List<UserComment> comments, LoginDbContext db)
        {
            Id = movie.Id;
            Name= movie.Name;
            DirectorName  = movie.DirectorName;
            CreatedDateTime= movie.CreatedDateTime;
            ReleaseYear= movie.ReleaseYear;
            Rating= movie.Rating;
            IsDeleted= movie.IsDeleted;
            PosterUrl= movie.PosterUrl;
            Description= movie.Description;
            Comments= comments;
            Db = db;
        }
        public List<UserComment>? Comments { get; set; }
        public LoginDbContext Db { get; set; }
    }
}

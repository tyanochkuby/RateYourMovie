using RateYourMovie.Models;
namespace RateYourMovie.ViewModels
{
    public class MovieListWMaxPage
    {
        public IEnumerable<Movie> MovieList { get; set; }
        public int MaxPage;
        public int CurrentPage;
        public MovieListWMaxPage(IEnumerable<Movie> movieList, int maxPage, int currentPage)
        {
            MovieList = movieList;
            MaxPage = maxPage;
            CurrentPage = currentPage;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RateYourMovie.Data;
using RateYourMovie.Models;
using RateYourMovie.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using PagedList;

namespace RateYourMovie.Controllers
{
    public class MovieController : Controller
    {
        private readonly LoginDbContext _db;

        public MovieController(LoginDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DirectorSortParam = sortOrder == "Director" ? "Director_decs" : "Director";
            ViewBag.RatingSortParam = sortOrder == "Rating" ? "Rating_desc" : "Rating";

            if (searchString != null)
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            IEnumerable<Movie> MovieList = _db.Movie.Skip(pageSize*(pageNumber-1)).Take(pageSize);

            if (!String.IsNullOrEmpty(searchString))
                MovieList = MovieList.Where(m => m.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) || m.DirectorName.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var MovieIds = from m in _db.Movie select m.Id;
            
            IEnumerable<UserComment> CommentList = _db.Rating.Where(c => MovieIds.Contains(c.MovieId));
            var dick = new Dictionary<string, List<int>>();
            foreach (UserComment comment in CommentList)
            {
                if (!dick.ContainsKey(comment.MovieId))
                    dick.Add(comment.MovieId, new List<int>{ comment.NumberOfStars });
                else
                    dick[comment.MovieId].Add(comment.NumberOfStars);
            }

            foreach(var movie in MovieList)
                if(dick.ContainsKey(movie.Id))
                    movie.Rating = dick[movie.Id].Average()/2;
            switch (sortOrder)
            {
                case "name_desc":
                    MovieList = MovieList.OrderByDescending(m => m.Name);
                    break;
                case "Director":
                    MovieList = MovieList.OrderBy(m => m.DirectorName);
                    break;
                case "Director_desc":
                    MovieList = MovieList.OrderByDescending(m => m.DirectorName);
                    break;
                case "Rating":
                    MovieList = MovieList.OrderByDescending(m => m.Rating);
                    break;
                case "Rating_desc":
                    MovieList = MovieList.OrderBy(m => m.Rating);
                    break;
                default:
                    MovieList = MovieList.OrderBy(m => m.Name);
                    break;
            }
            
            return View(new MovieListWMaxPage(MovieList, _db.Movie.Count()/pageSize + (_db.Movie.Count() % pageSize == 0 ? 0 : 1), pageNumber));
        }

        //GET
        public IActionResult Details(string id)
        {
            var commentList = _db.Rating.Where(c => c.MovieId == id).ToList(); //(from Comment in _db.Rating where Comment.MovieId == id select Comment).ToList();
            Movie movie = _db.Movie.Find(id) ?? new Movie { };
            var rating = (from c in _db.Rating where c.MovieId == id select c.NumberOfStars).ToList().DefaultIfEmpty(0).Average();
            var detailModel = new MovieWComments(movie, commentList, _db);
            detailModel.Rating = rating;
            return View(detailModel);
        }

        //GET
        public IActionResult Rate(string id)
        {
            if (id == null)
                return NotFound();
            string? userId = null;
            ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
            }
            
            if(userId == null)
                return BadRequest();
            var objComment = new UserComment();
            var query = _db.Rating.Where(c => c.MovieId == id && c.UserId == userId)
                                  .Select(c => new { c.Comment, c.NumberOfStars })
                                  .FirstOrDefault(); //from comment in _db.Rating where comment.MovieId == id && comment.UserId == userId select new { comment.Comment, comment.NumberOfStars }).FirstOrDefault();
            if (query != null)
            {
                objComment.Comment = query.Comment;
                objComment.NumberOfStars = query.NumberOfStars;
            }
            else 
            { 
                objComment.Comment = null;
                objComment.NumberOfStars = 0;
            }
            objComment.UserId = userId;
            objComment.MovieId = id;
            
            return View(objComment);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rate(UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                var commentsOfUser = (_db.Rating.Where(c => c.UserId == userComment.UserId))
                                                .ToList();
                foreach(var comment in commentsOfUser)
                {
                    if (comment.MovieId == userComment.MovieId)
                        _db.Rating.Remove(comment);
                }                    
                _db.Rating.Add(userComment);
                _db.SaveChanges();
                TempData["success"] = "Comment has been added";
                return RedirectToAction("Index");
            }
            return View(userComment);
        }

        //GET
        public IActionResult Remove(string? id)
        {
            if (id == null)
                return NotFound();
            var movie = _db.Movie.Find(id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(Movie obj)
        {
            obj.IsDeleted = true;
            _db.Movie.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Movie has been deleted";
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(string? id)
        {
            if(id == null)
                return NotFound();
            var objCategory = _db.Movie.Find(id);
            if(objCategory == null)
                return NotFound();
            return View(objCategory);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (movie.DirectorName.Any(char.IsDigit))
                ModelState.AddModelError("DirectorName", "Names can't contain numbers");
            if (ModelState.IsValid)
            {
                _db.Movie.Update(movie);
                _db.SaveChanges();
                TempData["success"] = "Movie has been edited successfully";
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie obj)
        {
            if (obj.DirectorName.Any(char.IsDigit))
                ModelState.AddModelError("DirectorName", "Names can't contain numbers");
            if (ModelState.IsValid)
            {
                _db.Movie.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "New movie has been added";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}

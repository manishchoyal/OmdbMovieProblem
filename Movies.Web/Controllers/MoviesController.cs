using Microsoft.AspNetCore.Mvc;
using Movies.Service;
using Movies.Web.Models.Movies;
using Schema.NET;
using System;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        protected readonly IMovieService _movieService;
        public MoviesController(IMovieService iMovieService)
        {
            _movieService = iMovieService;
        }
        public IActionResult Index()
        {
            var model = new SearchResults();
            model.LoadSampleData();

            Person author = new Person();
            author.Name = "Manish";
            var articleStructuredSchema = new Schema.NET.Article()
            {
                Author = author,
                Creator = author,
                Headline = "Heading",
                Publisher = new Organization() { Name = "Your Organization" },
                ArticleBody = "Body",
                
            };
            ViewBag.JasonLd = articleStructuredSchema.ToString();

            //var model1 = _movieService.GetSearchListAsync("avengers", 5);

            return View(model);
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Search(SearchResult search)
        {
            //var model = new SearchResults();
            //model.LoadSampleData();
            var model = _movieService.GetSearchListAsync("avengers", 5);
            
            return View(model.Result);
        }
    }
}

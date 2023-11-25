using CineProject.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CineProject.Controllers
{
    public class HomeController : Controller
    {
    private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService) 
        {
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            var movies = _movieService.List();
            return View(movies);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}

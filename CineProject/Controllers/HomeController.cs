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
        public IActionResult Index(string term, int currentPage = 1)
        {
            //var movies = _movieService.List();
            //return View(movies);

            // var movies = string.IsNullOrEmpty(term)
            //? _movieService.List() // Obtener todas las películas si no hay término de búsqueda
            //: _movieService.GetMoviesByTitle(term); // Método para buscar películas por título

            //   return View(movies);

            const int pageSize = 5; // Define el tamaño de la página
            var movies = string.IsNullOrEmpty(term)
                ? _movieService.List(paging: true, currentPage: currentPage)
                : _movieService.GetMoviesByTitle(term, paging: true, currentPage: currentPage);

            return View(movies);

        }

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult MovieDetail(int movieId)
        {
            var movie = _movieService.GetById(movieId);
            return View(movie);
        }
    }
}

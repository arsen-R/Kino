using KinoSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using KinoSite.Data;

namespace KinoSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationContext context;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index(int PageNumber = 1)
        {
            var movies = context.Movies.Select(m => m);

            ViewBag.TotalPages = Math.Ceiling(movies.Count() / 20.0);
            movies = movies
                .Skip((PageNumber - 1) * 20)
                .Take(20)
                .OrderByDescending(m => m.Id)
                .ThenByDescending(m => m.Id);
            return View(movies.ToList());
            //

        }
        public IActionResult Search(string searchString)
        {
            var movies = context.Movies.Select(m => m);
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            return View(movies);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

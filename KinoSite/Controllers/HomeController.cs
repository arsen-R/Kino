﻿using KinoSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Data;
using KinoSite.Areas.Identity.Data;
namespace KinoSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        MovieContext context;
        public HomeController(ILogger<HomeController> logger, MovieContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index(int PageNumber = 1)
        {
            //var movies = from m in context.Movies
            //             select m;
            var movies = context.Movies.Select(m => m);
           
            ViewBag.TotalPages = Math.Ceiling(movies.Count() / 30.0);
            movies = movies.Skip((PageNumber - 1) * 30).Take(30);
            return View(movies.ToList());
            //.OrderBy(m => m.Title).ThenByDescending(m => m.Id)
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

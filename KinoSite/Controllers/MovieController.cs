using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using KinoSite.Models;
using KinoSite.Data;

namespace KinoSite.Controllers
{
    public class MovieController : Controller
    {
        ApplicationContext context;
        public MovieController(ApplicationContext context)
        {
            this.context = context;
        }
        //public async Task<IActionResult> ListMovie()
        //{          
        //    return View(context.Movies.ToList());
        //}
        //public IActionResult CreateMovie()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateMovie(Movie movie)
        //{
        //    context.Movies.Add(movie);
        //    context.SaveChanges();
        //    return View();
        //}
    }
}

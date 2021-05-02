using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using KinoSite.Models;
using KinoSite.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace KinoSite.Controllers
{
    public class MovieController : Controller
    {
        ApplicationContext context;
        public MovieController(ApplicationContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> ListMovie(string searchString)
        {
            var movies = context.Movies.Select(m => m);
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            return View(movies.ToList());
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                context.Movies.Add(movie);
                context.SaveChanges();
                return RedirectToAction("ListMovie");
            }
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Movie movie = context.Movies.Select(m => m).Where(m => m.Id == Id).FirstOrDefault();
            //context.Movies.Include(m => m.Directions);
            return View(movie);
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Movies = Id;
            Movie movie = context.Movies.Select(m => m).Where(m => m.Id == Id).First();
            return View(movie);
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                //movie = await ActionWithImage(movie, Video);
                context.Movies.Update(movie);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.MovieDetails = id;
            Movie movie = context.Movies.Select(m => m).Where(m => m.Id == id).First();
            return View(movie);
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Delete(Movie movie)
        {
            if (ModelState.IsValid)
            {
                //details = await ActionWithImage(details, ImagesMovie);
                context.Movies.Remove(movie);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Movie(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var movie = context.Movies.Select(m => m).Where(m => m.Id == Id).FirstOrDefault();
            return View(movie);
        }
        private async Task<Movie> ActionWithImage(Movie details, List<IFormFile> Image)
        {
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        details.Image = stream.ToArray();
                    }
                }
            }
            return details;
        }
    }
}

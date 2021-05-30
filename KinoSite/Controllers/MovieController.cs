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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KinoSite.Areas.Identity.Data;

namespace KinoSite.Controllers
{
    public class MovieController : Controller
    {
        MovieContext context;
        public MovieController(MovieContext context)
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
            List<SelectListItem> selectListItems = context.Genres.Select(a => new SelectListItem
            {
                Text = a.NameGenre,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.GenreId = selectListItems;

            List<SelectListItem> selectListItems1 = context.Actors.Select(a => new SelectListItem
            {
                Text = a.SurnameActor,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.ActorId = selectListItems1;

            ViewBag.DirectionId = new SelectList(context.Directions, "Id", "SurnameDirection");
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, List<IFormFile> Image, List<int> GenreIdList, List<int> ActorIdList)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                context.Movies.Add(movie);
                context.SaveChanges();
                foreach (var item in GenreIdList)
                {
                    GenreMovie movieGenre = new GenreMovie()
                    {
                        GenreId = item,
                        MovieId = movie.Id,
                        Genre = context.Genres.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    context.GenreMovies.Add(movieGenre);
                }
                context.SaveChanges();

                foreach (var item in ActorIdList)
                {
                    ActorMovie actorMovie = new ActorMovie()
                    {
                        ActorId = item,
                        MovieId = movie.Id,
                        Actor = context.Actors.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    context.ActorMovies.Add(actorMovie);
                }
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
                return NotFound();
            }
            var movie = context.Movies
                .Select(m => m)
                .Where(m => m.Id == Id)
                .Include(d => d.Directions)                
                .FirstOrDefault();
            return View(movie);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            List<SelectListItem> selectListItems = context.Genres.Select(a => new SelectListItem
            {
                Text = a.NameGenre,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.GenreId = selectListItems;

            List<SelectListItem> selectListItems1 = context.Actors.Select(a => new SelectListItem
            {
                Text = a.SurnameActor,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.ActorId = selectListItems1;

            ViewBag.DirectionId = new SelectList(context.Directions, "Id", "SurnameDirection");
            ViewBag.Movies = Id;
            Movie movie = context.Movies.Select(m => m).Where(m => m.Id == Id).First();
            return View(movie);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie, List<IFormFile> Image, List<int> GenreIdLists, List<int> ActorIdLists)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                context.Movies.Update(movie);
                context.SaveChanges();
                foreach (var item in GenreIdLists)
                {
                    GenreMovie genreMovie = new GenreMovie()
                    {
                        GenreId = item,
                        MovieId = movie.Id,
                        Genre = context.Genres.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    context.GenreMovies.Add(genreMovie);
                }
                context.SaveChanges();

                foreach (var item in ActorIdLists)
                {
                    ActorMovie actorMovie = new ActorMovie()
                    {
                        ActorId = item,
                        MovieId = movie.Id,
                        Actor = context.Actors.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    context.ActorMovies.Update(actorMovie);
                }
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
                return NotFound();
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
                return NotFound();
            }
            Movie movie = context.Movies
                .Select(m => m)
                .Where(m => m.Id == Id)
                .Include(m => m.Directions)
                .FirstOrDefault();           
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

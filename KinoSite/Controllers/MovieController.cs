using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KinoSite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KinoSite.Areas.Identity.Data;
using KinoSite.Models.Comments;
using KinoSite.ViewModel;
using Microsoft.AspNetCore.Identity;
using KinoSite.Data;

namespace KinoSite.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationContext context;
        private UserManager<ApplicationUser> _userManager;
        public MovieController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        //[HttpPost]
        //public async Task<IActionResult> Comment(CommentViewModel vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("PlayMovie", new { Id = vm.MovieId });
        //    }
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var post = context.Movies
        //        .Select(m => m)
        //        .Where(m => m.Id == vm.MovieId)
        //        .Include(m => m.MainComments)
        //        .ThenInclude(mc => mc.SubComments)
        //        .FirstOrDefault();
        //        if (vm.MainCommentId == 0)
        //        {
        //            post.MainComments = post.MainComments ?? new List<MainComment>();
        //            post.MainComments.Add(new MainComment
        //            {
        //                Message = vm.Message,
        //                Created = DateTime.Now,
        //            });
        //            context.Movies.Update(post);
        //        }
        //        else
        //        {
        //            var comment = new SubComment
        //            {
        //                MainCommentId = vm.MainCommentId,
        //                Message = vm.Message,
        //                Created = DateTime.Now
        //            };
        //            context.SubComments.Add(comment);
        //        }
        //        await context.SaveChangesAsync();

        //        return RedirectToAction("PlayMovie", new { Id = vm.MovieId });
        //    }
        //    return RedirectToAction("PlayMovie", new { Id = vm.MovieId });
        //}
        [HttpGet]
        public IActionResult Movie(string searchString, int PageNumber = 1)
        {
            var movies = context.Movies.Select(m => m);
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            ViewBag.TotalPages = Math.Ceiling(movies.Count() / 30.0);
            movies = movies
                .Skip((PageNumber - 1) * 30)
                .Take(30);
                //.Include(m => m.Category);
            return View(movies.ToList());
        }
        [HttpGet]
        public IActionResult PlayMovie(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var movie = context.Movies
                 .Select(m => m)
                 .Where(m => m.Id == Id)
                 //.Include(m => m.Directions)
                 //.Include(m => m.Category)
                 //.Include(m => m.MainComments).ThenInclude(m => m.SubComments)
                 //.Include(m => m.GenreMovies).ThenInclude(m => m.Genre)
                 //.Include(m => m.ActorMovies).ThenInclude(m => m.Actor)
                 .FirstOrDefault();
            return View(movie);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> MovieList(string searchString, int PageNumber = 1)
        {
            var movies = context.Movies.Select(m => m);
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            ViewBag.TotalPages = Math.Ceiling(movies.Count() / 15.0);
            movies = movies
                .Skip((PageNumber - 1) * 15)
                .Take(15); 
                //.Include(m => m.Category);
            return View(movies.ToList());
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Create()
        {
            //List<SelectListItem> selectListItems = context.Genres.Select(a => new SelectListItem
            //{
            //    Text = a.NameGenre,
            //    Value = a.Id.ToString()
            //}).ToList();
            //ViewBag.GenreId = selectListItems;

            //List<SelectListItem> selectListItems1 = context.Actors.Select(a => new SelectListItem
            //{
            //    Text = a.FullName,
            //    Value = a.Id.ToString()
            //}).ToList();
            //ViewBag.ActorId = selectListItems1;

            //ViewBag.DirectionId = context.Directions.Select(d => new SelectListItem
            //{
            //    Text = d.FullName,
            //    Value = d.Id.ToString()
            //});
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, List<IFormFile> Image, List<int> GenreIdList, List<int> ActorIdList)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                //movie.CategoryId = 1;
                context.Movies.Add(movie);
                context.SaveChanges();

                //foreach (var item in GenreIdList)
                //{
                //    GenreMovie movieGenre = new GenreMovie()
                //    {
                //        GenreId = item,
                //        MovieId = movie.Id,
                //        Genre = context.Genres.Where(x => x.Id == item).FirstOrDefault(),
                //        Movie = movie
                //    };
                //    context.GenreMovies.Add(movieGenre);
                //}
                //context.SaveChanges();

                //foreach (var item in ActorIdList)
                //{
                //    ActorMovie actorMovie = new ActorMovie()
                //    {
                //        ActorId = item,
                //        MovieId = movie.Id,
                //        Actor = context.Actors.Where(x => x.Id == item).FirstOrDefault(),
                //        Movie = movie
                //    };
                //    context.ActorMovies.Add(actorMovie);
                //}
                //context.SaveChanges();

                return RedirectToAction("MovieList", "Movie");
            }
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            //List<SelectListItem> selectListItems = context.Genres.Select(a => new SelectListItem
            //{
            //    Text = a.NameGenre,
            //    Value = a.Id.ToString()
            //}).ToList();
            //ViewBag.GenreId = selectListItems;

            //List<SelectListItem> selectListItems1 = context.Actors.Select(a => new SelectListItem
            //{
            //    Text = a.FullName,
            //    Value = a.Id.ToString()
            //}).ToList();
            //ViewBag.ActorId = selectListItems1;

            //ViewBag.DirectionId = context.Directions.Select(d => new SelectListItem
            //{
            //    Text = d.FullName,
            //    Value = d.Id.ToString()
            //});

            //ViewBag.CategoryId = context.Categories.Select(c => new SelectListItem
            //{
            //    Text = c.NameCategory,
            //    Value = c.Id.ToString()
            //});
            //new SelectList(context.Directions, "Id", "SurnameDirection");
            ViewBag.Movies = Id;
            Movie movie = context.Movies.Select(m => m).Where(m => m.Id == Id).First();
            return View(movie);
        }
        // Виправити Update
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie, List<IFormFile> Image, List<int> GenreIdLists, List<int> ActorIdLists)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                //movie.CategoryId = 1;
                context.Movies.Update(movie);
                context.SaveChanges();

                //foreach (var item in GenreIdLists)
                //{
                //    GenreMovie genreMovie = new GenreMovie()
                //    {
                //        GenreId = item,
                //        MovieId = movie.Id,
                //        Genre = context.Genres.Where(x => x.Id == item).FirstOrDefault(),
                //        Movie = movie
                //    };
                //    context.GenreMovies.Add(genreMovie);
                //}
                //context.SaveChanges();

                //foreach (var item in ActorIdLists)
                //{
                //    ActorMovie actorMovie = new ActorMovie()
                //    {
                //        ActorId = item,
                //        MovieId = movie.Id,
                //        Actor = context.Actors.Where(x => x.Id == item).FirstOrDefault(),
                //        Movie = movie
                //    };
                //    context.ActorMovies.Update(actorMovie);
                //}
                //context.SaveChanges();

                return RedirectToAction("MovieList", "Movie");
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
                return RedirectToAction("MovieList", "Movie");
            }
            return View();
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

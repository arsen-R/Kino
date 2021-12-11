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
using KinoSite.Data;
using KinoSite.Models.Comments;
using KinoSite.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace KinoSite.Controllers
{
    public class TvSeriesController : Controller
    {
        private ApplicationContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TvSeriesController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PlayMovie", new { Id = vm.MovieId });
            }
            if (User.Identity.IsAuthenticated)
            {

                var post = _context.Movies
                .Select(m => m)
                .Where(m => m.Id == vm.MovieId)
                .Include(m => m.MainComments)
                .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault();
                if (vm.Message != null)
                {
                    if (vm.MainCommentId == 0)
                    {
                        post.MainComments = post.MainComments ?? new List<MainComment>();
                        post.MainComments.Add(new MainComment
                        {
                            Message = vm.Message,
                            Created = DateTime.Now,
                            ApplicationUserId = _userManager.GetUserId(User),
                            UserName = User.Identity.Name
                        });
                        _context.Movies.Update(post);
                    }
                    else
                    {
                        var comment = new SubComment
                        {
                            MainCommentId = vm.MainCommentId,
                            Message = vm.Message,
                            Created = DateTime.Now,
                            ApplicationUserId = _userManager.GetUserId(User),
                            UserName = User.Identity.Name
                        };
                        _context.SubComments.Add(comment);
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction("PlayMovie", new { Id = vm.MovieId });
                }
            }

            return RedirectToAction("PlayMovie", new { Id = vm.MovieId });
        }
        [HttpGet]
        public IActionResult TvSeries(string searchString, int PageNumber = 1)
        {
            var movies = _context.Movies.Select(m => m);
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            ViewBag.TotalPages = Math.Ceiling(movies.Count() / 30.0);
            movies = movies
                .Skip((PageNumber - 1) * 30)
                .Take(30)
                .OrderByDescending(m => m.Id)
                .ThenByDescending(m => m.Id);
            //.Include(m => m.Category);
            return View(movies.ToList());
        }
        [HttpGet]
        public IActionResult PlaySeries(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var movie = _context.Movies
                 .Select(m => m)
                 .Where(m => m.Id == Id)
                 .Include(m => m.Directions)
                 .Include(m => m.Category)
                 .Include(m => m.MainComments).ThenInclude(m => m.SubComments)
                 .Include(m => m.GenreMovies).ThenInclude(m => m.Genre)
                 .Include(m => m.ActorMovies).ThenInclude(m => m.Actor)
                 .FirstOrDefault();
            return View(movie);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> SeriesList(string searchString, int PageNumber = 1)
        {
            var movies = _context.Movies.Select(m => m);
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            ViewBag.TotalPages = Math.Ceiling(movies.Count() / 15.0);
            movies = movies
                .Skip((PageNumber - 1) * 15)
                .Take(15)
                .Include(m => m.Category);
            return View(movies.ToList());
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GenreId = _context.Genres.Select(a => new SelectListItem
            {
                Text = a.NameGenre,
                Value = a.Id.ToString()
            }).ToList();

            ViewBag.ActorId = _context.Actors.Select(a => new SelectListItem
            {
                Text = a.FullName,
                Value = a.Id.ToString()
            }).ToList();

            ViewBag.DirectionId = _context.Directions.Select(d => new SelectListItem
            {
                Text = d.FullName,
                Value = d.Id.ToString()
            });
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, List<IFormFile> Image, List<int> genreId, List<int> actorId)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                movie.CategoryId = 2;

                int categoryId = movie.CategoryId;
                var category = _context.Categories.Select(c => c).Where(c => c.Id == categoryId).FirstOrDefault();
                movie.VideoLink += $"{category.NameCategory}/{movie.Video}";

                _context.Movies.Add(movie);
                _context.SaveChanges();

                foreach (var item in genreId)
                {
                    GenreMovie movieGenre = new GenreMovie()
                    {
                        GenreId = item,
                        MovieId = movie.Id,
                        Genre = _context.Genres.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    _context.GenreMovies.Add(movieGenre);
                    _context.SaveChanges();
                }

                foreach (var item in actorId)
                {
                    ActorMovie actorMovie = new ActorMovie()
                    {
                        ActorId = item,
                        MovieId = movie.Id,
                        Actor = _context.Actors.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    _context.ActorMovies.Add(actorMovie);
                    _context.SaveChanges();
                }
                return RedirectToAction("SeriesList", "TvSeries");
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
            ViewBag.GenreId = _context.Genres.Select(a => new SelectListItem
            {
                Text = a.NameGenre,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.ActorId = _context.Actors.Select(a => new SelectListItem
            {
                Text = a.FullName,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.DirectionId = _context.Directions.Select(d => new SelectListItem
            {
                Text = d.FullName,
                Value = d.Id.ToString()
            });

            ViewBag.Movies = Id;
            Movie movie = _context.Movies.Select(m => m).Where(m => m.Id == Id).First();
            return View(movie);
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie, List<IFormFile> Image, List<int> genreId, List<int> actorId)
        {
            if (ModelState.IsValid)
            {
                movie = await ActionWithImage(movie, Image);
                movie.CategoryId = 2;
                int categoryId = movie.CategoryId;
                var category = _context.Categories.Select(c => c).Where(c => c.Id == categoryId).FirstOrDefault();
                movie.VideoLink += $"{category.NameCategory}/{movie.Video}";

                _context.Movies.Update(movie);
                _context.SaveChanges();

                var genreMovies = _context.GenreMovies.Where(gm => gm.MovieId == movie.Id).ToList();
                _context.GenreMovies.RemoveRange(genreMovies);
                _context.SaveChanges();

                foreach (var item in genreId)
                {
                    GenreMovie movieGenre = new GenreMovie()
                    {
                        GenreId = item,
                        MovieId = movie.Id,
                        Genre = _context.Genres.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    _context.GenreMovies.Add(movieGenre);
                    _context.SaveChanges();
                }
               

                var actorMovies = _context.ActorMovies.Where(am => am.MovieId == movie.Id).ToList();
                _context.ActorMovies.RemoveRange(actorMovies);
                _context.SaveChanges();

                foreach (var item in actorId)
                {
                    ActorMovie actorMovie = new ActorMovie()
                    {
                        ActorId = item,
                        MovieId = movie.Id,
                        Actor = _context.Actors.Where(x => x.Id == item).FirstOrDefault(),
                        Movie = movie
                    };
                    _context.ActorMovies.Add(actorMovie);
                    _context.SaveChanges();
                }

                return RedirectToAction("SeriesList", "TvSeries");
            }
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Movie movie = _context.Movies.FirstOrDefault(m => m.Id == id);
                return View(movie);

            }
            return NotFound();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("SeriesList", "TvSeries");
        }

        private async Task<Movie> ActionWithImage(Movie details, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
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
            }

            return details;
        }
    }
}

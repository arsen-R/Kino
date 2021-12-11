using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models;
using KinoSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace KinoSite.Controllers
{
    public class GenreController : Controller
    {
        private ApplicationContext _context;
        public GenreController(ApplicationContext context)
        { 
            _context = context;
        }
        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult GenreList(string searchString)
        {
            var genre = _context.Genres.Select(g => g);

            if (!String.IsNullOrEmpty(searchString))
            {
                genre = genre.Where(s => (s.NameGenre.Contains(searchString)));
            }
            return View(genre.ToList());
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("GenreList", "Genre");
            }
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Genre genre = _context.Genres
                .Select(g => g)
                .Where(g => g.Id == Id)
                .FirstOrDefault();
            return View(genre);
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Update(genre);
                _context.SaveChanges();
                return RedirectToAction("GenreList", "Genre");
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
                Genre genre = _context.Genres.FirstOrDefault(m => m.Id == id);
                return View(genre);

            }
            return NotFound();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Genre genre = await _context.Genres.FirstOrDefaultAsync(m => m.Id == id);
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return RedirectToAction("GenreList", "Genre");
        }
    }
}

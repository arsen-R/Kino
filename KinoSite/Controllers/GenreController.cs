using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models;
using KinoSite.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using KinoSite.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace KinoSite.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class GenreController : Controller
    {
        private ApplicationContext _context;
        public GenreController(ApplicationContext context)
        { 
            _context = context;
        }
        public IActionResult GenreList(string searchString)
        {
            var genre = _context.Genres.Select(g => g);

            if (!String.IsNullOrEmpty(searchString))
            {
                genre = genre.Where(s => (s.NameGenre.Contains(searchString)));
            }
            return View(genre.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
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
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("GenreList", "Genre");
            }
            ViewBag.GenreId = Id;
            Genre genre = _context.Genres
                .Select(g => g)
                .Where(g => g.Id == Id)
                .FirstOrDefault();
            return View(genre);
        }
        [HttpPost]
        public IActionResult Delete(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return RedirectToAction("GenreList", "Genre");
            }
            return View();
        }
    }
}

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
        MovieContext context;
        public GenreController(MovieContext context)
        {
            this.context = context;
        }
        public IActionResult GenreList(string searchString)
        {
            var genre = context.Genres.Select(g => g);

            if (!String.IsNullOrEmpty(searchString))
            {
                genre = genre.Where(s => (s.NameGenre.Contains(searchString)));
            }
            return View(genre.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            //List<SelectListItem> selectListItems = context.Categories.Select(c => new SelectListItem
            //{
            //    Text = c.NameCategory,
            //    Value = c.Id.ToString()
            //}).ToList();
            //ViewBag.CategoryId = selectListItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                context.Genres.Add(genre);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Genre genre = context.Genres
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
                context.Genres.Update(genre);
                context.SaveChanges();
                return RedirectToAction("GenreList", "Genre");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("GenreList", "Genre");
            }
            ViewBag.GenreId = Id;
            Genre genre = context.Genres
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
                context.Genres.Remove(genre);
                context.SaveChanges();
                return RedirectToAction("GenreList", "Genre");
            }
            return View();
        }
    }
}

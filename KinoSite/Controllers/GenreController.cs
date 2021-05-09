using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models;
using KinoSite.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using KinoSite.Areas.Identity.Data;
namespace KinoSite.Controllers
{
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
    }
}

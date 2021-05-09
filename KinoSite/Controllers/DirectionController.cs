using KinoSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models;
using KinoSite.Areas.Identity.Data;
namespace KinoSite.Controllers
{
    public class DirectionController : Controller
    {
        MovieContext context;
        public DirectionController(MovieContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> DirectionList(string searchString)
        {
            var direction = context.Directions.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {
                direction = direction.Where(s => (s.SurnameDirection.Contains(searchString)) || (s.NameDirection.Contains(searchString)));
            }
            return View(direction.ToList());
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(Direction direction)
        {
            if (ModelState.IsValid)
            {
                context.Directions.Add(direction);
                context.SaveChanges();
                return RedirectToAction("DirectionList", "Direction");
            }
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Direction = Id;
            Direction direction = context.Directions.Select(m => m).Where(m => m.Id == Id).First();
            return View(direction);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Direction direction)
        {
            if (ModelState.IsValid)
            {
                context.Directions.Update(direction);
                context.SaveChanges();
                return RedirectToAction("DirectionList", "Direction");
            }
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("DirectionList", "Direction");
            }
            ViewBag.Direction = id;
            Direction direction = context.Directions.Select(m => m).Where(m => m.Id == id).First();
            return View(direction);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Delete(Direction direction)
        {
            if (ModelState.IsValid)
            {
                context.Directions.Remove(direction);
                context.SaveChanges();
                return RedirectToAction("DirectionList", "Direction");
            }
            return View();
        }
    }
}

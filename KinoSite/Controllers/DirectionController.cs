using KinoSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models;
using KinoSite.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace KinoSite.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class DirectionController : Controller
    {
        MovieContext context;
        public DirectionController(MovieContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DirectionList(string searchString)
        {
            var direction = context.Directions.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {
                direction = direction.Where(s => (s.FullName.Contains(searchString)));
            }
            return View(direction.ToList());
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var direction = context.Directions
                    .Select(d => d)
                    .Where(d => d.Id == Id)
                    .Include(d => d.Movie)
                    .FirstOrDefault();
            return View(direction);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Direction direction, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                direction = await ActionWithImage(direction, Image);
                context.Directions.Add(direction);
                context.SaveChanges();
                return RedirectToAction("DirectionList", "Direction");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            ViewBag.Direction = Id;
            Direction direction = context.Directions.Select(m => m).Where(m => m.Id == Id).First();
            return View(direction);
        }
      
        [HttpPost]
        public async Task<IActionResult> Edit(Direction direction, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                direction = await ActionWithImage(direction, Image);
                context.Directions.Update(direction);
                context.SaveChanges();
                return RedirectToAction("DirectionList", "Direction");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Direction = id;
            Direction direction = context.Directions.Select(m => m).Where(m => m.Id == id).First();
            return View(direction);
        }
       
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

        private async Task<Direction> ActionWithImage(Direction details, List<IFormFile> Image)
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

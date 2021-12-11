using KinoSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace KinoSite.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class DirectionController : Controller
    {
        private ApplicationContext _context;
        public DirectionController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DirectionList(string searchString, int PageNumber = 1)
        {
            var direction = _context.Directions.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {
                direction = direction.Where(s => (s.FullName.Contains(searchString)));
            }

            ViewBag.TotalPages = Math.Ceiling(direction.Count() / 15.0);
            direction = direction.Skip((PageNumber - 1) * 15).Take(15);

            return View(direction.ToList());
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var direction = _context.Directions
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
                _context.Directions.Add(direction);
                _context.SaveChanges();
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
            Direction direction = _context.Directions.Select(m => m).Where(m => m.Id == Id).First();
            return View(direction);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Direction direction, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                direction = await ActionWithImage(direction, Image);
                _context.Directions.Update(direction);
                _context.SaveChanges();
                return RedirectToAction("DirectionList", "Direction");
            }
            return View();
        }
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Direction direction = _context.Directions.FirstOrDefault(m => m.Id == id);
                return View(direction);

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Direction direction = await _context.Directions.FirstOrDefaultAsync(m => m.Id == id);
            _context.Directions.Remove(direction);
            _context.SaveChanges();
            return RedirectToAction("DirectionList", "Direction");
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

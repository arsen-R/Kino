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
    public class ActorController : Controller
    {
        private ApplicationContext _context;
        public ActorController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> ActorList(string searchString, int PageNumber = 1)
        {
            var actor = _context.Actors.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {
                actor = actor.Where(s => (s.FullName.Contains(searchString)));
            }
            ViewBag.TotalPages = Math.Ceiling(actor.Count() / 15.0);
            actor = actor.Skip((PageNumber - 1) * 15).Take(15);
            return View(actor.ToList());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Actor actor = _context.Actors
                .Select(a => a)
                .Where(a => a.Id == Id)
                //.Include(a => a.ActorMovies)
                //.ThenInclude(m => m.Movie)
                .FirstOrDefault();
            return View(actor);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Actor actor, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                actor = await ActionWithImage(actor, Image);
                _context.Actors.Add(actor);
                _context.SaveChanges();
                return RedirectToAction("ActorList", "Actor");
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
            ViewBag.Actor = Id;
            Actor actor = _context.Actors.Select(m => m).Where(m => m.Id == Id).First();
            return View(actor);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                actor = await ActionWithImage(actor, Image);
                _context.Actors.Update(actor);
                _context.SaveChanges();
                return RedirectToAction("ActorList", "Actor");
            }
            return View();
        }
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Actor actor = _context.Actors.FirstOrDefault(m => m.Id == id);
                return View(actor);

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
           Actor actor = await _context.Actors.FirstOrDefaultAsync(m => m.Id == id);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return RedirectToAction("ActorList", "Actor");
        }
        private async Task<Actor> ActionWithImage(Actor details, List<IFormFile> Image)
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


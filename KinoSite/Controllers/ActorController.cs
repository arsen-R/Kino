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
    public class ActorController : Controller
    {
        MovieContext context;
        public ActorController(MovieContext context)
        {
            this.context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> ActorList(string searchString)
        {
            var actor = context.Actors.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {
                actor = actor.Where(s => (s.FullName.Contains(searchString)));
            }
            return View(actor.ToList());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Actor actor = context.Actors
                .Select(a => a)
                .Where(a => a.Id == Id)
                .Include(a => a.ActorMovies)
                .ThenInclude(m => m.Movie)
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
                context.Actors.Add(actor);
                context.SaveChanges();
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
            Actor actor = context.Actors.Select(m => m).Where(m => m.Id == Id).First();
            return View(actor);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                actor = await ActionWithImage(actor, Image);
                context.Actors.Update(actor);
                context.SaveChanges();
                return RedirectToAction("ActorList", "Actor");
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
            ViewBag.Actor = id;
            Actor actor = context.Actors.Select(m => m).Where(m => m.Id == id).First();
            return View(actor);
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(Actor actor)
        {
            if (ModelState.IsValid)
            {
                context.Actors.Remove(actor);
                context.SaveChanges();
                return RedirectToAction("ActorList", "Actor");
            }
            return View();
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


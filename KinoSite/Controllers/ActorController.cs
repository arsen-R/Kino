using KinoSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Models;

namespace KinoSite.Controllers
{
    public class ActorController : Controller
    {
        ApplicationContext context;
        public ActorController(ApplicationContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> ActorList(string searchString)
        {
            var actor = context.Actors.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {
                actor = actor.Where(s => (s.SurnameActor.Contains(searchString)) || (s.NameActor.Contains(searchString)));
            }
            return View(actor.ToList());
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                context.Actors.Add(actor);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
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
            ViewBag.Actor = Id;
            Actor actor = context.Actors.Select(m => m).Where(m => m.Id == Id).First();
            return View(actor);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor)
        {
            if (ModelState.IsValid)
            {
                context.Actors.Update(actor);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Actor = id;
            Actor actor = context.Actors.Select(m => m).Where(m => m.Id == id).First();
            return View(actor);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> Delete(Actor actor)
        {
            if (ModelState.IsValid)
            {
                context.Actors.Remove(actor);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}

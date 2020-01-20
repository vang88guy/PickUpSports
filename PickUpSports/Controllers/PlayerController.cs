using Microsoft.AspNet.Identity;
using PickUpSports.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickUpSports.Controllers
{
    public class PlayerController : Controller
    {
     
        public class PlayersController : Controller
        {
            ApplicationDbContext db = new ApplicationDbContext();

            // GET: Players
            public ActionResult Index()
            {
                var players = db.Player.Include(p => p.ApplicationUser).ToList();
                return View(players);
            }

            // GET: Players/Details/5
            public ActionResult Details(int id)
            {
                var player = db.Player.Include(p => p.ApplicationUser).FirstOrDefault(p => p.Id == id);
                return View(player);
            }

            // GET: Players/Create
            public ActionResult Create()
            {
                Player player = new Player();
                return View(player);
            }

            // POST: Players/Create
            [HttpPost]
            public ActionResult Create(Player player)
            {
                try
                {
                    // TODO: Add insert logic here
                    player.ApplicationId = User.Identity.GetUserId();
                    db.Player.Add(player);
                    db.SaveChanges();

                    return RedirectToAction("LogOut", "Account");
                }
                catch
                {
                    return View();
                }
            }

            // GET: Players/Edit/5
            public ActionResult Edit(int id)
            {
                return View();
            }

            // POST: Players/Edit/5
            [HttpPost]
            public ActionResult Edit(int id, FormCollection collection)
            {
                try
                {
                    // TODO: Add update logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            // GET: Players/Delete/5
            public ActionResult Delete(int id)
            {
                return View();
            }

            // POST: Players/Delete/5
            [HttpPost]
            public ActionResult Delete(int id, FormCollection collection)
            {
                try
                {
                    // TODO: Add delete logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }
    }
}
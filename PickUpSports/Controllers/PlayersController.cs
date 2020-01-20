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
        public ActionResult Details()
        {
            var userid = User.Identity.GetUserId();
            var player = db.Player.Include(p => p.ApplicationUser).FirstOrDefault(p => p.ApplicationId == userid);
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
            var player = db.Player.Include(p => p.ApplicationUser).Where(p => p.PlayerId == id).FirstOrDefault();
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Player player)
        {
            try
            {
                // TODO: Add update logic here
                var playeredit = db.Player.Where(p => p.PlayerId == id).FirstOrDefault();
                playeredit.FirstName = player.FirstName;
                playeredit.LastName = player.LastName;
                playeredit.PhoneNumber = player.PhoneNumber;
                playeredit.SkillLevel = player.SkillLevel;
                playeredit.SportsInterest = player.SportsInterest;
                playeredit.ApplicationUser.UserName = player.ApplicationUser.UserName;
                playeredit.ApplicationUser.Email = player.ApplicationUser.Email;
                db.SaveChanges();
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
            var player = db.Player.Include(p => p.ApplicationUser).Where(p => p.PlayerId == id).FirstOrDefault();
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Player player)
        {
            try
            {
                // TODO: Add delete logic here
                player = db.Player.Include(p => p.ApplicationUser).Where(p => p.PlayerId == id).FirstOrDefault();               
                var userdelete = db.Users.SingleOrDefault(c => c.Id == player.ApplicationId);
                player.ApplicationId = null;
                db.Player.Remove(player);
                db.Users.Remove(userdelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

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
    public class EventsController : Controller
    {
        ApplicationDbContext db;
        public EventsController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Events
        public ActionResult Index()
        {           
            var events = db.Event.Include(e => e.Player);
            return View(events);
        }

        // Get Events that Player is interested in
        public ActionResult PlayerInterestEvents() 
        {
            string datenow = System.DateTime.Now.ToString("MM/dd/yyyy");
            var userid = User.Identity.GetUserId();
            var player = db.Player.Include(s => s.ApplicationUser).Where(p => p.ApplicationId == userid).FirstOrDefault();            
            var events = db.Event.Include(e => e.Player).Where(e => e.SportsName == player.SportsInterest && e.ZipCode == player.ZipCode && e.SkillLevel == player.SkillLevel && e.DateOfEvent == datenow).FirstOrDefault();
            return View(events);
        }

        //Join Event
        public ActionResult JoinEvent(int id) 
        {

            return View();
        }
        [HttpPost]
        public ActionResult JoinEvent(int id, Event joinevent)
        {

            return View();
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            var eventone = db.Event.Include(e => e.Player).FirstOrDefault(e=>e.EventId == id);
            return View(eventone);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            Event eventcreate = new Event();
            var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
            ViewBag.SkillLevel = new SelectList(skilllevel);
            var SportName = db.Sport.Select(s => s.SportName).ToList();
            ViewBag.SportName = new SelectList(SportName);
            return View(eventcreate);
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(Event eventone)
        {
            try
            {
                // TODO: Add insert logic here
                var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
                ViewBag.SkillLevel = new SelectList(skilllevel);
                var SportName = db.Sport.Select(s => s.SportName).ToList();
                ViewBag.SportName = new SelectList(SportName);
                var userid = User.Identity.GetUserId();
                int playerrating = db.Player.Include(p => p.ApplicationUser).Where(p => p.ApplicationId == userid).Select(p => p.PlayerRating).FirstOrDefault();
                eventone.PlayerRating = playerrating;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            var eventedit = db.Event.Include(e => e.Player).FirstOrDefault(e => e.EventId == id);
            var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
            ViewBag.SkillLevel = new SelectList(skilllevel);
            var SportName = db.Sport.Select(s => s.SportName).ToList();
            ViewBag.SportName = new SelectList(SportName);
            return View(eventedit);
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event eventedit)
        {
            try
            {
                // TODO: Add update logic here
                var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
                ViewBag.SkillLevel = new SelectList(skilllevel);
                var SportName = db.Sport.Select(s => s.SportName).ToList();
                ViewBag.SportName = new SelectList(SportName);
                var eventoriginal = db.Event.Include(e => e.Player).FirstOrDefault(e => e.EventId == id);
                eventoriginal.EventName = eventedit.EventName;
                eventoriginal.SportsName = eventedit.SportsName;
                eventoriginal.SkillLevel = eventedit.SkillLevel;
                eventoriginal.IsFull = eventedit.IsFull;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            var eventdelete = db.Event.Include(e => e.Player).FirstOrDefault(e => e.EventId == id);
            return View(eventdelete);
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event eventdelete)
        {
            try
            {
                // TODO: Add delete logic here
                eventdelete = db.Event.Include(e => e.Player).FirstOrDefault(e => e.EventId == id);
                eventdelete.PlayerId = 0;
                db.Event.Remove(eventdelete);
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

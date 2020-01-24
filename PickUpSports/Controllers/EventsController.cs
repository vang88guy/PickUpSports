using Microsoft.AspNet.Identity;
using PickUpSports.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using static PickUpSports.Models.TwilioAPIKey;
using System.Windows;

namespace PickUpSports.Controllers
{
    public class EventsController : Controller
    
    {
        ApplicationDbContext db;
        SMSController SMS;
        public EventsController()
        {
            SMS = new SMSController();
            db = new ApplicationDbContext();
        }
        
        // GET: Events
        public ActionResult Index()
        {           
            var events = db.Event.Include(e => e.Player).Include(e=>e.Player.ApplicationUser).ToList();
            return View(events);
        }

        // Get Events that Player is interested in
        public ActionResult PlayerInterestEvents() 
        {
            try
            {
                string datenow = System.DateTime.Now.ToString("MM/dd/yyyy");
                var userid = GetAppId();
                var player = GetPlayerByAppId(userid);
                //var events = db.Event.Include(e => e.Player).Include(e => e.Player.ApplicationUser).Where(e => e.SportsName == player.SportsInterest && e.ZipCode == player.ZipCode && e.SkillLevel == player.SkillLevel && e.DateOfEvent == datenow && e.IsFull == false && e.PlayerId != player.PlayerId).ToList();
                var eventsnow = db.PlayerEvent.Include(e=>e.Event).Include(e=>e.Player).Include(e=>e.Player.ApplicationUser).Where(e=>e.PlayerId != player.PlayerId).Select(e=>e.Event).Where(e => e.SportsName == player.SportsInterest && e.ZipCode == player.ZipCode && e.SkillLevel == player.SkillLevel && e.DateOfEvent == datenow && e.IsFull == false && e.PlayerId != player.PlayerId).ToList();
                return View(eventsnow);
            }
            catch (Exception)
            {
                return View();
            }           
        }       

        //Get Events that player created

        public ActionResult MyEvents() 
        {
            var userid = GetAppId();
            var player = GetPlayerByAppId(userid);
            ViewBag.UserId = player.PlayerId;
            try
            {                
                var eventsjoined = db.PlayerEvent.Include(e => e.Player).Include(e => e.Event).Where(e => e.PlayerId == player.PlayerId && e.Event.PlayerId != player.PlayerId).Select(e => e.Event).ToList();
                var myevents = db.Event.Include(e => e.Player).Include(e => e.Player.ApplicationUser).Where(e => e.PlayerId == player.PlayerId).ToList();
                var allevents = myevents.Concat(eventsjoined);
                return View(allevents.ToList());
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        //Join Event
        public ActionResult JoinEvent(int id) 
        {
            var eventjoin = GetEventById(id);
            return View(eventjoin);
        }
        [HttpPost]
        public ActionResult JoinEvent(int id, Event joinevent)
        {
            try
            {
                var userid = GetAppId();
                var player = GetPlayerByAppId(userid);
                var eventnow = GetEventById(id);
                if (eventnow.IsFull == false && eventnow.CurrentPlayers != eventnow.MaximumPlayers)
                {
                    eventnow.CurrentPlayers += 1;
                    if (eventnow.CurrentPlayers == eventnow.MaximumPlayers)
                    {
                        eventnow.IsFull = true;
                    }
                    PlayerEvent playerEvent = new PlayerEvent();
                    playerEvent.PlayerId = player.PlayerId;
                    playerEvent.EventId = eventnow.EventId;
                    db.PlayerEvent.Add(playerEvent);
                    db.SaveChanges();
                    return RedirectToAction( "PlayerInerestEvents", "Event");
                }
                else
                {
                    MessageBox.Show("Sorry, Event is full");
                    return RedirectToAction("PlayerInerestEvents", "Event");
                }               
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            var eventone = GetEventById(id);
            return View(eventone);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            Event eventcreate = new Event();
            var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
            ViewBag.SkillLevel = new SelectList(skilllevel);
            var SportsName = db.Sport.Select(s => s.SportName).ToList();
            ViewBag.SportsName = new SelectList(SportsName);
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
                var SportsName = db.Sport.Select(s => s.SportName).ToList();
                ViewBag.SportsName = new SelectList(SportsName);
                var userid = GetAppId();
                var player = GetPlayerByAppId(userid);
                eventone.PlayerRating = player.PlayerRating;
                eventone.PlayerId = player.PlayerId;
                string dateofevent = GetDateFormat(eventone.DateOfEvent);
                string timeofevent = GetTimeFormat(eventone.TimeOfEvent);
                eventone.TimeOfEvent = timeofevent;
                eventone.DateOfEvent = dateofevent;
                db.Event.Add(eventone);
                PhoneNumbers.PlayersPhoneNumbers = db.Player.Include(p => p.ApplicationUser).Where(p => p.SportsInterest == eventone.SportsName && p.PlayerId != eventone.PlayerId).Select(p => p.PhoneNumber).ToList();
                db.SaveChanges();
                var eventid = db.Event.Include(e => e.Player).Include(e => e.Player.ApplicationUser).Where(e => e.EventName == eventone.EventName).Select(e => e.EventId).FirstOrDefault();
                PlayerEvent playerEvent = new PlayerEvent();
                playerEvent.PlayerId = player.PlayerId;
                playerEvent.EventId = eventid;
                db.PlayerEvent.Add(playerEvent);
                db.SaveChanges();
                SMS.SendSMSToPlayers();

                return RedirectToAction("Details","Players");
            }
            catch
            {
                Redirect("https://localhost:44357/sms/sendsms");
                return RedirectToAction("Details", "Players");
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            var eventedit = GetEventById(id);
            var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
            ViewBag.SkillLevel = new SelectList(skilllevel);
            var SportsName = db.Sport.Select(s => s.SportName).ToList();
            ViewBag.SportsName = new SelectList(SportsName);
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
                var SportsName = db.Sport.Select(s => s.SportName).ToList();
                ViewBag.SportsName = new SelectList(SportsName);
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
            var eventdelete = GetEventById(id);
            return View(eventdelete);
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event eventdelete)
        {
            try
            {
                // TODO: Add delete logic here
                eventdelete = GetEventById(id);
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

        public string GetAppId()
        {
            var userid = User.Identity.GetUserId();
            return userid;
        }

        public Player GetPlayerByAppId(string userid)
        {
            var player = db.Player.Include(s => s.ApplicationUser).Where(p => p.ApplicationId == userid).FirstOrDefault();
            return player;
        }
        public Event GetEventById(int id) 
        {
            var eventnow = db.Event.Include(e => e.Player).Include(e=>e.Player.ApplicationUser).FirstOrDefault(e => e.EventId == id);
            return eventnow;
        }
        public string GetDateFormat(string date) 
        {
            DateTime dateformat = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateofevent = dateformat.ToString("MM/dd/yyyy");
            return dateofevent;
        }
        public string GetTimeFormat(string time) 
        {           
            DateTime timeformat = DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture);
            string timeofevent = timeformat.ToString("h:mm tt");
            return timeofevent;
        }
    }
}

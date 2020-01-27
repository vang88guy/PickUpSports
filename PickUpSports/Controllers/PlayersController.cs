using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PickUpSports.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PickUpSports.Controllers
{
    public class PlayersController : Controller
    {
        ApplicationDbContext db;

        public PlayersController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Players
        public ActionResult Index()
        {
            var userId = GetAppId();
            var player = GetPlayerByAppId(userId);
            return View(player);
        }

        // GET: Players/Details/5
        public ActionResult Details()
        {
            var userid = GetAppId();
            var player = GetPlayerByAppId(userid);
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            Player player = new Player();
            var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
            ViewBag.SkillLevel = new SelectList(skilllevel);
            var SportsInterest = db.Sport.Select(s => s.SportName).ToList();
            ViewBag.SportsInterest = new SelectList(SportsInterest);
            return View(player);
        }

        // POST: Players/Create
        [HttpPost]
        public ActionResult Create(Player player)
        {
            try
            {
                // TODO: Add insert logic here

                player.ApplicationId = GetAppId();

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
            var player = GetPlayerByPlayerId(id);
            var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
            ViewBag.SkillLevel = new SelectList(skilllevel);
            var SportsInterest = db.Sport.Select(s => s.SportName).ToList();
            ViewBag.SportsInterest = new SelectList(SportsInterest);
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Player player)
        {

            try
            {
                // TODO: Add update logic here
                var SportsInterest = db.Sport.Select(s => s.SportName).ToList();
                ViewBag.SportsInterest = new SelectList(SportsInterest);
                var skilllevel = db.SkillLevel.Select(s => s.Level).ToList();
                ViewBag.SkillLevel = new SelectList(skilllevel);
                var playeredit = GetPlayerByPlayerId(id);
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

        public async System.Threading.Tasks.Task<ActionResult> ParkLocationsAsync()
        {
            var id = GetAppId();
            Player player = GetPlayerByAppId(id);
            List<Park> parks = new List<Park>();
            string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=park+in+" + player.ZipCode + "&key=" + GooglePlacesKey.Key;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                PlacesJSONResult postManJSON = JsonConvert.DeserializeObject<PlacesJSONResult>(jsonResult);
                foreach (var result in postManJSON.results)
                {
                    Park park = new Park();
                    park.Latitude = result.geometry.location.lat;
                    park.Longitude = result.geometry.location.lng;
                    park.ParkName = result.name;
                    parks.Add(park);
                }
                return View(parks);
            }
            return View();
        }  
        // GET: Players/Delete/5
        public ActionResult Delete(int id)
        {
            var player = GetPlayerByPlayerId(id);

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Player player)
        {
            try
            {
                // TODO: Add delete logic here
                player = GetPlayerByPlayerId(id);
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

        public ActionResult RatePlayer()
        {
            var PlayerRating = db.SkillLevel.Select(s => s.Level).ToList();
            ViewBag.PlayerRating = new SelectList(PlayerRating);
            var userid = GetAppId();
            var player = GetPlayerByAppId(userid);
            string yesterday = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
            var eventscompleted = db.Event.Include(e => e.Player).Where(e => e.DateOfEvent == yesterday).ToList();
            List<Player> players = new List<Player>();

            foreach (var item in eventscompleted)
            {
                var playersforevent = db.PlayerEvent.Include(p => p.Player).Include(p => p.Event).Where(p => p.EventId == item.EventId && p.PlayerId != player.PlayerId).Select(p => p.Player).ToList();
                players.Concat(playersforevent);
            }
            if (players.Count == 0)
            {
                return RedirectToAction("PlayerInterestEvents", "Events");
            }
            else
            {
                return View(players);
            }

        }
        [HttpPost]
        public ActionResult RatePlayer(List<Player> players)
        {
            try
            {
                foreach (var item in players)
                {
                    var player = db.Player.Include(p => p.ApplicationUser).Where(p => p.PlayerId == item.PlayerId).FirstOrDefault();
                    if (player.PlayerRating == 0)
                    {
                        player.PlayerRating += item.PlayerRating;
                        db.SaveChanges();
                    }
                    else
                    {
                        player.PlayerRating = Math.Round(((player.PlayerRating + item.PlayerRating) / 2), 2);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("PlayerInterestEvents", "Events");
            }
            catch (Exception)
            {

                return RedirectToAction("PlayerInterestEvents", "Events");
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
        public Player GetPlayerByPlayerId(int id)
        {
            var player = db.Player.Include(p => p.ApplicationUser).Where(p => p.PlayerId == id).FirstOrDefault();
            return player;
        }

    }
}

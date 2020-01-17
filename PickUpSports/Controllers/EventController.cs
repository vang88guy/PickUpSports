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
    public class EventController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        // GET: Event
        public class EmployeesController : Controller
        {
            private ApplicationDbContext db = new ApplicationDbContext();

            // GET: Players
            public ActionResult Index()
            {
                string userId = User.Identity.GetUserId();
                Player player = db.Player.Include(e=>e.ApplicationUser).Where(e => e.ApplicationId == userId).SingleOrDefault();
                var today = Convert.ToString(DateTime.Now.DayOfWeek);
                var todayDate = Convert.ToString(DateTime.Now.Date);
                PlayerHomeViewModel viewModel = new PlayerHomeViewModel();
                viewModel.Player = db.Player.Include(e => e.ApplicationUser).Where(c => c.ZipCode == player.ZipCode).FirstOrDefault();
                viewModel.SportsInterest = new SelectList(new List<string>() { "Basketball", "Football", "Soccer", "Tennis", "Hockey" });
                return View(viewModel);

            }
            [HttpPost]

            public ActionResult Index(PlayerHomeViewModel playerView)
            {
                string userId = User.Identity.GetUserId();
                Player player = db.Player.Where(e => e.ApplicationId == userId).SingleOrDefault();
                PlayerHomeViewModel viewModel = new PlayerHomeViewModel();
                viewModel.Player = db.Users.Include(e => e.ApplicationUser).Where(c => c.ZipCode == player.ZipCode && c.SportsInterest == playerView.SelectedSport).ToList();
                viewModel.SportsInterest = new SelectList(new List<string>() { "Basketball", "Football", "Soccer", "Baseball", "Tennis", "Hockey", "Saturday" });
                return View(viewModel);

            }

           

            // GET: Employees/Details/5
            public ActionResult Details(int? id)
            {
                //if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                //Employee employee = db.Employees.Find(id);
                //if (employee == null)
                //{
                //    return HttpNotFound();
                //}
                //return View(employee);

                Player player = null;
                if (id == null)
                {
                    var FoundUserId = User.Identity.GetUserId();
                    player = db.Player.Where(c => c.ApplicationId == FoundUserId).FirstOrDefault();
                    return View();
                }

                else
                {
                    player = db.Player.Find(id);
                }
                if (player == null)
                {
                    return HttpNotFound();
                }
                return View(player);


            }

            // GET: Employees/Create
            public ActionResult Create()
            {
                ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email");
                return View();
            }

            // POST: Employees/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Id,ApplicationId,firstName,lastName,zipCode")] )
            {
                if (ModelState.IsValid)
                {
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", .ApplicationId);
                return View();
            }

            // GET: Employees/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    
                }
                
            }

            // POST: Employees/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,Name,Zip,Address,PickupStartDate,PickupEndDate,DayOfWeek,ApplicationUserId")] Customer customer)
            {
               
                if (ModelState.IsValid)
                {
                    

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserRole", customer.Id);
                return View(customer);
            }





            // GET: Employees/Delete/5
            public ActionResult Delete(int? id)
            {
                
            }

            // POST: Employees/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                
                return RedirectToAction("Index");
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }



            public ActionResult ConfirmPickUp(int? id)
            {
                

                return RedirectToAction("Index");
            }
        }
    }
}
}
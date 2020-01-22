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
        public ApplicationDbContext db;
        // GET: Event
        public EventController()
        {
            db = new ApplicationDbContext();
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
        public ActionResult Create([Bind(Include = "Id,ApplicationId,firstName,lastName,zipCode")] Player player)
        {
            if (ModelState.IsValid)
            {
                    
                db.SaveChanges();
                return RedirectToAction("Index");
            }

<<<<<<< HEAD
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", player.ApplicationId);
            return View();
        }

        // GET: Employees/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
=======
            // GET: Plyayers/Edit/5
            //public ActionResult Edit(int? id)
            //{
            //    if (id == null)
            //    {
>>>>>>> 30c5260ffdb8dfbec421e2a53026cfcdf0193e31
                    
        //    }
                
<<<<<<< HEAD
        //}

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Zip,Address,PickupStartDate,PickupEndDate,DayOfWeek,ApplicationUserId")] Player player)
        {
=======
            //}

            // POST: Players/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "PlayerId,Name,Zip,Address,SportsInterest,ApplicationUserId")] Player player)
            {
>>>>>>> 30c5260ffdb8dfbec421e2a53026cfcdf0193e31
               
            if (ModelState.IsValid)
            {
                    

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "PlayerId", "UserRole", player.PlayerId);
            return View(player);
        }




<<<<<<< HEAD

        // GET: Employees/Delete/5
        //public ActionResult Delete(int? id)
        //{
=======
            // GET: Players/Delete/5
            //public ActionResult Delete(int? id)
            //{
>>>>>>> 30c5260ffdb8dfbec421e2a53026cfcdf0193e31
                
        //}

<<<<<<< HEAD
        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
=======
            // POST: Players/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
>>>>>>> 30c5260ffdb8dfbec421e2a53026cfcdf0193e31
                
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
                

<<<<<<< HEAD
            return RedirectToAction("Index");
        }
=======
                return RedirectToAction("Index");
            }
        
>>>>>>> 30c5260ffdb8dfbec421e2a53026cfcdf0193e31
    }
}

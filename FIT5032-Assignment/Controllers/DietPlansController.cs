using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment.Controllers
{
    public class DietPlansController : Controller
    {
        private HealthPro db = new HealthPro();

        // GET: DietPlans but only ones that matches this user's id
        [Authorize(Roles = "Doctor")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var diets = db.DietPlans.Where(d => d.User_Id ==
            userId).ToList();
            return View(diets);
        }

        //patients can view all the diet plans from all the doctors
        [Authorize(Roles = "Patient")]
        public ActionResult View_Diet_Plans()
        {
            var diets = db.DietPlans.ToList();
            return View(diets);
        }

        // GET: DietPlans/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietPlan dietPlan = db.DietPlans.Find(id);
            if (dietPlan == null)
            {
                return HttpNotFound();
            }
            return View(dietPlan);
        }

        // GET: DietPlans/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DietPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //Ratings and date added are automatically selected for the doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public ActionResult Create([Bind(Include = "Id,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Date_added,User_Id,Rating, RatingNum")] DietPlan dietPlan)
        {
            dietPlan.User_Id = User.Identity.GetUserId();
            DateTime now = DateTime.Now;
            dietPlan.Date_added = now;
            dietPlan.Rating = 0;
            dietPlan.RatingNum = 0;
            ModelState.Clear();
            TryValidateModel(dietPlan);
            if (ModelState.IsValid)
            {
                db.DietPlans.Add(dietPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dietPlan);
        }

        // GET: DietPlans/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietPlan dietPlan = db.DietPlans.Find(id);
            if (dietPlan == null)
            {
                return HttpNotFound();
            }
            return View(dietPlan);
        }

        // GET: DietPlans/EditRating/5
        

        // POST: DietPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Date_added,User_Id,Rating,RatingNum")] DietPlan dietPlan)
        {
            var dateAdded = dietPlan.Date_added;
            var userID = dietPlan.User_Id;
            if (ModelState.IsValid)
            {
                db.Entry(dietPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dietPlan);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult EditRating(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietPlan dietPlan = db.DietPlans.Find(id);
            
            if (dietPlan == null)
            {
                return HttpNotFound();
            }
            return View(dietPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRating([Bind(Include = "Id,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Date_added,User_Id,Rating,RatingNum")] DietPlan dietPlan, int Rating)
        {
            //DietPlan dp1 = db.DietPlans.Find(dietPlan.Id).AsNoTracking(); // fetched from DB
            //get the diet plan instance being rated
            var dp1 = db.DietPlans.AsNoTracking().Where(d => d.Id == dietPlan.Id).ToList();
            //get the dietplan object
            var dietPlan1 = dp1[0];
            //get the current rating
            var currRating = dietPlan1.Rating;
            //get the total rating count
            var currCount = dietPlan1.RatingNum;
            //calculate the rating times count
            var total = currRating * currCount;
            //calculate the new total rating
            total = total + dietPlan.Rating;
            //increase the current count
            currCount++;
            //calculate the new rating
            var newRating = total / currCount;
            //assign this new instance of rating
            dietPlan.RatingNum = currCount;
            //convert the rating to int
            dietPlan.Rating = (int) newRating;
            //add the instnace to the db for an updated rating
            if (ModelState.IsValid)
            {
                db.Entry(dietPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("View_Diet_Plans");
                
            }
            return View(dietPlan);
        }


        // GET: DietPlans/Delete/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietPlan dietPlan = db.DietPlans.Find(id);
            if (dietPlan == null)
            {
                return HttpNotFound();
            }
            return View(dietPlan);
        }

        // POST: DietPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DietPlan dietPlan = db.DietPlans.Find(id);
            db.DietPlans.Remove(dietPlan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Home()
        {
            return Redirect("~/Home/Home");
        }

        public ActionResult Contact()
        {

            return Redirect("~/Home/Contact");
        }

        public ActionResult About()
        {

            return Redirect("~/Home/About");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

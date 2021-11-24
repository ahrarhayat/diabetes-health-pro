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
    public class BloodSugarLevelsController : Controller
    {
        private BloodSugarLevelModels db = new BloodSugarLevelModels();

        // GET: BloodSugarLevels but only ones that matches the user id for this particular user
        [Authorize(Roles = "Patient")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var bloodsugars = db.BloodSugarLevels.Where(d => d.User_Id ==
            userId).ToList();
            return View(bloodsugars);
        }

        // GET: BloodSugarLevels/Details/5
        [Authorize(Roles = "Patient")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodSugarLevel bloodSugarLevel = db.BloodSugarLevels.Find(id);
            if (bloodSugarLevel == null)
            {
                return HttpNotFound();
            }
            return View(bloodSugarLevel);
        }

        // GET: BloodSugarLevels/Create
        [Authorize(Roles = "Patient")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BloodSugarLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BloodSugarLevel1,DateOfTest,User_Id")] BloodSugarLevel bloodSugarLevel)
        {

                DateTime now = DateTime.Now;
                bloodSugarLevel.DateOfTest = now;
                var id = User.Identity.GetUserId();
                bloodSugarLevel.User_Id = id;
                db.BloodSugarLevels.Add(bloodSugarLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            

           
        }

        // GET: BloodSugarLevels/Edit/5
        [Authorize(Roles = "Patient")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodSugarLevel bloodSugarLevel = db.BloodSugarLevels.Find(id);
            if (bloodSugarLevel == null)
            {
                return HttpNotFound();
            }
            return View(bloodSugarLevel);
        }

        // POST: BloodSugarLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BloodSugarLevel1,DateOfTest,User_Id")] BloodSugarLevel bloodSugarLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodSugarLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bloodSugarLevel);
        }

        // GET: BloodSugarLevels/Delete/5
        [Authorize(Roles = "Patient")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodSugarLevel bloodSugarLevel = db.BloodSugarLevels.Find(id);
            if (bloodSugarLevel == null)
            {
                return HttpNotFound();
            }
            return View(bloodSugarLevel);
        }

        // POST: BloodSugarLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodSugarLevel bloodSugarLevel = db.BloodSugarLevels.Find(id);
            db.BloodSugarLevels.Remove(bloodSugarLevel);
            db.SaveChanges();
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
    }
}

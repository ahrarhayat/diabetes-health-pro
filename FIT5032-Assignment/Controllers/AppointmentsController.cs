using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FIT5032_Assignment.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment.Controllers
{
    public class AppointmentsController : Controller
    {
        private AppointmentModels db = new AppointmentModels();
        private EmailModels db1 = new EmailModels();

        [Authorize]
        public ActionResult HomeLoggedIn()

        {
            return View();
        }

        // GET: Appointments
        [Authorize(Roles = "Administrator,Patient,Doctor")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var appointments = db.Appointments.Where(d => d.User_Id ==
            userId && d.EndTime > DateTime.Now).ToList();

            return View(appointments);
        }
        [Authorize(Roles = "Administrator,Patient")]
        public ActionResult ClashView()
        {
            var userId = User.Identity.GetUserId();
            var appointments = db.Appointments.Where(d => d.User_Id ==
            userId && d.EndTime > DateTime.Now).ToList();

            return View(appointments);
        }
        [Authorize(Roles = "Administrator,Patient")]
        public ActionResult NoDoctorView()
        {
            var userId = User.Identity.GetUserId();
            var appointments = db.Appointments.Where(d => d.User_Id ==
            userId && d.EndTime > DateTime.Now).ToList();

            return View(appointments);
        }

        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult DoctorAppointments()
        {
            var userId = User.Identity.GetUserId();
            var appointments = db.Appointments.Where(d => d.DoctorID ==
            userId && d.EndTime > DateTime.Now).ToList();

            return View(appointments);
        }

        // GET: Appointments/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Administrator,Patient")]
        public ActionResult Create()
        {
            var list1 = new List<string>() { "Prahran Clinic", "Clayton Clinic", "Melbourne Clinic", "Caufield Clinic"};
            var list2 = new List<string>() { "Diet Recommendation", "Diabetes Checkup", "Other"};
            ViewBag.clinicList = list1;
            ViewBag.typeList = list2;
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Type,Description,DoctorID,Clinic,StartTime,EndTime,User_Id")] Appointment appointment)
        {
            //get the user id that is logged in
            appointment.User_Id = User.Identity.GetUserId();
            Debug.WriteLine("User ID " + appointment.User_Id);
            //extract the date chosen by the user
            DateTime StartTime = appointment.StartTime;
            int month = StartTime.Month;
            int day = StartTime.Day;
            int year = StartTime.Year;
            Debug.WriteLine("Year " + StartTime.Year);
            Debug.WriteLine("Day " + StartTime.Day);
            Debug.WriteLine("Month " + StartTime.Month);
            //start the hour from 12pm
            int hour = 12;
            int minutes = 0;
            //start trying the time from 12-2pm
            DateTime date1 = new DateTime(year, month, day, hour, minutes, 0);
            //then try from 2-4pm
            DateTime date2 = new DateTime(year, month, day, hour+2, minutes, 0);
            //then try 4-6pm
            DateTime date3 = new DateTime(year, month, day, hour+4, minutes, 0);
            appointment.StartTime = date1;
            appointment.EndTime = appointment.StartTime.AddHours(2);
            Debug.WriteLine("Start time " + appointment.StartTime);
            Debug.WriteLine("End time " + appointment.EndTime);

            //get all the available doctors in the system
            var doctors = db1.AspNetUsers.Where(x => x.UserType.Equals("Doctor")).ToArray();
            Debug.WriteLine("Number of doctors " + doctors.Length);

            //for each of these doctors, see if they have an appointment
            foreach (AspNetUser user in doctors)
            {
                Debug.WriteLine("Doctor ID: " + user.Id);
                string doctorID = user.Id;
                //check how many appointments this doctor has and check if they clash with current date chosen by the user and check against all the times available 
                int doctorAppointmentsNum = db.Appointments.Where(x => x.DoctorID == doctorID && (x.StartTime == date1 || x.StartTime == date2 || x.StartTime == date3)).Count();
                
                Debug.WriteLine("AppointmentNum " + doctorAppointmentsNum);
                //appoint the first doctor found
                
                if (doctorAppointmentsNum >= 1)
                {
                    if (doctorAppointmentsNum == 1)
                    {
                        //set start time accordingly and assign doctor and break
                        appointment.DoctorID = user.Id;
                        appointment.StartTime = date2;
                        appointment.EndTime = appointment.StartTime.AddHours(2);
                        break;
                    }

                    if (doctorAppointmentsNum == 2)
                    {
                        //set start time accordingly and assign doctor and break
                        appointment.DoctorID = user.Id;
                        appointment.StartTime = date3;
                        appointment.EndTime = appointment.StartTime.AddHours(2);
                        break;
                    }

                }
                //this is if there are no appointments at all, assign the first available time line and break
                else
                {
                    //add start time
                    appointment.DoctorID = user.Id;
                    break;
                }

                


            }

            //if the user got an appointment and a doctor assigned, go in here
            if (appointment.DoctorID != null)
            {
                //this is to check if the user already has a previous appointment yet to be attended
                Debug.WriteLine("End Time " + appointment.EndTime);
                var appointments = db.Appointments.ToArray();
                foreach (Appointment app in appointments)
                {
                    string userID = app.User_Id;
                    DateTime endTime = (DateTime)app.EndTime;

                    //if an appointment exists with the user, then tell them that they must finish their upcoming appointment and cannot book another before then
                    if (userID.Equals(appointment.User_Id) && endTime > appointment.EndTime)
                    {
                        
                        Debug.WriteLine("Appointment Clashes");
                        return RedirectToAction("ClashView");


                    }
                    else
                    {
                        Debug.WriteLine("Appointment does not clash");
                    }

                }
                //add the appointment in the db and save it
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //if all the available time slots are taken, ask the user to choose another date
            return RedirectToAction("NoDoctorView");



        }

        [Authorize]
        public ActionResult AppointmentCalender()
        {
            return View();
        }
            
        //get the appointment info from the database and send it in json format
        public JsonResult GetEvents()
        {
            var appointments = db.Appointments.ToList();
            return new JsonResult { Data = appointments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Type,Description,DoctorID,Venue,StartTime,EndTime,User_Id")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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

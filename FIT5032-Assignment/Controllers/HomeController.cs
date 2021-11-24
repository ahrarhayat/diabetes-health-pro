using FIT5032_Assignment.Models;
using FIT5032_Assignment.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Assignment.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        HealthPro db = new HealthPro();
        private AppointmentModels db1 = new AppointmentModels();

        public ActionResult Home()
        {
            //check if the user is authenticated, if so, check their role and redirect to different login pages
            if(User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("Patient"))
                {
                    return RedirectToAction("HomeLoggedIn", "Home");
                }

                if (User.IsInRole("Doctor"))
                {
                    return RedirectToAction("HomeLoggedInDoctor", "Home");
                }



            }
            return View();
        }

        [Authorize]
        public ActionResult HomeLoggedIn()

        {
            var userId = User.Identity.GetUserId();
            var appointments = db1.Appointments.Where(d => d.User_Id ==
            userId && d.EndTime > DateTime.Now).ToList();
            return View(appointments);
        }

        [Authorize]
        public ActionResult HomeLoggedInDoctor()

        {
            var userId = User.Identity.GetUserId();
            var appointments = db1.Appointments.Where(d => d.DoctorID ==
            userId && d.EndTime > DateTime.Now).ToList();
            return View(appointments);
        }

        //get the data for the chart with different rating of the diet plans and calculate, then send as json
        [Authorize]
        public ActionResult GetData()
        {
            int fives = db.DietPlans.Where(x => x.Rating == 5).Count();
            int fours = db.DietPlans.Where(x => x.Rating == 4).Count();
            int threes = db.DietPlans.Where(x => x.Rating == 3).Count();
            int twos = db.DietPlans.Where(x => x.Rating == 2).Count();
            int ones = db.DietPlans.Where(x => x.Rating == 1).Count();
            Debug.WriteLine("Fives: " + fives);
            Debug.WriteLine("Fives: " + fours);
            Debug.WriteLine("Fives: " + threes);
            Debug.WriteLine("Fives: " + twos);
            Debug.WriteLine("Fives: " + ones);
            Ratio ratio = new Ratio();
            ratio.Fives = fives;
            ratio.Fours = fours;
            ratio.Threes = threes;
            ratio.Twos = twos;
            ratio.Ones = ones;
            return Json(ratio,JsonRequestBehavior.AllowGet);
        }

        public ActionResult RatingChart()
        {
            return View();
        }

        public class Ratio

         {
            public int Fives { get; set; }
            public int Fours { get; set; }
            public int Threes { get; set; }

            public int Twos { get; set; }

            public int Ones { get; set; }
        }
           

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //EmailSender.Execute().Wait();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Administrator, Doctor")]
        public ActionResult MealPlan()
        {
            return View();
        }

        public ActionResult Register()
        {
           
            return Redirect("~/Account/Register");
        }

        public ActionResult Login()
        {
            return Redirect("~/Account/Login");
        }

        public ActionResult View_Diet_Plans()
        {
            return View();
        }





    }
}
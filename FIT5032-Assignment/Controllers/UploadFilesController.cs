using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment.Models;
using FIT5032_Assignment.Utils;

namespace FIT5032_Assignment.Controllers
{
    public class UploadFilesController : Controller
    {
        private Upload db = new Upload();
        private EmailModels db1 = new EmailModels();

        // GET: UploadFiles
        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult UploadFiles()
        {
            return View(db.UploadFiles.ToList());
        }

        // GET: UploadFiles/Details/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFile uploadFile = db.UploadFiles.Find(id);
            if (uploadFile == null)
            {
                return HttpNotFound();
            }
            return View(uploadFile);
        }

        // GET: UploadFiles/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UploadFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Path,Name")] UploadFile uploadFile, HttpPostedFileBase postedFile)
        {
            var emails = db1.AspNetUsers.ToArray();
            //Debug.WriteLine("My debug string here");
            /*foreach (AspNetUser user in emails)
            {
                string email = user.Email;
                Debug.WriteLine(email);
            }*/
            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            uploadFile.Path = myUniqueFileName;
            TryValidateModel(uploadFile);

            if (ModelState.IsValid)
            {
                
                string serverPath = Server.MapPath("~/Uploads/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = uploadFile.Path + fileExtension;
                uploadFile.Path = filePath;
                //save the file in the server
                postedFile.SaveAs(serverPath + uploadFile.Path);
                //save the file details in the db
                Debug.WriteLine(serverPath + uploadFile.Path);
                db.UploadFiles.Add(uploadFile);
                db.SaveChanges();
                return RedirectToAction("Email");
            }

            return View(uploadFile);
        }

        [Authorize(Roles = "Doctor,Administrator")]
        public async Task<ActionResult> Email()
        {
            //get all the user emails
            var emails = db1.AspNetUsers.ToArray();
            //find the last uploaded file
            int id = db.UploadFiles.Max(item => item.Id);
            string serverPath = Server.MapPath("~/Uploads/");
            var path = "";
            //get the upload file instance from the id you got
            UploadFile up = db.UploadFiles.SingleOrDefault(x => x.Id == id);
            path = up.Path;
            Debug.WriteLine("File path: "+ path);
            //for each user email call the utils function from the EmailSender class to send the attachment to all the users
            foreach (AspNetUser user in emails)
            {
                string email = user.Email;
                Debug.WriteLine("Starts Here");
                Debug.WriteLine(email);
               
                string fullpath = serverPath + path;
                Debug.WriteLine("File path: "+ fullpath);
                await EmailSender.ExecuteManualAttachmentAdd(email,fullpath);
            }
            
      
            return RedirectToAction("EmailSent", "UploadFiles");
        }
        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult EmailSent()
        {
            return View();
        }

        // GET: UploadFiles/Edit/5
        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFile uploadFile = db.UploadFiles.Find(id);
            if (uploadFile == null)
            {
                return HttpNotFound();
            }
            return View(uploadFile);
        }

        // POST: UploadFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,Name")] UploadFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uploadFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UploadFiles");
            }
            return View(uploadFile);
        }

        // GET: UploadFiles/Delete/5
        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFile uploadFile = db.UploadFiles.Find(id);
            if (uploadFile == null)
            {
                return HttpNotFound();
            }
            return View(uploadFile);
        }

        // POST: UploadFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UploadFile uploadFile = db.UploadFiles.Find(id);
            db.UploadFiles.Remove(uploadFile);
            db.SaveChanges();
            return RedirectToAction("UploadFiles");
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

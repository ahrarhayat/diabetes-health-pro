using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FIT5032_Assignment.Utils
{
    public class EmailSender
    {
        private static void Main()
        {
            Execute("").Wait();
            ExecuteManualAttachmentAdd("","").Wait();
            ExecuteManualAttachmentAdd("").Wait();
            

        }
        
        //send email with sign up
        public static async Task Execute(String Email)
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ahrarhayat@gmail.com", "Diabetes Health Pro Team");
            var subject = "Registration Successful";
            var to = new EmailAddress(Email, "User");
            var plainTextContent = "Welcome to Diabetes health pro, your registration was successful";
            var htmlContent = "<strong>Welcome to Diabetes health pro, your registration was successful</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

       public static async Task ExecuteManualAttachmentAdd(String Email)
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ahrarhayat@gmail.com");
            var subject = "Welcome to Diabetes Health Pro";
            var to = new EmailAddress(Email,"User");
            var body = "Welcome to Diabetes health pro, your registration was successful";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");
            var bytes = File.ReadAllBytes("");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("Instructions.pdf", file);
            var response = await client.SendEmailAsync(msg);
        }

        //SendGrid bulk email
        public static async Task ExecuteManualAttachmentAdd(String Email, String path)
        {
            //get the month
            DateTime dt = DateTime.Now;
            string month = dt.ToString("MMMM");
            Debug.WriteLine("Month: " + month);
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ahrarhayat@gmail.com");
            var subject = month+" Newsletter!";
            var to = new EmailAddress(Email, "User");
            var body = "Hello User, this is your " + month+" newsletter";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");
            var bytes = File.ReadAllBytes(path);
             var file = Convert.ToBase64String(bytes);
             msg.AddAttachment(month+" "+"NewsLetter.pdf", file);
             var response = await client.SendEmailAsync(msg);
            Debug.WriteLine("Email Sent!: " + path);
        }
    }
}
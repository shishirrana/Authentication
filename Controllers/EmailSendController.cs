using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using reviseAuthentication.Models;
using reviseAuthentication.Settings;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace reviseAuthentication.Controllers
{
    public class EmailSendController : Controller
    {
        private readonly MailSettings _emailSetting;

        public EmailSendController(IOptions<MailSettings> emailSetting)
        {

            _emailSetting = emailSetting.Value;
        }
        public IActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Email(string to, string subject, string description)
        {
            try
            {
                // Create the MailMessage object
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_emailSetting.UserName);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = description;
                mail.IsBodyHtml = true; // Set to true if your body contains HTML content

                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient(_emailSetting.SmtpServer, _emailSetting.SmtpPort);
                smtpClient.EnableSsl = _emailSetting.EnableSsl;
                smtpClient.Credentials = new NetworkCredential(_emailSetting.UserName, _emailSetting.Password);

                // Send the email
                smtpClient.Send(mail);

                // Optionally, you can redirect to a success page or return a JSON response
                /*  return Json(new { success = true, message = "Email sent successfully." });*/
                ModelState.Clear();
                TempData["EmailSent"] = "Mail sent successfully";
                return View();


            }
            catch (Exception)
            {
                // Handle exceptions here, you can redirect to an error page or return a JSON response
                TempData["EmailError"] = "Error while sending mail";
            }
            return View();

        }


    }
}

using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Hosting;
using System.Web.Mvc;
using Hangfire;
using HangFire.Mailer.Attribute;
using HangFire.Mailer.Models;
using Postal;

namespace HangFire.Mailer.Controllers
{
    public class HomeController : Controller
    {
        private readonly MailerDbContext _db = new MailerDbContext();

        [HttpGet]
        public ActionResult Index()
        {
          
              

           
            var comments = _db.Comments.OrderBy(x => x.Id).ToList();
            return View(comments);
        }

        [HttpPost]
        public ActionResult Create(Comment model)
        {
            if (ModelState.IsValid)
            {
                _db.Comments.Add(model);
                _db.SaveChanges();

                //var email = new NewCommentEmail
                //{
                //    To = "yourmail@example.com",
                //    UserName = model.UserName,
                //    Comment = model.Text
                //};
              
                //email.Send();
            }

            BackgroundJob.Enqueue(() => NotifyNewComment(model.Id));
            return RedirectToAction("Index");
        }

        [AutomaticRetry(Attempts = 20)]
        [LogFailure]
        public  void NotifyNewComment(int commentId)
        {
            // Prepare Postal classes to work outside of ASP.NET request
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
          
                var client = new SmtpClient()
                {
                    Host = "smtp.sendgrid.net",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials =
                        new NetworkCredential("xpto", "xtpo")
                };
                var emailService = new EmailService(engines, () => client);

                // Get comment and send a notification.
                using (var db = new MailerDbContext())
                {


                    var comment = db.Comments.Find(commentId);
                    //var evento = db.tbClientesEventos.Where(c => c.cdEvento.Equals("010701"));

                    var email = new NewCommentEmail
                    {
                        To = "sample@email.com",
                        UserName = comment.UserName,
                        Comment = comment.Text
                    };

                    emailService.Send(email);
                
            }

         
          
            
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
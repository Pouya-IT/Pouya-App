using System;
using System.Web;
using System.Linq;
using Pouya.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;


namespace Pouya.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Models.RegisterModel model)
        {
            var Repository = new Models.Benutzer();

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            bool FindRegister = Adapter.Benutzer.Any
                  (x => x.Benutzername == model.Benutzername);

            if (FindRegister)
            {
                ModelState.AddModelError("", "Ein Benutzer mit diesem Namen existiert bereit");
                return this.View(model);
            }

            Repository.Vorname = model.Vorname;
            Repository.Nachname = model.Nachname;
            Repository.Adresse = model.Adresse;
            Repository.Benutzername = model.Benutzername;
            Repository.E_Mail = model.E_Mail;
            Repository.IDE_Delete_State = false;
            Repository.Land = model.Land;
            Repository.Passwort = model.Passwort.Trim();
            Repository.Postleitzahl = model.Postleitzahl;
            Repository.Stadt = model.Stadt;
            Repository.Telefon = model.Telefon;
            Repository.Id_Role = 2;
            try
            {
                Adapter.Benutzer.Add(Repository);
                Adapter.SaveChanges();
                Success("Neuer Benutzer erfolgreich erstellt");
            }
            catch
            {
                Error("Fehler beim Speichern. Bitte später noch einmal versuchen.");
            }
            return this.View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.Login LoginModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(LoginModel);
            }

            var User = Adapter.Benutzer.FirstOrDefault
                              (p => p.Benutzername.ToLower().Trim() == LoginModel.Benutzername.ToLower().Trim()
                              &&
                              p.Passwort.ToLower() == LoginModel.Passwort.ToLower());

            if (User == null)
            {
                ModelState.AddModelError("", "Benutzername ist falsch");
                return View(LoginModel);
            }
            else
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(User.Benutzername, false);
                return this.RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult SignOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return this.RedirectToAction("Login");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }


        [HttpGet]
        [Authorize]
        public ActionResult Produkte()
        {
            ViewBag.Message = "Produkte";
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            var VM = new Models.FeedbackPageMV();
            VM.CreateForm = new Models.FeedbackModel();
            if (IsAdmin())
            {
                VM.FeedBackList = Adapter.Feedback
                                         .Where(p => !p.IsApproved && !p.IDE_Delete_State)
                                         .OrderByDescending(p => p.CreatedDate)
                                         .ToList();
            }
            else
            {
                VM.FeedBackList = Adapter.Feedback
                                         .Where(p => p.IsApproved && !p.IDE_Delete_State)
                                         .OrderByDescending(p => p.CreatedDate)
                                         .ToList();
            }
            ViewBag.IsAdmin = IsAdmin();
            return View(VM);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Contact(Models.FeedbackPageMV model)
        {
            if (!ModelState.IsValid)
            {
                if (IsAdmin())
                {
                    model.FeedBackList = Adapter.Feedback
                                             .Where(p => !p.IsApproved && !p.IDE_Delete_State)
                                             .OrderByDescending(p => p.CreatedDate)
                                             .ToList();
                }
                else
                {
                    model.FeedBackList = Adapter.Feedback
                                             .Where(p => p.IsApproved && !p.IDE_Delete_State)
                                             .OrderByDescending(p => p.CreatedDate)
                                             .ToList();
                }
                ViewBag.IsAdmin = IsAdmin();
                return View(model);
            }

            var Repository = new Models.Feedback();

            Repository.Name = model.CreateForm.Name;
            Repository.Email = model.CreateForm.Email;
            Repository.Text = model.CreateForm.Text;

            Repository.IsApproved = false;
            Repository.CreatedDate = DateTime.UtcNow;
            Repository.IDE_Delete_State = false;

            try
            {
                Adapter.Feedback.Add(Repository);
                Adapter.SaveChanges();
                Success("Vielen Dank! Ihr Feedback wurde erfolgreich gespeichert.");
            }
            catch
            {
                Error("Fehler beim Speichern. Bitte später noch einmal versuchen.");
            }

            if (IsAdmin())
            {
                model.FeedBackList = Adapter.Feedback
                                         .Where(p => !p.IsApproved && !p.IDE_Delete_State)
                                         .OrderByDescending(p => p.CreatedDate)
                                         .ToList();
            }
            else
            {
                model.FeedBackList = Adapter.Feedback
                                         .Where(p => p.IsApproved && !p.IDE_Delete_State)
                                         .OrderByDescending(p => p.CreatedDate)
                                         .ToList();
            }
            ViewBag.IsAdmin = IsAdmin();
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult BestätigungFeedback(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (!IsAdmin())
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }
            var FindFeedback = Adapter.Feedback.Find(id);
            if (FindFeedback == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            FindFeedback.IsApproved = true;
            FindFeedback.CreatedDate = DateTime.UtcNow;

            try
            {
                Adapter.SaveChanges();
                Success("Das Feedback wurde freigegeben.");
            }
            catch
            {
                Error("Fehler beim Speichern. Bitte später noch einmal versuchen.");
            }

            return RedirectToAction("Contact");
        }


        [HttpPost]
        [Authorize]
        public ActionResult DeleteFeedback(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (!IsAdmin())
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            var FindFeedback = Adapter.Feedback.Find(id);
            if (FindFeedback == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }
            FindFeedback.IDE_Delete_State = true;
            try
            {
                Adapter.SaveChanges();
                Success("Das Feedback wurde erfolgreich gelöscht.");
            }
            catch
            {
                Error("Fehler beim DeleteFeedback. Bitte später noch einmal versuchen.");
            }
            return RedirectToAction("Contact");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult DetailsFeedback(int id)
        {
            var item = Adapter.Feedback.Find(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }
    }
}
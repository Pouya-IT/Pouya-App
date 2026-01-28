using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Pouya.Models;

namespace Pouya.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet, Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost,AllowAnonymous]
        public ActionResult Register(Models.RegisterModel model)
        {

            var Repository = new Models.Benutzer();

            using (var Adapter = new Models.DatabaseContex())
            {
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
                else
                {
                    Repository.Vorname = model.Vorname;
                    Repository.Nachname = model.Nachname;
                    Repository.Adresse = model.Adresse;
                    Repository.Benutzername =   model.Benutzername;
                    Repository.E_Mail = model   .E_Mail;
                    Repository.IDE_Delete_State = false;
                    Repository.Land = model.Land;
                    Repository.Passwort = model.Passwort.Trim();
                    Repository.Postleitzahl = model.Postleitzahl;
                    Repository.Stadt = model.Stadt;
                    Repository.Telefon = model.Telefon;
                    Repository.Id_Role = 2;
                    
                    Adapter.Entry(Repository).State = System.Data.Entity.EntityState.Added;
                    Adapter.SaveChanges();
                    ViewBag.Massege = "Neuer Benutzer erfolgreich erstellt";
                }
                return this.View(model);
            }
        }


        [HttpGet,AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost,AllowAnonymous]
        public ActionResult Login(Models.Login LoginModel) 
        {
            if (!ModelState.IsValid)
            {
                return this.View(LoginModel);
            }

            using (var Adapter = new Models.DatabaseContex())
            {
                var User= Adapter.Benutzer.FirstOrDefault
                                  (p=>p.Benutzername.ToLower().Trim() == LoginModel.Benutzername.ToLower().Trim() 
                                  && 
                                  p.Passwort.ToLower()== LoginModel.Passwort.ToLower());

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
        }

        [HttpGet,AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpGet, Authorize]
        public ActionResult Produkte()
        {
            ViewBag.Message = "Produkte";
            return View();
        }



        [HttpGet, AllowAnonymous]
        public ActionResult Contact()
        {
            //ViewBag.Message = "contact";

            var VM = new Models.FeedbackPageMV();
            VM.CreateForm = new Models.FeedbackModel();

            using(var Adapter = new Models.DatabaseContex())
            {
                
                VM.FeedBackList = Adapter.Feedback
                    .Where(p=>p.IsApproved && !p.IDE_Delete_State)
                    .OrderByDescending(p=>p.CreatedDate)
                    .ToList();

                bool IsAdmin = false;
                if (User.Identity.IsAuthenticated) 
                { 
                    var UserName=User.Identity.Name;
                    var UserAdmin=Adapter.Benutzer.FirstOrDefault(p=>p.Benutzername == UserName);
                    if(UserAdmin!=null && UserAdmin.Id_Role == 1)
                    {
                        IsAdmin= true;
                    }
                    ViewBag.IsAdmin = IsAdmin;
                }

            }
            return View(VM);
        }

        [HttpPost,AllowAnonymous]
        public ActionResult Contact(Models.FeedbackPageMV model)
        {
            if (!ModelState.IsValid) 
            { 
                using(var Adapter = new Models.DatabaseContex())
                {
                    model.CreateForm = new Models.FeedbackModel();
                    model.FeedBackList= Adapter.Feedback
                    .Where(p => p.IsApproved && !p.IDE_Delete_State)
                    .OrderByDescending(p => p.CreatedDate)
                    .ToList();
                }
            }

            using(var Adapter = new Models.DatabaseContex())
            { 
                var Repository = new Models.Feedback();

                Repository.Name= model.CreateForm.Name;
                Repository.Email= model.CreateForm.Email;
                Repository.Text = model.CreateForm.Text;

                Repository.IsApproved = false;
                Repository.CreatedDate = DateTime.UtcNow;
                Repository.IDE_Delete_State = false;

                Adapter.Entry(Repository).State=System.Data.Entity.EntityState.Added;
                Adapter.SaveChanges();
                TempData["Success"] = "Vielen Dank! Ihr Feedback wurde erfolgreich gespeichert.";

                model.FeedBackList = Adapter.Feedback
                                            .Where(p => p.IsApproved && !p.IDE_Delete_State)
                                            .OrderByDescending(p => p.CreatedDate)
                                            .ToList();
            }
            return View(model);
        }


        //To Do
        [HttpPost]
        [Authorize]
        public ActionResult Bestätigung(int id)
        {
            var UserName= User.Identity.Name;
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }
            using(var Adapter = new Models.DatabaseContex())
            {
                var AdminUser = Adapter.Benutzer
                    .FirstOrDefault(p => p.Benutzername.ToLower() == UserName.ToLower() 
                    && p.Id_Role == 1);

                var FindFeedback = Adapter.Feedback.Find(id);
                if (AdminUser == null || FindFeedback == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }

                FindFeedback.IsApproved = true;
                FindFeedback.CreatedDate = DateTime.UtcNow;
                Adapter.Entry(FindFeedback).State=System.Data.Entity.EntityState.Modified;
                Adapter.SaveChanges();
                TempData["Success"] = "Das Feedback wurde freigegeben.";
            }
            return RedirectToAction ("Contact");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
                    Repository.IDE_Delete_State = 0;
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

        [HttpGet,Authorize]
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

        [HttpGet, Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
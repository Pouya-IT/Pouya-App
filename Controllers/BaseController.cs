using System;
using System.Web;
using System.Linq;
using Pouya.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;


namespace Pouya.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = User.Identity.Name;
            }
            base.OnActionExecuting(filterContext);
        }



        //=====================================================
        // DataBase (Shared DB Context)
        //=====================================================
        protected DatabaseContex Adapter = new DatabaseContex();

        //=====================================================
        // Auto Dispose Db
        //=====================================================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Adapter.Dispose();
            }
            base.Dispose(disposing);
        }
        //=====================================================



        //=====================================================
        // UserName 
        //=====================================================
        protected string CurrentUserName
        {
            get
            {
                return User?.Identity?.Name;
            }
        }


        //=====================================================
        // CurrentUser
        //=====================================================
        protected Benutzer CurrentUser
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentUserName)) return null;
                return Adapter.Benutzer.FirstOrDefault(p => p.Benutzername == CurrentUserName);
            }
        }


        //=====================================================
        // IsAuthenticated
        //=====================================================
        protected bool IsLoggedIn()
        {
            return User?.Identity?.IsAuthenticated == true;
        }


        //=====================================================
        // IsAdmin
        //=====================================================
        protected bool IsAdmin()
        {
            return CurrentUser?.Id_Role == 1;
        }


        //=====================================================
        // Success Message (TempData Helper)
        //=====================================================
        protected void Success(string message)
        {
            TempData["Success"] = message;
        }


        //=====================================================
        // Error Message (TempData Helper)
        //=====================================================
        protected void Error(string message)
        {
            TempData["Error"] = message;
        }
    }
}
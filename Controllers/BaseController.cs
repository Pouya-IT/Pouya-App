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
    }
}
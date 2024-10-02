using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_User.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["userName"] == null)
            {
                filterContext.Result = RedirectToAction("FormLogin", "Login", new { message = "You must be login" });
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
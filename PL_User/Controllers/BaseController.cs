using BLL_User.BUS;
using BLL_User.Model;
using PL_User.Extension;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PL_User.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["Id"] != null && Session["userName"] != null)
            {
                var sesssionPermissions = SessionExtension.GetSession(filterContext.HttpContext, "listpermission_"+ Session["Id"]);
                if(sesssionPermissions == null)
                {
                    SessionExtension.SetSessionPermission(filterContext.HttpContext, Convert.ToInt32(Session["Id"].ToString()));
                    sesssionPermissions = SessionExtension.GetSession(filterContext.HttpContext, "listpermission_" + Session["Id"]);    
                }
                var permission = (List<PermissionDTO>)sesssionPermissions;
                PermissionServices permissionService = new PermissionServices();
                ViewBag.Menu = permissionService.GetMenuByPermission(permission);
            }
            else
            {
                filterContext.Result = RedirectToAction("FormLogin", "Login", new { message = "You must be login" });
            }
        }
    }
}
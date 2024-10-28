using BLL_User.Model;
using PL_User.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PL_User
{
    public class AuthorizeUser : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _permissionFilter;
        public AuthorizeUser(params string[] permissions)
        {
            _permissionFilter = permissions;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserName"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    HandleUnauthorizedRequest(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "FormLogin" } ,  { "controller","Login"} });
                    return;
                }
            }
            if(_permissionFilter != null && _permissionFilter.Any() && !CheckPermission(filterContext, filterContext.HttpContext.Session["Id"].ToString()))
            {
                filterContext.Controller.TempData["Error"] = "You can not access features, Please grant permissions ";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Home" } });
            }
           
        }
        private bool CheckPermission(AuthorizationContext filterContext, string userId)
        {
            var sessionPermission = SessionExtension.GetSession(filterContext.HttpContext, "listpermission_" + userId);
            if (sessionPermission == null) SessionExtension.SetSessionPermission(filterContext.HttpContext, Convert.ToInt32(userId));
            sessionPermission = SessionExtension.GetSession(filterContext.HttpContext, "listpermission_" + userId);
            var permission = (List<PermissionDTO>) sessionPermission;
            if(_permissionFilter != null && _permissionFilter.Any())
            {
                var IsPermisison = true;
                foreach(var item in _permissionFilter)
                {
                    if(!permission.Any(a => a.Name.Equals(item)))
                    {
                        IsPermisison = false;
                        break;
                    }
                    else
                    {
                        return true;
                    }
                }
                return IsPermisison;
            }
            return true;
        }
    }
}
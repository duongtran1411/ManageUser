using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using BLL_User.BUS;

namespace PL_User.Extension
{
    public class SessionExtension
    {
        public static void AddSession(HttpContextBase context, string sessionName, object value)
        {
            context.Session.Add(sessionName, value);
        }

        public static object GetSession(HttpContextBase context, string sessionName)
        {
            return context.Session[sessionName];
        } 

        public static void ClearSession(HttpContextBase context, string sessioName)
        {
            var enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                context.Session.Remove((string)enumerator.Key);
            }
        }

        public static void SetSessionPermission(HttpContextBase context, long userId)
        {
            var permissionService = new PermissionServices();
            var permissions = permissionService.GetPermissionByUserId(userId);
            if (permissions.Count > 0)
            {
                AddSession(context, $"listPermission_{userId}", permissions);
            }
        }
    }
}
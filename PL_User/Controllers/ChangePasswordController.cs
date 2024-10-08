﻿using BLL_User.BUS;
using BLL_User.Model;
using System.Web.Mvc;

namespace PL_User.Controllers
{
    public class ChangePasswordController : BaseController
    {
        private UserServices _services = new UserServices();
        // GET: ChangePassword
        public ActionResult View(int id)
        {
            UserDTO user = _services.GetUserById(id);
            return View(user);
        }

        public ActionResult ChangePass(ChangePassDto input)
        {
            string errorMessage = string.Empty;
            if(_services.ChangePassword(input, out errorMessage))
            {
                TempData["ChangeSuccess"] = "Change Successful";
                return RedirectToAction("View", "ChangePassword", new {id = input.UserId });
            }
            else
            {
                TempData["ChangeFailed"] = errorMessage;
                return RedirectToAction("View", "ChangePassword", new { id = input.UserId });
            }
        }
    }
}
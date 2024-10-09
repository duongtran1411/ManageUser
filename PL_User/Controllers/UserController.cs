using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL_User.BUS;
using BLL_User.Model;

namespace PL_User.Controllers
{
    public class UserController : BaseController
    {
        private UserServices _services = new UserServices();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListUser()
        {
            try
            {
                var draw = HttpContext.Request.Form.GetValues("draw").FirstOrDefault();
                var skip = Convert.ToInt32(Request.Form.GetValues("start").FirstOrDefault());
                var take = Convert.ToInt32(Request.Form.GetValues("length").FirstOrDefault());
                var searchText = Request.Form.GetValues("search[value]").FirstOrDefault();
                var totalRecord = 0;
                var listUser = _services.GetUserByPaging(new FilterDTO { SkipCount = skip, PageCount = take, FilterSearch = searchText }, out totalRecord);

                return Json(new { draw, recordsFiltered = totalRecord, recordsTotal = totalRecord, data = listUser });
            }
            catch (Exception)
            {
                return Json(new { recordsFiltered = 0, recordsTotal = 0, data = new List<UserDTO>() });
            }
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var message = "";
                if (_services.CreateOrEdit(userDTO, out message))
                {
                    message = "Add Successful";
                    return Json(new { success = true, message });
                }
                else
                {
                    return Json(new { success = false, message });
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return Json(new { success = false, message = string.Join(", ", errors) });
            }

        }


        [HttpGet]
        public ActionResult DeleteUser(int userId)
        {
            var message = "";
            if (_services.DeleteUserById(userId, out message))
            {
                return Json(new { success = true, message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = "";
                if (_services.CreateOrEdit(user, out errorMessage))
                {
                    TempData["updateSuccess"] = "Update Successful or Unchanges";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["updateFailed"] = errorMessage;
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["updateFailed"] = string.Join(", ", errors);
                return RedirectToAction("Index", "User");
            }

        }

        [HttpGet]
        public ActionResult GetUserToEdit(int userId)
        {
            if (userId != 0)
            {
                UserDTO user = _services.GetUserById(userId);
                return Json(user, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["errorMessage"] = "User not existed or deleted";
                return RedirectToAction("ListUser", "User", JsonRequestBehavior.AllowGet);
            }

        }

    }
}
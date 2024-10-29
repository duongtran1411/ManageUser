using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL_User.BUS;
using BLL_User.Enumeration;
using BLL_User.Model;

namespace PL_User.Controllers
{
    public class UserController : BaseController
    {
        private UserServices _services = new UserServices();
        private RoleServices _roleServices = new RoleServices();
        // GET: User
        [AuthorizeUser(RoleEnums.User_View)]
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

        [AuthorizeUser(RoleEnums.User_Created)]
        public ActionResult AddUser()
        {
            LoadRoleStatic();
            return View();
        }

        [HttpPost]
        [AuthorizeUser(RoleEnums.User_Created)]
        public ActionResult AddUser(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var message = "";
                long userId = (long)Session["Id"];
                if (_services.CreateOrEdit(userDTO, out message, userId))
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
        [AuthorizeUser(RoleEnums.User_Deleted)]
        public ActionResult DeleteUser(int userId)
        {
            var message = "";
            long userDeleted = (long)Session["Id"];
            if (_services.DeleteUserById(userId, out message, userDeleted))
            {
                return Json(new { success = true, message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeUser(RoleEnums.User_Updated)]
        public ActionResult EditUser(long id)
        {
            UserDTO user = _services.GetUserById(id);
            LoadRoleStatic();
            return View(user);
        }

        [HttpPost]
        [AuthorizeUser(RoleEnums.User_Updated)]
        public ActionResult EditUser(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = "";
                long userId = (long)Session["Id"];
                if (_services.CreateOrEdit(user, out errorMessage, userId))
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
        [AuthorizeUser(RoleEnums.User_Updated)]
        public ActionResult GetUserToEdit(long userId)
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

        public ActionResult ViewChange(int id)
        {
            UserDTO user = _services.GetUserById(id);
            Session["PreviousPage"] = Request.UrlReferrer.ToString();
            return View(user);
        }

        public ActionResult GoBack()
        {
            if (Session["PreviousPage"] != null)
            {
                return Redirect(Session["PreviousPage"].ToString());
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangePass(ChangePassDto input)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                long userId = (long)Session["Id"];
                if (_services.ChangePassword(input, out errorMessage, userId))
                {
                    TempData["ChangeSuccess"] = "Change Successful";
                    return RedirectToAction("ViewChange", "User", new { id = input.UserId });
                }
                else
                {
                    TempData["ChangeFailed"] = errorMessage;
                    return RedirectToAction("ViewChange", "User", new { id = input.UserId });
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["ChangeFailed"] = string.Join(", ", errors);
                return RedirectToAction("ViewChange", "User", new { id = input.UserId });
            }
        }

        private void LoadRoleStatic()
        {
            ViewBag.Role = _roleServices.GetStaticRole();
        }

    }
}
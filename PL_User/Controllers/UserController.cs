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
                return Json(new { recordsFiltered = 0, recordsTotal = 0, data = new List<UserDTO>()});
            }
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO userDTO)
        {
            if (_services.CreateUser(userDTO))
            {
                TempData["AddSuccess"] = "Add Successful";
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["AddFailed"] = "Username is existed";
                return RedirectToAction("Index", "User");
            }
        }

        public void DeleteUser(int userId)
        {
            string message = "";
            _services.DeleteUserById(userId, message);
        }

        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            if (_services.EditUser(user))
            {
                TempData["updateSuccess"] = "Update Successful";
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["updateFailed"] = "Update Unsuccessful Or Unchanges";
                return RedirectToAction("Index", "User");
            }
        }

        [HttpGet]
        public ActionResult GetUserToEdit(int userId)
        {
            UserDTO user = _services.GetUserById(userId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}
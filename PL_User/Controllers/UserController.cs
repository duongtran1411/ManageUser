using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL_User;
using BLL_User.DTO;

namespace PL_User.Controllers
{
    public class UserController : Controller
    {
        private CRUDUser _user = new CRUDUser();
        // GET: User
        public ActionResult ListUser()
        {
            List<UserDTO> listUser = _user.GetUsers();
            ViewData["listUser"] = listUser;
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO userDTO)
        {
            if (_user.DupplicateUser(userDTO))
            {
                _user.AddUserToList(userDTO);
                TempData["AddSuccess"] = "User has added";
                return RedirectToAction("ListUser", "User");
            }
            else
            {
                TempData["AddFailed"] = "Username is existed";
                return RedirectToAction("ListUser", "User");

            }
        }

        public void DeleteUser(int userId)
        {
            _user.RemoveUser(userId);
        }

        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {

            if (_user.DupplicateUser(user))
            {
                if (_user.AllowEdit(user))
                {
                    var originalUser = _user.GetUserById(user.id);
                    if (user.userName == originalUser.userName)
                    {
                        TempData["updateSuccess"] = "No changes made, but successful.";
                        return RedirectToAction("ListUser", "User");
                    }
                    TempData["updateFailed"] = "Change Unsuccessful";
                    return RedirectToAction("ListUser", "User");
                }
                else
                {
                    
                    _user.EditUser(user);
                    TempData["updateSuccess"] = "Change Successful";
                    return RedirectToAction("ListUser", "User");
                }
            }
            else
            {
                if (!_user.AllowEdit(user))
                {
                    _user.EditUser(user);
                    TempData["updateSuccess"] = "Change Successful";
                    return RedirectToAction("ListUser", "User");
                }
                else
                {
                    _user.EditUser(user);
                    TempData["updateSuccess"] = "Change Successful";
                    return RedirectToAction("ListUser", "User");
                }
            };
            
        }

        [HttpGet]
        public ActionResult GetUserToEdit(int userId)
        {
            UserDTO user = _user.GetUserById(userId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}
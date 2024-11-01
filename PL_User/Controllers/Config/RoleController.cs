using BLL_User.BUS;
using BLL_User.Enumeration;
using BLL_User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PL_User.Controllers.Role
{
    public class RoleController : BaseController
    {
        private RoleServices _services = new RoleServices();
        private PermissionServices _permServices = new PermissionServices();
        // GET: Role
        [AuthorizeUser(RoleEnums.Role_View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeUser(RoleEnums.Role_View)]
        public ActionResult ListRole()
        {
            try
            {
                var draw = HttpContext.Request.Form.GetValues("draw").FirstOrDefault();
                var skip = Convert.ToInt32(Request.Form.GetValues("start").FirstOrDefault());
                var length = Convert.ToInt32(Request.Form.GetValues("length").FirstOrDefault());
                var filter = Request.Form.GetValues("search[value]").FirstOrDefault();
                var totalRecord = 0;
                var listRole = _services.GetRolePaging(new FilterDTO { FilterSearch = filter, PageCount = length, SkipCount = skip }, out totalRecord);
                return Json(new {draw , recordsFiltered = totalRecord , recordsTotal = totalRecord , data = listRole });
            }
            catch (Exception ex)
            {
                return Json(new { recordsFiltered = 0, recordsTotal = 0, data = new List<RoleDTO>()});
            }
        }

        [AuthorizeUser(RoleEnums.Role_Created)]
        public ActionResult AddRole()
        {
            RoleDTO role = new RoleDTO();
            role.ListPermission = _permServices.GetStaticPermission(role.Id);
            return View(role);
        }

        [HttpPost]
        [AuthorizeUser(RoleEnums.Role_Created)]
        public ActionResult AddRole(RoleDTO role)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = "";
                long userId = (long)Session["Id"];
                if (_services.CreatedOrEdit(role, out errorMessage, userId))
                {
                    TempData["AddSuccess"] = "Add Role Successful";
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    TempData["AddFailed"] = errorMessage;
                    return RedirectToAction("Index", "Role");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["AddFailed"] = string.Join(", ", errors);
                return RedirectToAction("AddRole", "Role");
            }
        }

        [AuthorizeUser(RoleEnums.Role_Updated)]
        public ActionResult EditRole(long Id)
        {
            if(Id != 0)
            {
                RoleDTO role = _services.GetRoleById(Id);
                role.ListPermission = _permServices.GetStaticPermission(Id);
                return View(role);
            }
            else
            {
                return RedirectToAction("Index", "Role");
            }
        }

        public ActionResult GetPermissionRole(long Id)
        {
            var result = _permServices.GetStaticPermission(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeUser(RoleEnums.Role_Updated)]
        public ActionResult EditRole(RoleDTO role)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = "";
                long userId = (long)Session["Id"];
                if (_services.CreatedOrEdit(role, out errorMessage, userId))
                {
                    TempData["UpdateSucess"] = "Update Successful";
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    TempData["UpdateFailed"] = errorMessage;
                    return RedirectToAction("EditRole","Role");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["UpdateFailed"] = string.Join(", ", errors);
                return RedirectToAction("EditRole", "Role");
            }
        }

        [HttpGet]
        [AuthorizeUser(RoleEnums.Role_Deleted)]
        public ActionResult DeleteRole(long roleId)
        {
            var errorMessage = "";
            long userId = (long) Session["Id"];
            if (_services.DeletedRole(roleId, userId,out errorMessage))
            {
                return Json(new { success = true, message = "Delete Successful" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}
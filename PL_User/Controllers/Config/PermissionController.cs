using BLL_User.BUS;
using BLL_User.Extension;
using BLL_User.Model;
using BLL_User.Model.Role;
using BLL_User.Enumeration;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PL_User.Controllers
{
    public class PermissionController : BaseController
    {
        private PermissionServices _services = new PermissionServices();
        // GET: Permission
        [AuthorizeUser(RoleEnums.Permission_View)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListPermission()
        {
            var result = _services.GetAllPermission();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(RoleEnums.Permission_Created)]
        public ActionResult AddPermission()
        {
            LoadDataStatic();
            PermissionDTO permission = new PermissionDTO();
            return View(permission);
        }

        [HttpPost]
        [AuthorizeUser(RoleEnums.Permission_Created)]
        public ActionResult AddPermission(PermissionDTO permission)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = "";
                long userId = (long)Session["Id"];
                if (_services.CreateOrEdit(permission, out errorMessage, userId))
                {
                    TempData["AddSucess"] = "Add Successful";
                    return RedirectToAction("Index", "Permission");
                }
                else
                {
                    TempData["AddFailed"] = errorMessage;
                    return RedirectToAction("AddPermission", "Permission");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["AddFailed"] = string.Join(", ", errors);
                return RedirectToAction("AddPermission", "Permission");
            }

        }

        public void LoadDataStatic()
        {
            ViewBag.PermissionTypes = StaticData.PermissionTypes;
            ViewBag.PermissionsParent = _services.GetPermissionByType(new List<byte> { 1, 2 });
        }

        [AuthorizeUser(RoleEnums.Permission_Updated)]
        public ActionResult EditPermission(long Id)
        {
            var errorMessage = "";
            var permission = _services.GetPermissionById(Id, out errorMessage);
            if (permission == null)
            {
                TempData["errorMessage"] = errorMessage;
                return RedirectToAction("Index", "Permission");
            }
            LoadDataStatic();
            return View(permission);
        }

        [HttpPost]
        [AuthorizeUser(RoleEnums.Permission_Updated)]
        public ActionResult EditPermission(PermissionDTO input)
        {
            var errorMessage = "";
            long userId = (long)Session["Id"];
            if (_services.CreateOrEdit(input,out errorMessage, userId))
            {
                TempData["UpdateSuccess"] = "Update Successful";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["UpdateFailed"] = errorMessage;
                return RedirectToAction("EditPermission","Permission");
            }
        }

        [HttpGet]
        [AuthorizeUser(RoleEnums.Permission_Deleted)]
        public ActionResult DeletePermission(int permissionId)
        {
            var errorMessage = "";
            long userId = (long)Session["Id"];
            if (_services.DeletedPermission(permissionId, userId, out errorMessage))
            {
                return Json(new {success = true, message = "Deleted Success" },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPermissionByRoleId(string roleId)
        {
            var result = new List<TreeViewDTO>();
            result = _services.GetPermissionbyRoleId(roleId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
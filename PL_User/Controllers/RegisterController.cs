using System.Linq;
using System.Web.Mvc;
using BLL_User.BUS;
using BLL_User.Model;

namespace PL_User.Controllers
{
    public class RegisterController : Controller
    {
        private UserServices _userServices = new UserServices();
        // GET: Register
        public ActionResult FormRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = "";
                if (_userServices.CreateOrEdit(user, out errorMessage))
                {
                    TempData["RegisterSuccess"] = "Register Successful ! Login Right Now";
                    return View("FormLogin", "Login");
                }
                else
                {
                    TempData["Dupplicate"] = errorMessage;
                    return View("FormRegister", "Register");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["registerFailed"] = string.Join(", ", errors);
                return RedirectToAction("FormRegister", "Register");
            }
        }
    }
}
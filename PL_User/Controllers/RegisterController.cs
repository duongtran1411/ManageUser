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
            var errorMessage = "";
            if (_userServices.CreateOrEdit(user, out errorMessage))
            {
                TempData["RegisterSuccess"] = "Register Successful ! Login Right Now";
                return View("FormLogin","Login");
            }
            else
            {
                TempData["Dupplicate"] = errorMessage;
                return View("FormRegister","Register");
            }
        }
    }
}
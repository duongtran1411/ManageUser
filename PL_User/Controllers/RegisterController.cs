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
            if (_userServices.CreateUser(user))
            {
                TempData["RegisterSuccess"] = "Register Successful ! Login Right Now";
                return View("FormLogin","Login");
            }
            else
            {
                TempData["Dupplicate"] = "Username is existed";
                return View("FormRegister","Register");
            }
        }
    }
}
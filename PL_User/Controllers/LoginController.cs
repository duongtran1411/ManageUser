using BLL_User.BUS;
using BLL_User.Model;
using System.Linq;
using System.Web.Mvc;

namespace PL_User.Controllers
{
    public class LoginController : Controller
    {
        private UserServices _services = new UserServices();
        // GET: Login
        public ActionResult FormLogin(string message)
        {
            TempData["ErrorMessage"] = message;
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(LoginDTO loginDto)
        {
            
            if(ModelState.IsValid)
            {
                var errorMessage = "";
                var result = _services.GetUserByUserNameAndPassword(loginDto,out errorMessage);
                if (result != null)
                {
                    Session["userName"] = result.UserName;
                    Session["Id"] = result.Id;
                    TempData["loginSuccess"] = "Login Successful";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["loginFailed"] = errorMessage;
                    return RedirectToAction("FormLogin", "Login");
                }
            }
            else
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["loginFailed"] = string.Join(",", error);
                return RedirectToAction("FormLogin", "Login");
            }
        }

        
        public ActionResult UserLogout()
        {
            Session["userName"] = null;
            Session.Clear();
            return RedirectToAction("FormLogin", "Login");
        }

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
                if (_services.RegisterUser(user, out errorMessage))
                {
                    TempData["RegisterSuccess"] = "Register Successful ! Login Right Now";
                    return RedirectToAction("FormLogin", "Login");
                }
                else
                {
                    TempData["Dupplicate"] = errorMessage;
                    return RedirectToAction("FormRegister", "Login");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                TempData["registerFailed"] = string.Join(", ", errors);
                return RedirectToAction("FormRegister", "Login");
            }
        }
    }
}
using BLL_User.BUS;
using BLL_User.Model;
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
            var result = _services.GetUserByUserNameAndPassword(loginDto);
            if(ModelState.IsValid)
            {
                if (result != null)
                {
                    Session["userName"] = result.UserName;
                    TempData["loginSuccess"] = "Login Successful";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["loginFailed"] = "Username or password incorrect";
                    return RedirectToAction("FormLogin", "Login");
                }
            }
            else
            {
                return RedirectToAction("FormLogin", "Login");
            }
        }

        
        public ActionResult UserLogout()
        {
            Session["userName"] = null;
            Session.Clear();
            return RedirectToAction("FormLogin", "Login");
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BLL_User.Model
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username can not empty")]
        [StringLength(50,ErrorMessage = "Length max is 50 character")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password can not empty")]
        [StringLength (100, ErrorMessage ="Length max is 100 character")]
        public string Password { get; set; }
    }
}

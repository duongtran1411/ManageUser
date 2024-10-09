
using System.ComponentModel.DataAnnotations;

namespace BLL_User.Model
{
    public class ChangePassDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Range(8,100, ErrorMessage = "Password between 8 to 100 characters")]
        public string Password { get; set; }
        [Required]
        [Range(8, 100, ErrorMessage = "Password between 8 to 100 characters")]
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}

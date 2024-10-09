
using System.ComponentModel.DataAnnotations;

namespace BLL_User.Model
{
    public class ChangePassDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be length at least 8 characters")]
        [MaxLength(100, ErrorMessage = "Password must be length not over 100 characters")]
        public string Password { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "New Password must be length at least 8 characters")]
        [MaxLength(100, ErrorMessage = "New Password must be length not over 100 characters")]
        public string NewPassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Confirm Password must be length at least 8 characters")]
        [MaxLength(100, ErrorMessage = "Confirm Password must be length not over 100 characters")]
        public string ConfirmPassword { get; set; }
    }
}

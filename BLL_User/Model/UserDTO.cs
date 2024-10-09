
using System.ComponentModel.DataAnnotations;

namespace BLL_User.Model
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be length at least 8 characters")]
        [MaxLength(50, ErrorMessage = "Password must be length not over 50 characters")]
        public string UserName { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be length at least 8 characters")]
        [MaxLength(50,ErrorMessage = "Password must be length not over 100 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name can not empty")]
        [StringLength(50, ErrorMessage = "First Name have max length 50 character")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name can not empty")]
        [StringLength(50, ErrorMessage = "First Name have max length 50 character")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter correct format email address")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Please enter a valid Phone no")]
        public string Phone { get; set; }
        public System.DateTime CreatedTime { get; set; }

    }
}

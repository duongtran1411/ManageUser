using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BLL_User.Model
{
    public class UserDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be length at least 8 characters")]
        [MaxLength(50, ErrorMessage = "Password must be length not over 50 characters")]
        public string UserName { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be length at least 8 characters")]
        [MaxLength(50, ErrorMessage = "Password must be length not over 100 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name can not empty")]
        [StringLength(50, ErrorMessage = "First Name have max length 50 character")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name can not empty")]
        [StringLength(50, ErrorMessage = "First Name have max length 50 character")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Please enter correct format email address")]
        public string Email { get; set; }
        [MinLength(10, ErrorMessage = "Phone number must be include 10 digit")]
        [MaxLength(10, ErrorMessage = "Phone number must be include 10 digit")]
        public string Phone { get; set; }
        [MaxLength(50, ErrorMessage = "Created By must be equal or less than 50 characters")]
        public string CreatedBy { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ListRole { get; set; }
        public List<string> PostRole { get; set; }    

    }
}

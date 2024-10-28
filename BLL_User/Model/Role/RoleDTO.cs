using System;
using System.ComponentModel.DataAnnotations;

namespace BLL_User.Model
{
    public class RoleDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Role must be has name")]
        [MaxLength(50,ErrorMessage = "Over max length character of role name")]
        public string RoleName { get; set; }
        [MaxLength(50,ErrorMessage = "Created By must be equal or less than 50 characters")]
        public string CreatedBy { get; set; }
        public System.DateTime CreatedTime { get; set; }
        [MaxLength(50, ErrorMessage = "Updated By must be equal or less than 50 characters")]
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
        [MaxLength(50, ErrorMessage = "Deleted By must be equal or less than 50 characters")]
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Required(ErrorMessage = "Role must be description")]
        public string Description { get; set; }
        public string ListPermission { get; set; }
        public string PostPermission {  get; set; }
    }
}

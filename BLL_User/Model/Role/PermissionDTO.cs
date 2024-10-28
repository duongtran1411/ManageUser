using System;
using System.ComponentModel.DataAnnotations;

namespace BLL_User.Model
{
    public class PermissionDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Over max length of name permission")]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public Nullable<long> ParentId { get; set; }
        [Required]
        public byte Type { get; set; }
        [MaxLength(50,ErrorMessage = "Created By must be equal or less than 50 character")]
        public string CreatedBy { get; set; }
        public System.DateTime CreatedTime { get; set; }
        [MaxLength(50, ErrorMessage = "Updated By must be equal or less than 50 character")]
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
        [MaxLength(50, ErrorMessage = "Deleted By must be equal or less than 50 character")]
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

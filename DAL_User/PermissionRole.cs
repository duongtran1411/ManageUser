//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL_User
{
    using System;
    using System.Collections.Generic;
    
    public partial class PermissionRole
    {
        public long Id { get; set; }
        public long PermissionId { get; set; }
        public long RoleId { get; set; }
    
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
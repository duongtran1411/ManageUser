using BLL_User.Enumeration;
using System.Collections.Generic;
using BLL_User.Model;
using System;

namespace BLL_User.Extension
{
    public static class StaticData
    {
        public static List<EnumModel> PermissionTypes { get; set; }

        public static void GetPermissionType()
        {
            var enums = new Enums();
            PermissionTypes = new List<EnumModel>();
            foreach (Enums.PermissionType permissionType in Enum.GetValues(typeof (Enums.PermissionType)))
            {
                PermissionTypes.Add(new EnumModel { Key = (int)permissionType , Value = enums.GetEnumDescription(permissionType)});
            }
        }
    }
}

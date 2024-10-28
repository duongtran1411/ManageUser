using System.ComponentModel;

namespace BLL_User.Enumeration
{
    public class Enums
    {
        public enum PermissionType
        {
            [Description("Level 1")]
            Type1 = 1,
            [Description("Level 2")]
            Type2 = 2,
            [Description("Level 3")]
            Type3 = 3
        }

        public string GetEnumDescription(PermissionType value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var atttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute),false);
            return atttributes.Length > 0 ? atttributes[0].Description : value.ToString();
        }
    }
}

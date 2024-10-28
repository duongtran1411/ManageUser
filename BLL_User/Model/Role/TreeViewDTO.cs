using System.Collections.Generic;

namespace BLL_User.Model.Role
{
    public class TreeViewDTO
    {
        public string title { get; set; }
        public long key { get; set; }
        public List<TreeViewDTO> children { get; set; }
        public bool? preselected { get; set; }
        public bool expanded { get; set; }
    }
}

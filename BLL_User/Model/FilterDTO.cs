using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_User.Model
{
    public class FilterDTO
    {
        public int SkipCount { get; set; }
        public int PageCount { get; set; } = 10;
        public string FilterSearch { get; set; }

    }
}

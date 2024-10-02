using System;

namespace BLL_User.Model
{
    public class UserDTO
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public System.DateTime created_time { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_User;
using BLL_User.Model;
using BLL_User.DTO;
using System.Diagnostics;


namespace BLL_User
{

    public class CRUDUser
    {
        private UserModel _usermodel = new UserModel();
        public List<UserDTO> GetUsers()
        {
            List<UserDTO> list = _usermodel.GetAllUser();
            return list;
        }

        public void AddUserToList(UserDTO user)
        {
            _usermodel.InsertUser(user);
        }

        public void RemoveUser(int id)
        {
            _usermodel.DeleteUser(id);
        }

        public void EditUser(UserDTO user)
        {
            _usermodel.EditUser(user);
        }

        public UserDTO GetUserById(int id)
        {
           UserDTO user = _usermodel.GetUserById(id);
           return user;
        }

       public bool DupplicateUser(UserDTO user)
        {
            if (_usermodel.IsExistedUser(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AllowEdit(UserDTO user)
        {
            if (_usermodel.DupplicateEditUser(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}

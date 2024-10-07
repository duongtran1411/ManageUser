using DAL_User;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL_User.Extension;
using BLL_User.Model;

namespace BLL_User.BUS
{
    public class UserServices : BaseServices<User>
    {
        public List<UserDTO> GetUserByPaging(FilterDTO filter,out int total)
        {
            var query = GetAllList(a => string.IsNullOrEmpty(filter.FilterSearch) || (!string.IsNullOrEmpty(filter.FilterSearch) && a.user_name.ToLower().Contains(filter.FilterSearch.ToLower())));
            total = query.Count();
            if(query != null)
            {
                var paging = query.OrderBy(u => u.id).Skip(filter.SkipCount).Take(filter.PageCount).ToList();
                var result = Mapper.Map<List<User>, List<UserDTO>>(paging);
                return result;
            }
            return null;
        }

        public bool CreateUser(UserDTO input)
        {
            bool IsUpdateOrCreate = true;
            var query = FirstOrDeFault(u => u.user_name.Equals(input.UserName));
            if (query != null)
            {
                return !IsUpdateOrCreate;
            }
            var user = Mapper.Map<UserDTO, User>(input);
            user.password = SecurityExtension.EncryptMD5(input.Password);
            user.created_time = DateTime.Now;
            Insert(user);
            return IsUpdateOrCreate;
        }

        public bool EditUser(UserDTO input)
        {
            bool isUpdated = false;
            var query = FirstOrDeFault(u => u.user_name.Equals(input.UserName));
            var user = FirstOrDeFault(u => u.id == input.Id);
            if (query != null)
            {
                if(query.id == input.Id)
                {
                    user.first_name = input.FirstName;
                    user.last_name = input.LastName;
                    user.email = input.Email;
                    user.phone = input.Phone;   
                    user.password = SecurityExtension.EncryptMD5(input.Password);
                    user.created_time = input.CreatedTime;
                    Update(user);
                    isUpdated = true;
                    return isUpdated;
                }
                else
                {
                    return isUpdated;
                }
            }
            else
            {
                return isUpdated;
            }
            
        }

        public void DeleteUserById(int id, string message)
        {
            message = "";
            var user = FirstOrDeFault(u => u.id == id);
            if (user != null)
            {
                Delete(user);
                message = "Deleted successful";
            }
            else
            {
                message = "User has been deleted or not existed";
            }
        }

        public UserDTO GetUserByUserNameAndPassword(LoginDTO loginDto)
        {
            var user = FirstOrDeFault(u => u.user_name.Equals(loginDto.UserName) && u.password.Equals(loginDto.Password));
            if (user == null)
            {
                return null;
            }
            return Mapper.Map<User, UserDTO>(user);
        }

        public UserDTO GetUserById(int id)
        {
            var user = FirstOrDeFault(u => u.id == id);
            if (user == null) return null;
            return Mapper.Map<User, UserDTO>(user);
        }
    }
}

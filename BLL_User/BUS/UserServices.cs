using DAL_User;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL_User.Extension;
using BLL_User.Model;
using System.Diagnostics;

namespace BLL_User.BUS
{
    public class UserServices : BaseServices<User>
    {
        public List<UserDTO> GetUserByPaging(FilterDTO filter,out int total)
        {
            var query = GetAllList(a => ((string.IsNullOrEmpty(filter.FilterSearch)) || (!string.IsNullOrEmpty(filter.FilterSearch))) && a.user_name.ToLower().Contains(filter.FilterSearch.ToLower()));
            var query2 = GetAllUser().Where(a => ((string.IsNullOrEmpty(filter.FilterSearch)) || (!string.IsNullOrEmpty(filter.FilterSearch))) && a.user_name.ToLower().Contains(filter.FilterSearch.ToLower()));
            total = query2.Count();
            if(query != null)
            {
                var paging = query2.OrderBy(u => u.id).Skip(filter.SkipCount).Take(filter.PageCount).ToList();
                //var result = Mapper.Map<List<User>, List<UserDTO>>(paging);
                return paging;
            }
            return null;
        }

        public List<UserDTO> GetAllUser()
        {
            return Mapper.Map<List<User>, List<UserDTO>>(GetAll().ToList());
        }
        public bool CreateUser(UserDTO input)
        {
            bool IsUpdateOrCreate = true;
            var query = dbContext.Users.FirstOrDefault(u => u.user_name.Equals(input.user_name));
            if (query != null)
            {
                return !IsUpdateOrCreate;
            }
            var user = Mapper.Map<UserDTO, User>(input);
            user.password = SecurityExtension.EncryptMD5(input.password);
            user.created_time = DateTime.Now;
            Insert(user);
            return IsUpdateOrCreate;
        }

        public bool EditUser(UserDTO input)
        {
            bool isUpdated = false;
            var query = dbContext.Users.FirstOrDefault(u => u.user_name.Equals(input.user_name));
            if(query != null)
            {
                if(query.id == input.id)
                {
                    var user = Mapper.Map<UserDTO, User>(input);
                    user.password = SecurityExtension.EncryptMD5(input.password);
                    user.created_time = input.created_time;
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
                var user = Mapper.Map<UserDTO, User>(input);
                user.password = SecurityExtension.EncryptMD5(input.password);
                user.created_time = input.created_time;
                Update(user);
                isUpdated = true;
                return isUpdated;
            }
        }

        public void DeleteUserById(int id, string message)
        {
            message = "";
            var user = dbContext.Users.Find(id);
            Debug.WriteLine("username: ", user.user_name);
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
            var user = dbContext.Users.FirstOrDefault(u => u.user_name.Equals(loginDto.userName) && u.password.Equals(loginDto.Password));
            if (user == null)
            {
                return null;
            }
            return Mapper.Map<User, UserDTO>(user);
        }

        public UserDTO GetUserById(int id)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.id == id);
            if (user == null) return null;
            return Mapper.Map<User, UserDTO>(user);
        }
    }
}

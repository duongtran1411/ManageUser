using DAL_User;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL_User.Extension;
using BLL_User.Model;
using System.Data.Entity;
using System.Web;

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

        

        public bool CreateOrEdit(UserDTO input, out string errorMessage)
        {
            errorMessage = "";
            if(input.Id != 0)
            {
                var query = FirstOrDeFault(u => u.id == input.Id);
                if(query != null)
                {
                    query.first_name = input.FirstName;
                    query.last_name = input.LastName;
                    query.email = input.Email;
                    query.phone = input.Phone;
                    Update(query);
                    return true;
                }
                else
                {
                    errorMessage = "User is not exist or deleted";
                    return false;
                }
            }
            else
            {
                var isExistedUserName = FirstOrDeFault(u => u.user_name.Equals(input.UserName));    
                if(isExistedUserName != null)
                {
                    errorMessage = "Username is existed";
                    return false;
                }
                else
                {
                    var user = Mapper.Map<UserDTO, User>(input);
                    user.password = SecurityExtension.EncryptMD5(input.Password);
                    user.created_time = DateTime.Now;
                    Insert(user);
                    return true;
                }
            }
            
        }

        public bool DeleteUserById(int id, out string message)
        {
            message = "";
            var user = FirstOrDeFault(u => u.id == id);
            if (user != null)
            {
                using (var transaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
                {
                    try
                    {
                        Delete(user);
                        transaction.Commit();
                        message = "Deleted successful";
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        message = ex.Message;
                        return false;
                    }
                }
            }
            else
            {
                message = "User has been deleted or not existed";
                return false;
            }
        }

        public UserDTO GetUserByUserNameAndPassword(LoginDTO loginDto, out string errorMessage)
        {
            errorMessage = "";
            var password = SecurityExtension.EncryptMD5(loginDto.Password);
            var user = FirstOrDeFault(u => u.user_name.Equals(loginDto.UserName) && u.password.Equals(password));
            if (user == null)
            {
                errorMessage = "Username or password incorrect";
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

        public bool ChangePassword(ChangePassDto input, out string errorMessage)
        {
            errorMessage = "";
            var user = FirstOrDeFault(u => u.id == input.UserId );
            if(user != null)
            {
                var password = SecurityExtension.EncryptMD5(input.Password);
                if (user.password.Equals(password))
                {
                    user.password = SecurityExtension.EncryptMD5(input.NewPassword);
                    Update(user);
                    return true;
                }
                else
                {
                    errorMessage = "Current password is incorrect";
                    return false;
                }
            }
            else
            {
                errorMessage = "User is not existed or deleted";
                return false;
            }
        }
    }
}

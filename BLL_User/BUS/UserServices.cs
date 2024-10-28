using DAL_User;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL_User.Extension;
using BLL_User.Model;
using System.Data.Entity;

namespace BLL_User.BUS
{
    public class UserServices : BaseServices<User>
    {
        public List<UserDTO> GetUserByPaging(FilterDTO filter,out int total)
        {
            var listUser = GetAllList(a => string.IsNullOrEmpty(filter.FilterSearch) || (!string.IsNullOrEmpty(filter.FilterSearch) && a.UserName.ToLower().Contains(filter.FilterSearch.ToLower())));
            total = listUser.Count();
            if(listUser != null)
            {
                var paging = listUser.OrderBy(u => u.Id).Skip(filter.SkipCount).Take(filter.PageCount).ToList();
                var result = Mapper.Map<List<User>, List<UserDTO>>(paging);
                return result;
            }
            return null;
        }

        public bool CreateOrEdit(UserDTO input, out string errorMessage, long userId)
        {
            errorMessage = "";
            if(input.Id != 0)
            {
                var user = FirstOrDeFault(u => u.Id == input.Id);
                if(user != null)
                {
                    user.FirstName = input.FirstName;
                    user.LastName = input.LastName;
                    user.Email = input.Email;
                    user.Phone = input.Phone;
                    user.UpdatedBy = GetUserById(userId).UserName;
                    user.UpdatedTime = DateTime.Now;
                    if (input.PostRole != null)
                    {
                        var existUserRoles = dbContext.UserRoles.Where(a => a.UserId == input.Id).ToList();
                        dbContext.UserRoles.RemoveRange(existUserRoles);
                        var userRoles = new List<UserRole>();    
                        foreach(var role in input.PostRole)
                        {
                            var userRole = new UserRole
                            {
                                RoleId = Convert.ToInt64(role),
                                UserId = input.Id
                            };
                            userRoles.Add(userRole);
                        }
                        dbContext.UserRoles.AddRange(userRoles);
                    }
                    Update(user);
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
                var checkUserNameExist = FirstOrDeFault(u => u.UserName.Equals(input.UserName));    
                if(checkUserNameExist != null)
                {
                    errorMessage = "Username is existed";
                    return false;
                }
                else
                {
                    var user = Mapper.Map<UserDTO, User>(input);
                    user.Password = SecurityExtension.EncryptMD5(input.Password);
                    user.CreatedTime = DateTime.Now;
                    user.CreatedBy = GetUserById(userId).UserName;
                    if (input.PostRole != null)
                    {
                        var userRoles = new List<UserRole>();
                        foreach(var item in input.PostRole)
                        {
                            var userRole = new UserRole
                            {
                                RoleId = Convert.ToInt64(item),
                                UserId = input.Id
                            };
                            userRoles.Add(userRole);
                        }
                        dbContext.UserRoles.AddRange(userRoles);
                        dbContext.SaveChanges();
                    }
                    Insert(user);
                    return true;
                }
            }
            
        }

        public bool RegisterUser(UserDTO input, out string errorMessage)
        {
            errorMessage = "";
            var userNameExist = FirstOrDeFault(u => u.UserName.Equals(input.UserName));
            if(userNameExist != null)
            {
                errorMessage = "Username is existed";
                return false;
            }
            else
            {
                var user = Mapper.Map<UserDTO, User>(input);
                user.Password = SecurityExtension.EncryptMD5(input.Password);
                user.CreatedTime = DateTime.Now;
                user.CreatedBy = "Admin";
                Insert(user);
                return true;
            }
        }

        public bool DeleteUserById(int id, out string message, long userId)
        {
            message = "";
            var user = FirstOrDeFault(u => u.Id == id);
            if (user != null)
            {
                using (var transaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
                {
                    try
                    {
                        if (user.UserName.Equals("Admin"))
                        {
                            message = "Can not deleted account admin";
                            return false;
                        }
                        user.IsDeleted = true;
                        user.UpdatedBy = GetUserById(userId).UserName;
                        user.UpdatedTime = DateTime.Now;
                        Update(user);
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
            var user = FirstOrDeFault(u => u.UserName.Equals(loginDto.UserName) && u.Password.Equals(password));
            if (user == null)
            {
                errorMessage = "Username or password incorrect";
                return null;
            }
            return Mapper.Map<User, UserDTO>(user);
        }

        public UserDTO GetUserById(long id)
        {
            var user = FirstOrDeFault(u => u.Id == id);
            if (user == null) return null;
            return Mapper.Map<User, UserDTO>(user);
        }

        public bool ChangePassword(ChangePassDto input, out string errorMessage, long userId)
        {
            errorMessage = "";
            var user = FirstOrDeFault(u => u.Id == input.UserId );
            if(user != null)
            {
                var password = SecurityExtension.EncryptMD5(input.Password);
                if (user.Password.Equals(password))
                {
                    user.Password = SecurityExtension.EncryptMD5(input.NewPassword);
                    user.UpdatedBy = GetUserById(userId).UserName;
                    user.UpdatedTime = DateTime.Now;
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

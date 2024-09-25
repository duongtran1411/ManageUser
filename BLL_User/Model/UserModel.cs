using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_User;
using BLL_User.DTO;
using System.Diagnostics;
using System.Configuration;
using System.CodeDom;

namespace BLL_User.Model
{
    public class UserModel
    {
        private AssignmentEntities2 _context = new AssignmentEntities2();
        public List<UserDTO> GetAllUser()
        {
            var query = _context.Users.Select(x =>
             new UserDTO
             {
                 id = x.id,
                 userName = x.user_name,
                 password = x.password,
                 firstName = x.first_name,
                 lastName = x.last_name,
                 email = x.email,
                 phone = x.phone,
                 createdTime = x.created_time
             }
            ).ToList();

            return query;
        }
        public void InsertUser(UserDTO user)
        {
            try
            {
                User userContext = new User
                {
                    user_name = user.userName,
                    password = user.password,
                    first_name = user.firstName,
                    last_name = user.lastName,
                    email = user.email,
                    phone = user.phone,
                    created_time = DateTime.Now
                };
                _context.Users.Add(userContext);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void DeleteUser(int id)
        {
            var query = _context.Users.Find(id);
            _context.Users.Remove(query);
            _context.SaveChanges();
        }

        public void EditUser(UserDTO userDto)
        {
            try
            {
                var existingUser = _context.Users.Single(u => u.id == userDto.id);
                if (existingUser != null)
                {
                    existingUser.id = userDto.id;
                    existingUser.user_name = userDto.userName;
                    existingUser.password = userDto.password;
                    existingUser.first_name = userDto.firstName;
                    existingUser.last_name = userDto.lastName;
                    existingUser.email = userDto.email;
                    existingUser.phone = userDto.phone;
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public UserDTO GetUserById(int id)
        {
            var query = _context.Users.Find(id);
            UserDTO user = new UserDTO
            {
                id = query.id,
                userName = query.user_name,
                password = query.password,
                lastName = query.last_name,
                firstName = query.first_name,
                phone = query.phone,
                email = query.email,
                createdTime = DateTime.Now
            };
            return user;
        }

        public bool IsExisted (UserDTO user)
        {
            var result = _context.Users.FirstOrDefault(u => user.userName.Equals(u.user_name));
            if(result != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsExistedUser(UserDTO user)
        {
            var result = _context.Users.FirstOrDefault(u => user.userName.Equals(u.user_name));
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DupplicateEditUser(UserDTO user)
        {
            var result = _context.Users.FirstOrDefault(u => u.id == user.id);
            if(result != null)
            {
                return true ;
            }
            else {
                return false;
            }
        }
    }
}

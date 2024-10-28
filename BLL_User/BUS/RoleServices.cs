using BLL_User.Model;
using DAL_User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace BLL_User.BUS
{
    public class RoleServices : BaseServices<Role>
    {
        public List<SelectListItem> GetStaticRole()
        {
            //return GetAllList(u => !u.IsDeleted).Select(a => new SelectListItem { Text = a.RoleName, Value = a.Id.ToString() }).ToList();
            var result = dbContext.Roles
                      .Where(x => !x.IsDeleted)
                      .AsEnumerable()
                      .Select(x => new SelectListItem { Text = x.RoleName, Value = x.Id.ToString()})
                      .ToList();
            return result;
        }
        public List<RoleDTO> GetRolePaging(FilterDTO filter, out int total)
        {
            var listRole = GetAllList(a => !a.IsDeleted && (string.IsNullOrEmpty(filter.FilterSearch) || a.RoleName.ToLower().Contains(filter.FilterSearch.ToLower())));
            total = listRole.Count();
            if (listRole != null)
            {
                var paging = listRole.OrderBy(u => u.Id).Skip(filter.SkipCount).Take(filter.PageCount).ToList();
                var result = Mapper.Map<List<Role>, List<RoleDTO>>(paging);
                return result;
            }
            return null;
        }

        public RoleDTO GetRoleById(long id)
        {
            var role = FirstOrDeFault(a => a.Id == id);
            if (role != null)
            {
                var result = Mapper.Map<Role, RoleDTO>(role);
                return result;
            }
            else
            {
                return null;    
            }
        }

        public bool CreatedOrEdit(RoleDTO roleDto, out string errorMessage, long userId)
        {
             errorMessage = "";
            UserServices userService = new UserServices();
            if (roleDto.Id != 0)
            {
                var checkRoleIdExist = FirstOrDeFault(u => u.RoleName.Equals(roleDto.RoleName));
                if (checkRoleIdExist.RoleName != null && checkRoleIdExist.Id != roleDto.Id)
                {
                    errorMessage = "Role name has existed";
                    return false;
                    
                }
                var role = FirstOrDeFault(u => u.Id == roleDto.Id && !u.IsDeleted);
                if(role.Id != null)
                {
                    role.RoleName = roleDto.RoleName;
                    if (!string.IsNullOrEmpty(roleDto.PostPermission))
                    {
                        var permissions = roleDto.PostPermission.Split(',').Distinct().ToList();
                        var existedPermission = dbContext.PermissionRoles.Where(u => u.RoleId ==  roleDto.Id).ToList();
                        dbContext.PermissionRoles.RemoveRange(existedPermission);
                        foreach(var permission in permissions)
                        {
                            if (permission.Contains("root")) continue;
                            var permissionClaim = new PermissionRole
                            {
                                RoleId = role.Id,
                                PermissionId = Convert.ToInt64(permission)
                            };
                            dbContext.PermissionRoles.Add(permissionClaim);
                        }
                    }
                    checkRoleIdExist.Description = roleDto.Description;
                    checkRoleIdExist.UpdatedBy = userService.GetUserById(userId).UserName;
                    checkRoleIdExist.UpdatedTime = DateTime.Now;
                    Update(checkRoleIdExist);
                    return true;
                }
                else
                {
                    errorMessage = "Role not existed or deleted";
                    return false;
                }
            }
            else
            {
                var checkRoleNameExist = FirstOrDeFault(r => r.RoleName.Equals(roleDto.RoleName));
                if(checkRoleNameExist != null)
                {
                    errorMessage = "role name has existed";
                    return false;
                }
                else
                {
                    var role = Mapper.Map<RoleDTO,Role>(roleDto);
                    role.CreatedBy = userService.GetUserById(userId).UserName;
                    role.CreatedTime = DateTime.Now;
                    Insert(role);
                    if (string.IsNullOrEmpty(roleDto.PostPermission))
                    {
                        var permissions = roleDto.PostPermission.Split(',').Distinct().ToList();
                        foreach(var permission in permissions)
                        {
                            var permissionClaim = new PermissionRole
                            {
                                PermissionId = Convert.ToInt64(permission),
                                RoleId = role.Id
                            };
                            dbContext.PermissionRoles.Add(permissionClaim);
                        }
                        dbContext.SaveChanges();    
                    }
                    return true;
                }
            }
        }

        public bool DeletedRole(long roleId, long userId, out string errorMessage)
        {
            errorMessage = "";
            var role = FirstOrDeFault(u => u.Id == roleId && !u.IsDeleted);
            if(role != null)
            {
                if (role.RoleName.Equals("Admin"))
                {
                    errorMessage = "Can not delete role admin";
                    return false;
                }
                else
                {
                    role.IsDeleted = true;
                    UserServices userService = new UserServices();
                    role.DeletedBy = userService.GetUserById(userId).UserName;
                    role.DeletedTime = DateTime.Now;
                    Update(role);
                    return true;
                }
            }
            else
            {
                errorMessage = "Role is not existed or has been deleted";
                return false;
            }
        }
    }
}

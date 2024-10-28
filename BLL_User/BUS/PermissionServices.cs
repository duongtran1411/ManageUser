using BLL_User.Model;
using BLL_User.Model.Role;
using DAL_User;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BLL_User.BUS
{
    public class PermissionServices : BaseServices<Permission>
    {
        public List<TreeViewDTO> GetAllPermission(List<PermissionDTO> permissions = null)
        {
            var result = new List<TreeViewDTO>();
            if (permissions == null)
            {
                permissions = Mapper.Map<List<Permission>, List<PermissionDTO>>(GetAll().ToList());
            }

            foreach (var item in permissions.Where(u => u.Type == 1).ToList())
            {
                var permissionDto = new TreeViewDTO()
                {
                    title = item.Name,
                    key = item.Id,
                    expanded = true,
                    preselected = false,
                    children = new List<TreeViewDTO>()
                };
                var itemsLv2 = permissions.Where(u => u.ParentId == item.Id).ToList();
                if (itemsLv2.Any())
                {
                    foreach (var itemLv2 in itemsLv2)
                    {
                        var permissionLv2Dto = new TreeViewDTO()
                        {
                            title = itemLv2.Name,
                            key = itemLv2.Id,
                            expanded = true,
                            preselected = false,
                            children = new List<TreeViewDTO>()
                        };

                        var itemsLv3 = permissions.Where(u => u.ParentId == itemLv2.Id).ToList();
                        if (itemsLv3.Any())
                        {
                            foreach (var itemLv3 in itemsLv3)
                            {
                                var permissionLv3Dto = new TreeViewDTO()
                                {
                                    title = itemLv3.Name,
                                    key = itemLv3.Id,
                                    expanded = true,
                                    preselected = false,
                                    children = new List<TreeViewDTO>()
                                };
                                permissionLv2Dto.children.Add(permissionLv3Dto);
                            }
                        }
                        else
                        {
                            permissionLv2Dto.children = null;
                        }
                        permissionDto.children.Add(permissionLv2Dto);
                    }
                }
                else
                {
                    permissionDto.children = null;
                }
                result.Add(permissionDto);
            }
            return result;
        }

        public List<PermissionDTO> GetPermissionByType(List<byte> types)
        {
            return Mapper.Map<List<Permission>, List<PermissionDTO>>(GetAllList(u => types.Contains(u.Type)).ToList());
        }

        public bool CreateOrEdit(PermissionDTO input, out string errorMessage, long userId)
        {
            UserServices userServices = new UserServices();
            errorMessage = "";
            var checkNameExisted = FirstOrDeFault(u => u.Name.Equals(input.Name) && !input.IsDeleted);

            if (input.ParentId != null)
            {
                var checkParentExisted = FirstOrDeFault(u => u.Id == input.ParentId && !input.IsDeleted);
                if (checkParentExisted == null)
                {
                    errorMessage = "Permission is deleted or not existed";
                    return false;
                }
                if (checkParentExisted.Type >= input.Type)
                {
                    errorMessage = $"can not grant permissions for {input.Type} in {checkParentExisted.Type}.Please try again";
                    return false;
                }
            }
            if (input.Id != 0)
            {
                if (checkNameExisted != null && checkNameExisted.Id == input.Id)
                {
                    errorMessage = "Permission has existed";
                    return false;
                }
                var permission = FirstOrDeFault(u => u.Id == input.Id && !input.IsDeleted);
                if (permission != null)
                {
                    permission.Name = input.Name;
                    permission.ParentId = input.ParentId;
                    permission.Code = input.Code;
                    permission.UpdatedTime = DateTime.Now;
                    permission.UpdatedBy = userServices.GetUserById(userId).UserName;
                    Update(permission);
                    return true;
                }
                errorMessage = "Permission not existed or incorrect information";
                return false;
            }
            else
            {
                if (checkNameExisted != null)
                {
                    errorMessage = "Permission name has existed";
                    return false;
                }
                else
                {
                    var user = Mapper.Map<PermissionDTO, Permission>(input);
                    user.CreatedTime = DateTime.Now;
                    user.CreatedBy = userServices.GetUserById(userId).UserName;
                    Insert(user);
                    return true;
                }
            }
        }

        public bool DeletedPermission(long permissionId, long userId, out string errorMessage)
        {
            errorMessage = "";
            UserServices userService = new UserServices();
            var childPermission = GetAllList(a => a.ParentId == permissionId && !a.IsDeleted).ToList();
            if (childPermission.Any())
            {
                errorMessage = "You need deleted child permission before deleted parent permission !";
                return false;
            }
            var permission = FirstOrDeFault(a => a.Id == permissionId && !a.IsDeleted);
            if (permission != null)
            {
                permission.IsDeleted = true;
                permission.DeletedTime = DateTime.Now;
                permission.DeletedBy = userService.GetUserById(userId).UserName;
                Update(permission);
                return true;
            }
            else
            {
                errorMessage = "Permission is not existed or deleted !";
                return false;
            }
        }

        public PermissionDTO GetPermissionById(long Id, out string errorMessage) {
            errorMessage = "";
            var permission = FirstOrDeFault(u => u.Id == Id);
            if(permission != null)
            {
                return Mapper.Map<Permission, PermissionDTO>(permission);
            }
            errorMessage = "Permission does not existed or deleted";
            return null;
        }  

        public List<TreeViewDTO> GetPermissionSelected(List<PermissionDTO> permissions, List<long> selectedPermission)
        {
            var result = new List<TreeViewDTO>();

            foreach (var permission in permissions.Where(u => u.Type == 1).ToList())
            {
                var permissionLv1 = new TreeViewDTO
                {
                    key = permission.Id,
                    title = permission.Name,
                    expanded = true,
                    preselected = CheckSelectedPermission(selectedPermission, permission),
                    children = new List<TreeViewDTO>()
                };
                var itemsLv2 = permissions.Where(u => u.ParentId == permission.Id).ToList();
                if (itemsLv2.Any())
                {
                    foreach (var itemLv2 in itemsLv2)
                    {
                        var permissionLv2 = new TreeViewDTO
                        {
                            key = itemLv2.Id,
                            title = itemLv2.Name,
                            expanded = true,
                            preselected = CheckSelectedPermission(selectedPermission, itemLv2),
                            children = new List<TreeViewDTO>()
                        };
                        var itemsLv3 = permissions.Where(u => u.ParentId ==  itemLv2.Id).ToList();
                        if (itemsLv3.Any())
                        {
                            foreach(var itemLv3 in itemsLv3)
                            {
                                var permissionLv3 = new TreeViewDTO
                                {
                                    key = itemLv3.Id,
                                    title = itemLv3.Name,
                                    expanded = true,
                                    preselected = CheckSelectedPermission(selectedPermission, itemLv3),
                                    children = new List<TreeViewDTO>()
                                };
                                permissionLv2.children.Add(permissionLv3);  
                            }
                        }
                        else
                        {
                            permissionLv2.children = null;
                        }
                        permissionLv1.children.Add(permissionLv2);
                    }
                }
                else 
                {
                    permissionLv1.children = null;
                }
                result.Add(permissionLv1);
            }

            return result;
        }
        private bool CheckSelectedPermission(List<long> selectedPermissionIds, PermissionDTO permission)
        {
            return selectedPermissionIds.Any(a => a == permission.Id);
        }

        public string GetStaticPermission(long roleId)
        {
            var listPermission = "";
            var permission = Mapper.Map<List<Permission>, List<PermissionDTO>>(GetAllList(u => !u.IsDeleted).ToList());
            if(roleId == 0)
            {
                 listPermission = JsonConvert.SerializeObject(GetAllPermission(permission));
            }
            else
            {
                var selectedPermission = (from pr in dbContext.PermissionRoles
                                         join p in dbContext.Permissions on pr.PermissionId equals p.Id into leftjoin
                                         from rs in leftjoin.DefaultIfEmpty()
                                         where pr.RoleId == roleId
                                         select pr.PermissionId).ToList();
                listPermission = JsonConvert.SerializeObject(GetPermissionSelected(permission, selectedPermission));
            }
            return listPermission;
        }

        public List<PermissionDTO> GetAllLevelPermission(List<PermissionDTO> permissions)
        {
            var permission = Mapper.Map<List<Permission>, List<PermissionDTO>>(GetAllList(a => (a.Type == 1 || a.Type == 2) && !a.IsDeleted).ToList());
            var childPermission = permissions.Where(a => a.Type == 2 || a.Type == 3).ToList();
            var result = new List<PermissionDTO>();
            foreach(var item in permissions)
            {
                result.Add(item);
                switch (item.Type)
                {
                    case 2:
                        var permissionLv1 = permission.FirstOrDefault(a => a.Id == item.ParentId);
                        if (permissionLv1 != null && !result.Any(a => a.Id == permissionLv1.Id)) result.Add(permissionLv1);
                        break;
                    case 3:
                        var permissionLv2 = permission.FirstOrDefault(a => a.Id == item.ParentId);
                        if (permissionLv2 != null && !result.Any(a => a.Id == permissionLv2.Id))
                        {
                            result.Add(permissionLv2);
                            permissionLv1 = permission.FirstOrDefault(a => a.Id == permissionLv2.ParentId);
                            if (permissionLv1 != null && !result.Any(a => a.Id == permissionLv1.Id)) result.Add(permissionLv1);
                        }
                        break;

                }
            }
            return result.Distinct().ToList();
        }
        public List<TreeViewDTO> GetPermissionbyRoleId(string roleId)
        {
            var result = new List<TreeViewDTO>();
            if (!string.IsNullOrEmpty(roleId))
            {
                var listId = roleId.Split(',').Select(u => Convert.ToInt64(u)).ToList();
                var permission = (from pr in dbContext.PermissionRoles
                                 join p in dbContext.Permissions on pr.PermissionId equals p.Id into leftjoin
                                 from rs in leftjoin.DefaultIfEmpty()
                                 where (pr.RoleId != 0 && listId.Contains(pr.RoleId))
                                 select new PermissionDTO
                                 {
                                     Name = rs.Name,
                                     Code = rs.Code,
                                     Type = rs.Type,
                                     ParentId = rs.ParentId,
                                     Id = rs.Id
                                 }).Distinct().ToList();
                var AllLevelPermission = GetAllLevelPermission(permission);
                result = GetAllPermission(AllLevelPermission);
            }
            return result;
        }

        public List<PermissionDTO> GetPermissionByUserId(long userId)
        {
            var roles = (from ur in dbContext.UserRoles
                        join r in dbContext.Roles on ur.RoleId equals r.Id
                        join u in dbContext.Users on ur.UserId equals u.Id
                        where u.Id == userId
                        select r.Id).ToList();
            var permissions = (from pr in dbContext.PermissionRoles
                              join p in dbContext.Permissions on pr.PermissionId equals p.Id
                              where (pr.RoleId != 0 && roles.Contains(pr.RoleId))
                              select new PermissionDTO
                              {
                                  Name = p.Name,
                                  Code = p.Code,
                                  Type = p.Type,
                                  ParentId = p.ParentId,
                                  Id = p.Id
                              }).Distinct().ToList();
            return permissions;
        }
    }
}

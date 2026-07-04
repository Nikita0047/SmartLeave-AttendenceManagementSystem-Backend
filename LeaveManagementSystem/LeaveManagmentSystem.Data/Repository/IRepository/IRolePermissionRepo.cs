using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.IRepository
{
  public interface IRolePermissionRepo
    {
        Task<List<string>> GetPermissionNamesForRoleAsync(UserRole role);
        Task<List<RolePermission>> GetByRoleAsync(UserRole role);
        Task DeleteByRoleAsync(UserRole role);
    }
}

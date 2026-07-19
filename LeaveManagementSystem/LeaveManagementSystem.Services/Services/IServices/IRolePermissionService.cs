using LeaveManagementSystem.Core.Dtos.Permissions;
using LeaveManagementSystem.Core.Dtos.RolePermission;
using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services.IServices
{
    // Core/Interfaces/Services/IRolePermissionService.cs
    public interface IRolePermissionService
    {
        Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync();
        Task<RolePermissionResponseDto> GetByRoleAsync(UserRole role);
        Task UpdateAsync(RolePermissionDto dto);
    }
}

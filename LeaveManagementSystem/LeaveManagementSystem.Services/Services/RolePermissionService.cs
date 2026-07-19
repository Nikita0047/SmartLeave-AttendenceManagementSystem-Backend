using LeaveManagementSystem.Core.Dtos.Permissions;
using LeaveManagementSystem.Core.Dtos.RolePermission;
using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Core.Enum;
using LeaveManagementSystem.Services.Services.IServices;
using LeaveManagmentSystem.Data.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services
{
    // Application/Services/RolePermissionService.cs
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IUnitOfWork _uow;
       

        public RolePermissionService(IUnitOfWork uow) => _uow = uow;

        public Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RolePermissionResponseDto> GetByRoleAsync(UserRole role)
        {
            var permissions = await _uow.RolePermissions
                .GetPermissionNamesForRoleAsync(role);

            return new RolePermissionResponseDto
            {
                Role = role.ToString(),
                Permissions = permissions
            };
        }

        public async Task UpdateAsync(RolePermissionDto dto)
        {
            // 1 — validate all permission names against DB
            var permissions = await _uow.Permissions
                .GetByNamesAsync(dto.Permissions);

            // if any name not found in DB — reject the whole request
            if (permissions.Count != dto.Permissions.Count)
            {
                var invalid = dto.Permissions
                    .Except(permissions.Select(p => p.Name))
                    .ToList();

                throw new InvalidOperationException(
                    $"Invalid permission names: {string.Join(", ", invalid)}");
            }

            // 2 — delete all existing permissions for this role
            await _uow.RolePermissions.DeleteByRoleAsync(dto.Role);

            // 3 — insert new ones using real PermissionIds from DB
            foreach (var permission in permissions)
            {
                await _uow.RolePermissions.AddAsync(new RolePermission
                {
                    Id = Guid.NewGuid(),
                    Role = dto.Role,
                    PermissionId = permission.Id   // ← real Guid from DB, not random
                });
            }

            await _uow.SaveChangesAsync();
        }
    }
}

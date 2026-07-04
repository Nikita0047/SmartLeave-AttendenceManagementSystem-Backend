using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Core.Enum;
using LeaveManagmentSystem.Data.Data;
using LeaveManagmentSystem.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeaveManagmentSystem.Data.Repository.Repo.RolePermissionRepo;

namespace LeaveManagmentSystem.Data.Repository.Repo
{
    public class RolePermissionRepo: Repository<RolePermission>, IRolePermissionRepo
    {
      
            public RolePermissionRepo(AppDbContext context) : base(context) { }

            public async Task<List<string>> GetPermissionNamesForRoleAsync(UserRole role)
                => await _dbSet
                    .Where(rp => rp.Role == role)
                    .Include(rp => rp.Permission)
                    .Select(rp => rp.Permission.Name)
                    .ToListAsync();

            public async Task<List<RolePermission>> GetByRoleAsync(UserRole role)
                => await _dbSet
                    .Where(rp => rp.Role == role)
                    .Include(rp => rp.Permission)
                    .ToListAsync();

            public async Task DeleteByRoleAsync(UserRole role)
            {
                var existing = await _dbSet.Where(rp => rp.Role == role).ToListAsync();
                _dbSet.RemoveRange(existing);
            }
        }
    }
}

using LeaveManagementSystem.Core.Entities;
using LeaveManagmentSystem.Data.Data;
using LeaveManagmentSystem.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.Repo
{
    // Infrastructure/Repositories/PermissionRepository.cs
    public class PermissionRepo: Repository<Permissions>, IPermissionRepo
    {
        public PermissionRepo(AppDbContext context) : base(context) { }

        public async Task<Permissions?> GetByNameAsync(string name)
            => await _dbSet.FirstOrDefaultAsync(p => p.Name == name);

        public async Task<List<Permissions>> GetByNamesAsync(List<string> names)
            => await _dbSet
                .Where(p => names.Contains(p.Name))
                .ToListAsync();

        
    }
}

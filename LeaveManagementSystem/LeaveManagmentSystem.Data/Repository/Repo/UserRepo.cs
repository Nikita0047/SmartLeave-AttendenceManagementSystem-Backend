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
    public class UserRepo:Repository<User>, IUserRepo   
    {
        public UserRepo(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
            => await _dbSet
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);

        public async Task<User?> GetByIdWithDepartmentAsync(Guid id)
            => await _dbSet
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<User>> GetByDepartmentAsync(Guid departmentId)
            => await _dbSet
                .Where(u => u.DepartmentId == departmentId && u.IsActive)
                .ToListAsync();

        public async Task<bool> EmailExistsAsync(string email)
            => await _dbSet.AnyAsync(u => u.Email == email);
    }
}

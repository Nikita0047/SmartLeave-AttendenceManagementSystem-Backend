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
using static LeaveManagmentSystem.Data.Repository.Repo.LeaveRequestRepo;

namespace LeaveManagmentSystem.Data.Repository.Repo
{
    public class LeaveRequestRepo:Repository<LeaveRequest>,ILeaveRequestRepo
    {
        // Infrastructure/Repositories/LeaveRequestRepository.cs
    
        
            public LeaveRequestRepo(AppDbContext context) : base(context) { }

            public async Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId)
                => await _dbSet
                    .Where(lr => lr.EmployeeId == employeeId)
                    .Include(lr => lr.LeaveType)
                    .OrderByDescending(lr => lr.CreatedAt)
                    .ToListAsync();

            public async Task<IEnumerable<LeaveRequest>> GetByDepartmentAsync(
                Guid departmentId, LeaveStatus? status = null)
                => await _dbSet
                    .Include(lr => lr.Employee).ThenInclude(u => u.Department)
                    .Include(lr => lr.LeaveType)
                    .Where(lr => lr.Employee.DepartmentId == departmentId
                              && (status == null || lr.Status == status))
                    .OrderByDescending(lr => lr.CreatedAt)
                    .ToListAsync();

            public async Task<IEnumerable<LeaveRequest>> GetAllWithDetailsAsync(LeaveStatus? status = null)
                => await _dbSet
                    .Include(lr => lr.Employee).ThenInclude(u => u.Department)
                    .Include(lr => lr.LeaveType)
                    .Where(lr => status == null || lr.Status == status)
                    .OrderByDescending(lr => lr.CreatedAt)
                    .ToListAsync();

            public async Task<LeaveRequest?> GetByIdWithDetailsAsync(Guid id)
                => await _dbSet
                    .Include(lr => lr.Employee).ThenInclude(u => u.Department)
                    .Include(lr => lr.LeaveType)
                    .FirstOrDefaultAsync(lr => lr.Id == id);
        }
    }
}

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
    public class LeaveBalanceRepo:Repository<LeaveBalance>, ILeaveBalanceRepo
    {
        public LeaveBalanceRepo(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<LeaveBalance>> GetByEmployeeAndYearAsync(Guid employeeId, int year)
            => await _dbSet
                .Include(lb => lb.LeaveType)
                .Where(lb => lb.EmployeeId == employeeId && lb.Year == year)
                .ToListAsync();

        public async Task<LeaveBalance?> GetByEmployeeLeaveTypeYearAsync(
            Guid employeeId, Guid leaveTypeId, int year)
            => await _dbSet
                .FirstOrDefaultAsync(lb =>
                    lb.EmployeeId == employeeId &&
                    lb.LeaveTypeId == leaveTypeId &&
                    lb.Year == year);
    }
}

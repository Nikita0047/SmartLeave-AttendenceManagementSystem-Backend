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
using static LeaveManagmentSystem.Data.Repository.Repo.AttendenceRepo;

namespace LeaveManagmentSystem.Data.Repository.Repo
{
    public class AttendenceRepo:Repository<AttendenceRecord>, IAttendenceRecoRepo
    {
        // Infrastructure/Repositories/AttendanceRepository.cs
       
            public AttendenceRepo(AppDbContext context) : base(context) { }

            public async Task<AttendenceRecord?> GetByEmployeeIdAndDateAsync(Guid employeeId, DateOnly date)
                => await _dbSet
                    .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date == date);

            public async Task<IEnumerable<AttendenceRecord>> GetByEmployeeIdAndMonthAsync(
                Guid employeeId, int month, int year)
                => await _dbSet
                    .Where(a => a.EmployeeId == employeeId
                             && a.Date.Month == month
                             && a.Date.Year == year)
                    .OrderBy(a => a.Date)
                    .ToListAsync();

            public async Task<IEnumerable<AttendenceRecord>> GetTeamAttendanceForDateAsync(
                Guid departmentId, DateOnly date)
                => await _dbSet
                    .Include(a => a.Employee)
                    .Where(a => a.Employee.DepartmentId == departmentId && a.Date == date)
                    .ToListAsync();

            public async Task<IEnumerable<AttendenceRecord>> GetAbsenteesForDateAsync(DateOnly date)
                => await _dbSet
                    .Include(a => a.Employee)
                    .Where(a => a.Date == date && a.Status == AttendenceStatus.Absent)
                    .ToListAsync();
        }
    }


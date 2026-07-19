using LeaveManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.IRepository
{
    public interface IAttendenceRecoRepo:IRepository<AttendenceRecord>
    {
        Task<AttendenceRecord?> GetByEmployeeIdAndDateAsync(Guid employeeId, DateOnly date);
        Task<IEnumerable<AttendenceRecord>> GetByEmployeeIdAndMonthAsync(Guid employeeId, int month, int year);
        Task<IEnumerable<AttendenceRecord>> GetTeamAttendanceForDateAsync(Guid departmentId, DateOnly date);
        Task<IEnumerable<AttendenceRecord>> GetAbsenteesForDateAsync(DateOnly date); // used by Hangfire job
    }
}

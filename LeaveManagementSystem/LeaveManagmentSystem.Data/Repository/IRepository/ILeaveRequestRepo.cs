using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.IRepository
{
   public interface ILeaveRequestRepo:IRepository<LeaveRequest>
    {
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId);
        Task<IEnumerable<LeaveRequest>> GetByDepartmentAsync(Guid departmentId, LeaveStatus? status = null);
        Task<IEnumerable<LeaveRequest>> GetAllWithDetailsAsync(LeaveStatus? status = null);
        Task<LeaveRequest?> GetByIdWithDetailsAsync(Guid id);  // includes Employee + Dept
    }
}

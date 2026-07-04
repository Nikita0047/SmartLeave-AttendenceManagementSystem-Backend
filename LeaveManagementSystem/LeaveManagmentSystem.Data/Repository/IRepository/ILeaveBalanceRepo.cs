using LeaveManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.IRepository
{
  public interface ILeaveBalanceRepo:IRepository<LeaveBalance>
    {
        Task<IEnumerable<LeaveBalance>> GetByEmployeeAndYearAsync(Guid employeeId, int year);
        Task<LeaveBalance?> GetByEmployeeLeaveTypeYearAsync(Guid employeeId, Guid leaveTypeId, int year);
    }
}

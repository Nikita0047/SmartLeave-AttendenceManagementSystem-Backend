using LeaveManagementSystem.Core.Dtos.LeaveBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services.IServices
{
    // Core/Interfaces/Services/ILeaveBalanceService.cs
    public interface ILeaveBalanceService
    {
        Task<IEnumerable<LeaveBalanceSummaryDto>> GetMyBalancesAsync(Guid employeeId, int? year = null);
        Task<IEnumerable<LeaveBalanceSummaryDto>> GetByEmployeeIdAsync(Guid employeeId, int? year = null);
        Task AdjustBalanceAsync(Guid employeeId, AdjustLeaveBalanceDto dto);
        Task InitialiseBalancesAsync(Guid employeeId, int year);  // called when new user created
    }
}

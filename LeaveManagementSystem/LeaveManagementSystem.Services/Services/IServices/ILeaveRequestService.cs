using LeaveManagementSystem.Core.Dtos.LeaveRequest;
using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services.IServices
{
   public interface ILeaveRequestService
    {
        // Core/Interfaces/Services/ILeaveRequestService.cs
        // Core/Interfaces/Services/ILeaveRequestService.cs
        public interface ILeaveRequestService
        {
            // Employee
            Task<IEnumerable<LeaveRequestResponseDto>> GetMyLeavesAsync(Guid employeeId);
            Task<LeaveRequestResponseDto> ApplyAsync(LeaveRequestDto dto, Guid employeeId);
            Task CancelAsync(Guid leaveRequestId, Guid employeeId);

            // Manager
            Task<IEnumerable<LeaveRequestResponseDto>> GetTeamLeavesAsync(Guid managerId, LeaveStatus? status = null);
            Task<LeaveRequestResponseDto> UpdateStatusAsync(Guid leaveRequestId, UpdateLeaveStatusDto dto, Guid reviewerId);
            //                                                                    

            // Admin
            Task<IEnumerable<LeaveRequestResponseDto>> GetAllAsync(LeaveStatus? status = null);

            // Shared
            Task<LeaveRequestResponseDto?> GetByIdAsync(Guid id);
        }
    }
}

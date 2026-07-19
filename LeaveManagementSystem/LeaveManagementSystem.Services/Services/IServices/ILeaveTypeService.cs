using LeaveManagementSystem.Core.Dtos.LeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services.IServices
{
    // Core/Interfaces/Services/ILeaveTypeService.cs
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveTypeResponseDto>> GetAllActiveAsync();
        Task<LeaveTypeResponseDto?> GetByIdAsync(Guid id);
        Task<LeaveTypeResponseDto> CreateAsync(LeaveTypeRequestDto dto);
        Task<LeaveTypeResponseDto> UpdateAsync(Guid id, LeaveTypeRequestDto dto);
    }
}

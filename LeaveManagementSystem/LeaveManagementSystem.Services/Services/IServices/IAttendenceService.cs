using LeaveManagementSystem.Core.Dtos.AttendanceRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services.IServices
{
    // Core/Interfaces/Services/IAttendanceService.cs
    public interface IAttendanceService
    {
        Task<AttendenceResponseDto> CheckInAsync(Guid employeeId);
        Task<AttendenceResponseDto> CheckOutAsync(Guid employeeId);
        Task<IEnumerable<AttendenceResponseDto>> GetMyAttendanceAsync(Guid employeeId, int month, int year);
        Task<IEnumerable<AttendenceResponseDto>> GetTeamAttendanceAsync(Guid managerId, DateOnly date);
        Task<IEnumerable<AttendenceResponseDto>> GetReportAsync(int month, int year);
        Task MarkAbsenteesAsync(DateOnly date);  // called by Hangfire job at midnight
    }
}

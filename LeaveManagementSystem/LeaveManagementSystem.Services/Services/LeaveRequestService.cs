using LeaveManagementSystem.Core.Dtos.LeaveRequest;
using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Core.Enum;
using LeaveManagementSystem.Services.Services.IServices;
using LeaveManagmentSystem.Data.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services
{
    // Application/Services/LeaveRequestService.cs
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IUnitOfWork _uow;

        public LeaveRequestService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<LeaveRequestResponseDto>> GetMyLeavesAsync(Guid employeeId)
        {
            var requests = await _uow.LeaveRequests.GetByEmployeeIdAsync(employeeId);
            return requests.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<LeaveRequestResponseDto>> GetTeamLeavesAsync(
            Guid managerId, LeaveStatus? status = null)
        {
            var manager = await _uow.Users.GetByIdWithDepartmentAsync(managerId);
            if (manager is null)
                throw new KeyNotFoundException("Manager not found.");

            var requests = await _uow.LeaveRequests
                .GetByDepartmentAsync(manager.DepartmentId, status);

            return requests.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<LeaveRequestResponseDto>> GetAllAsync(
            LeaveStatus? status = null)
        {
            var requests = await _uow.LeaveRequests.GetAllWithDetailsAsync(status);
            return requests.Select(MapToResponseDto);
        }

        public async Task<LeaveRequestResponseDto?> GetByIdAsync(Guid id)
        {
            var request = await _uow.LeaveRequests.GetByIdWithDetailsAsync(id);
            return request is null ? null : MapToResponseDto(request);
        }

        public async Task<LeaveRequestResponseDto> ApplyAsync(
            LeaveRequestDto dto, Guid employeeId)
        {
            // 1 — calculate total days
            var totalDays = dto.EndDate.DayNumber - dto.StartDate.DayNumber + 1;
            if (totalDays <= 0)
                throw new InvalidOperationException("End date must be after start date.");

            // 2 — check balance
            var balance = await _uow.LeaveBalances
                .GetByEmployeeLeaveTypeYearAsync(
                    employeeId, dto.LeaveTypeId, DateTime.UtcNow.Year);

            if (balance is null)
                throw new InvalidOperationException("No leave balance found for this leave type.");

            if (balance.Remaining < totalDays)
                throw new InvalidOperationException(
                    $"Insufficient balance. Remaining: {balance.Remaining}, Requested: {totalDays}.");

            // 3 — check no overlapping pending/approved request
            var existing = await _uow.LeaveRequests.GetByEmployeeIdAsync(employeeId);
            var hasOverlap = existing.Any(r =>
                (r.Status == LeaveStatus.Pending || r.Status == LeaveStatus.Approved) &&
                r.StartDate <= dto.EndDate && r.EndDate >= dto.StartDate);

            if (hasOverlap)
                throw new InvalidOperationException("You already have a leave request for these dates.");

            // 4 — create request
            var request = new LeaveRequest
            {
                Id = Guid.NewGuid(),
                EmployeeId = employeeId,
                LeaveTypeId = dto.LeaveTypeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalDays = totalDays,
                Reason = dto.Reason,
                Status = LeaveStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.LeaveRequests.AddAsync(request);

            // 5 — increment pending balance
            balance.Pending += totalDays;
            _uow.LeaveBalances.Update(balance);

            // 6 — save both in one transaction
            await _uow.SaveChangesAsync();

            return await GetByIdAsync(request.Id)
                ?? throw new Exception("Failed to retrieve created request.");
        }

        public async Task CancelAsync(Guid leaveRequestId, Guid employeeId)
        {
            var request = await _uow.LeaveRequests.GetByIdAsync(leaveRequestId);
            if (request is null)
                throw new KeyNotFoundException("Leave request not found.");

            // only own requests
            if (request.EmployeeId != employeeId)
                throw new UnauthorizedAccessException("You can only cancel your own requests.");

            // only pending requests can be cancelled
            if (request.Status != LeaveStatus.Pending)
                throw new InvalidOperationException("Only pending requests can be cancelled.");

            // restore balance
            var balance = await _uow.LeaveBalances
                .GetByEmployeeLeaveTypeYearAsync(
                    employeeId, request.LeaveTypeId, DateTime.UtcNow.Year);

            if (balance is not null)
            {
                balance.Pending -= request.TotalDays;
                _uow.LeaveBalances.Update(balance);
            }

            request.Status = LeaveStatus.Cancelled;
            request.UpdatedAt = DateTime.UtcNow;
            _uow.LeaveRequests.Update(request);

            await _uow.SaveChangesAsync();
        }

        public async Task<LeaveRequestResponseDto> UpdateStatusAsync(
            Guid leaveRequestId, UpdateLeaveStatusDto dto, Guid reviewerId)
        {
            // validate — only Approved or Rejected allowed here
            if (dto.Status != LeaveStatus.Approved && dto.Status != LeaveStatus.Rejected)
                throw new InvalidOperationException("Status must be Approved or Rejected.");

            // remarks required on rejection
            if (dto.Status == LeaveStatus.Rejected && string.IsNullOrEmpty(dto.Remarks))
                throw new InvalidOperationException("Remarks are required when rejecting.");

            var request = await _uow.LeaveRequests.GetByIdWithDetailsAsync(leaveRequestId);
            if (request is null)
                throw new KeyNotFoundException("Leave request not found.");

            if (request.Status != LeaveStatus.Pending)
                throw new InvalidOperationException("Only pending requests can be reviewed.");

            var balance = await _uow.LeaveBalances
                .GetByEmployeeLeaveTypeYearAsync(
                    request.EmployeeId, request.LeaveTypeId, DateTime.UtcNow.Year);

            if (dto.Status == LeaveStatus.Approved)
            {
                // move days from Pending → Used
                if (balance is not null)
                {
                    balance.Pending -= request.TotalDays;
                    balance.Used += request.TotalDays;
                    _uow.LeaveBalances.Update(balance);
                }
            }
            else
            {
                // Rejected — restore Pending days
                if (balance is not null)
                {
                    balance.Pending -= request.TotalDays;
                    _uow.LeaveBalances.Update(balance);
                }
            }

            request.Status = dto.Status;
            request.ReviewedBy = reviewerId;
            request.ReviewRemarks = dto.Remarks;
            request.UpdatedAt = DateTime.UtcNow;
            _uow.LeaveRequests.Update(request);

            await _uow.SaveChangesAsync();

            return MapToResponseDto(request);
        }

        private static LeaveRequestResponseDto MapToResponseDto(LeaveRequest r) => new()
        {
            Id = r.Id,
            EmployeeId = r.EmployeeId,
            EmployeeName = r.Employee?.FullName ?? string.Empty,
            DepartmentName = r.Employee?.Department?.Name ?? string.Empty,
            LeaveTypeId= r.LeaveTypeId,
            LeaveTypeName = r.LeaveType.Name,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TotalDays = r.TotalDays,
            Reason = r.Reason,
            Status = r.Status.ToString(),
            ReviewerName = r.ReviewedBy.HasValue ? "Reviewed" : null,
            ReviewRemark = r.ReviewRemarks,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt??DateTime.Now
        };
    }
}

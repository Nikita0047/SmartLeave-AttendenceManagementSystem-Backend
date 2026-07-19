using LeaveManagementSystem.Core.Dtos.LeaveType;
using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Services.Services.IServices;
using LeaveManagmentSystem.Data.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services
{
    // Application/Services/LeaveTypeService.cs
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly IUnitOfWork _uow;
       
        public LeaveTypeService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<LeaveTypeResponseDto>> GetAllActiveAsync()
        {
            var types = await _uow.LeaveTypes.GetActiveAsync();
            return types.Select(MapToResponseDto);
        }

        public async Task<LeaveTypeResponseDto?> GetByIdAsync(Guid id)
        {
            var type = await _uow.LeaveTypes.GetByIdAsync(id);
            return type is null ? null : MapToResponseDto(type);
        }

        public async Task<LeaveTypeResponseDto> CreateAsync(LeaveTypeRequestDto dto)
        {
            var leaveType = new LeaveType
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                MaxDaysPerYear = dto.MaxDaysPerYear??0,
                IsCarryForwardAllowed = dto.IsCarryForwardAllowed??false,
                IsActive = true
            };

            await _uow.LeaveTypes.AddAsync(leaveType);
            await _uow.SaveChangesAsync();

            return MapToResponseDto(leaveType);
        }

        public async Task<LeaveTypeResponseDto> UpdateAsync(Guid id, LeaveTypeRequestDto dto)
        {
            var leaveType = await _uow.LeaveTypes.GetByIdAsync(id);
            if (leaveType is null)
                throw new KeyNotFoundException("Leave type not found.");

            leaveType.Name = dto.Name ?? leaveType.Name;
            leaveType.MaxDaysPerYear = dto.MaxDaysPerYear ?? leaveType.MaxDaysPerYear;
            leaveType.IsCarryForwardAllowed = dto.IsCarryForwardAllowed ?? leaveType.IsCarryForwardAllowed;

            if (dto.IsActive.HasValue)
                leaveType.IsActive = dto.IsActive.Value;

            _uow.LeaveTypes.Update(leaveType);
            await _uow.SaveChangesAsync();

            return MapToResponseDto(leaveType);
        }

        private static LeaveTypeResponseDto MapToResponseDto(LeaveType lt) => new()
        {
            Id = lt.Id,
            Name = lt.Name,
            MaxDaysPerYear = lt.MaxDaysPerYear,
            IsCarryForwardAllowed = lt.IsCarryForwardAllowed,
            IsActive = lt.IsActive
        };
    }
}

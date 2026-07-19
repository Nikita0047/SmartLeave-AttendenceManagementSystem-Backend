using LeaveManagementSystem.Core.Dtos.LeaveBalance;
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
    // Application/Services/LeaveBalanceService.cs
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly IUnitOfWork _uow;

        public LeaveBalanceService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<LeaveBalanceSummaryDto>> GetMyBalancesAsync(
            Guid employeeId, int? year = null)
        {
            var targetYear = year ?? DateTime.UtcNow.Year;
            var balances = await _uow.LeaveBalances
                .GetByEmployeeAndYearAsync(employeeId, targetYear);
            return balances.Select(MapToDto);
        }

        public async Task<IEnumerable<LeaveBalanceSummaryDto>> GetByEmployeeIdAsync(
            Guid employeeId, int? year = null)
        {
            var targetYear = year ?? DateTime.UtcNow.Year;
            var balances = await _uow.LeaveBalances
                .GetByEmployeeAndYearAsync(employeeId, targetYear);
            return balances.Select(MapToDto);
        }

        public async Task AdjustBalanceAsync(Guid employeeId, AdjustLeaveBalanceDto dto)
        {
            var balance = await _uow.LeaveBalances
                .GetByEmployeeLeaveTypeYearAsync(
                    employeeId, dto.LeaveTypeId, dto.Year);

            if (balance is null)
                throw new KeyNotFoundException("Leave balance not found.");

            balance.TotalAllotted = dto.AdjustedTotal;
            _uow.LeaveBalances.Update(balance);
            await _uow.SaveChangesAsync();
        }

        public async Task InitialiseBalancesAsync(Guid employeeId, int year)
        {
            var leaveTypes = await _uow.LeaveTypes.GetActiveAsync();

            foreach (var leaveType in leaveTypes)
            {
                var existing = await _uow.LeaveBalances
                    .GetByEmployeeLeaveTypeYearAsync(employeeId, leaveType.Id, year);

                if (existing is not null) continue;  // already initialised

                await _uow.LeaveBalances.AddAsync(new LeaveBalance
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    Year = year,
                    TotalAllotted = leaveType.MaxDaysPerYear,
                    Used = 0,
                    Pending = 0
                });
            }

            await _uow.SaveChangesAsync();
        }

        private static LeaveBalanceSummaryDto MapToDto(LeaveBalance lb) => new()
        {
            LeaveTypeId = lb.LeaveTypeId,
            LeaveTypeName = lb.LeaveType?.Name ?? string.Empty,
            TotalAllotted = lb.TotalAllotted,
            Used = lb.Used,
            Pending = lb.Pending,
            Remaining = lb.Remaining,   // computed property on entity
            Year = lb.Year
        };
    }
}

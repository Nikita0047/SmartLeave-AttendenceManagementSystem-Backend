using LeaveManagementSystem.Core.Dtos.AttendanceRecord;
using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Core.Enum;
using LeaveManagementSystem.Services.Services.IServices;
using LeaveManagmentSystem.Data.Repository.IRepository;
using LeaveManagmentSystem.Data.Repository.Repo;
using LeaveManagmentSystem.Data.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LeaveManagementSystem.Services.Services
{
    public class AttendenceService : IAttendanceService
    {
        private readonly IUnitOfWork _UoW;
        private readonly IAttendenceRecoRepo _attendanceRecoRepo;
        private readonly IUserRepo _userRepo;

        public AttendenceService(IUnitOfWork UoW, IAttendenceRecoRepo attendenceRecoRepo, IUserRepo userRepo)
        {
            _UoW = UoW;
            _attendanceRecoRepo = attendenceRecoRepo;
            _userRepo = userRepo;
        }
        public Task<AttendenceResponseDto> CheckInAsync(Guid employeeId)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var existingRecord = _attendanceRecoRepo.GetByEmployeeIdAndDateAsync(employeeId, today);
            if (existingRecord != null)
            {
                throw new InvalidOperationException("Employee has already checked in today.");
            }
            else
            {
                var newRecord = new AttendenceRecord
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    Date = today,
                    CheckIn = TimeOnly.FromDateTime(DateTime.UtcNow),
                    /*Atten = dto.Notes*/
                };
                _attendanceRecoRepo.AddAsync(newRecord);
                _UoW.SaveChangesAsync();
                return Task.FromResult(new AttendenceResponseDto
                {
                    Id = newRecord.Id,
                    EmployeeId = newRecord.EmployeeId,
                    Date = newRecord.Date,
                    CheckInTime = newRecord.CheckIn,
                    Status = "Checked In"
                });
            }
        }

                public  async Task<AttendenceResponseDto> CheckOutAsync(Guid employeeId)
                {
                        var today = DateOnly.FromDateTime(DateTime.UtcNow);

                        var record = await _attendanceRecoRepo
                            .GetByEmployeeIdAndDateAsync(employeeId, today);

                        if (record is null)
                            throw new InvalidOperationException("No check-in found for today.");

                        if (record.CheckOut.HasValue)
                            throw new InvalidOperationException("Already checked out today.");

                        record.CheckOut = TimeOnly.FromDateTime(DateTime.UtcNow);
                        record.HoursWorked = (record.CheckOut.Value.ToTimeSpan()
                                             - record.CheckIn!.Value.ToTimeSpan()).TotalHours;

                        _attendanceRecoRepo.Update(record);
                         _UoW.SaveChangesAsync();

                        return MapToDto(record);
        }

        public async Task<IEnumerable<AttendenceResponseDto>> GetMyAttendanceAsync(Guid employeeId, int month, int year)
        {
            var records = await _attendanceRecoRepo
            .GetByEmployeeIdAndMonthAsync(employeeId, month, year);
            return records.Select(MapToDto);
        }

        public async Task<IEnumerable<AttendenceResponseDto>> GetReportAsync(int month, int year)
        {
            var allUsers = await _userRepo.GetAllAsync();
            var result = new List<AttendenceResponseDto>();

            foreach (var user in allUsers.Where(u => u.IsActive))
            {
                var records = await _attendanceRecoRepo
                    .GetByEmployeeIdAndMonthAsync(user.Id, month, year);
                result.AddRange(records.Select(MapToDto));
            }

            return result;

        }

        public async Task<IEnumerable<AttendenceResponseDto>> GetTeamAttendanceAsync(Guid managerId, DateOnly date)
        {
            var manager = await _userRepo.GetByIdWithDepartmentAsync(managerId);
            if (manager is null)
                throw new KeyNotFoundException("Manager not found.");

            var records = await _attendanceRecoRepo
                .GetTeamAttendanceForDateAsync(manager.DepartmentId, date);
            return records.Select(MapToDto);
        }

        public async Task MarkAbsenteesAsync(DateOnly date)
        {
            var allUsers = await _userRepo.GetAllAsync();

            foreach (var user in allUsers.Where(u => u.IsActive))
            {
                var record = await _attendanceRecoRepo
                    .GetByEmployeeIdAndDateAsync(user.Id, date);

                if (record is not null) continue;  // already has a record

                // check if on approved leave
                var onLeave = await _UoW.LeaveRequests
                    .GetByEmployeeIdAsync(user.Id);

                var isOnLeave = onLeave.Any(r =>
                    r.Status == LeaveStatus.Approved &&
                    r.StartDate <= date && r.EndDate >= date);

                await _attendanceRecoRepo.AddAsync(new AttendenceRecord
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = user.Id,
                    Date = date,
                    Status = isOnLeave
                  ? AttendenceStatus.OnLeave
                  : AttendenceStatus.Absent
                });
            }

           await  _UoW.SaveChangesAsync();
        }


        private static AttendenceResponseDto MapToDto(AttendenceRecord r) => new()
        {
            Id = r.Id,
            EmployeeId = r.EmployeeId,
            EmployeeName = r.Employee?.FullName ?? string.Empty,
            Date = r.Date,
            CheckInTime = r.CheckIn,
            CheckOutTime = r.CheckOut,
            TotalHoursWorked = r.HoursWorked,
            Status = r.Status.ToString()
        };
    }
}

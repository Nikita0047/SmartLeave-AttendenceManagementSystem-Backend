using LeaveManagementSystem.Core.Dtos.User;
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
    // Application/Services/UserService.cs
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _uow.Users.GetAllAsync();
            return users.Select(MapToResponseDto);
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            var user = await _uow.Users.GetByIdWithDepartmentAsync(id);
            return user is null ? null : MapToResponseDto(user);
        }

        public async Task<UserResponseDto> GetCurrentUserAsync(Guid userId)
        {
            var user = await _uow.Users.GetByIdWithDepartmentAsync(userId);
            if (user is null)
                throw new KeyNotFoundException("User not found.");
            return MapToResponseDto(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetByDepartmentAsync(Guid departmentId)
        {
            var users = await _uow.Users.GetByDepartmentAsync(departmentId);
            return users.Select(MapToResponseDto);
        }

        public async Task<UserResponseDto> CreateAsync(UserRequestDto dto)
        {
            // 1 — check email not already taken
            if (await _uow.Users.EmailExistsAsync(dto.Email))
                throw new InvalidOperationException("Email already in use.");

            // 2 — password required on create
            if (string.IsNullOrEmpty(dto.Password))
                throw new InvalidOperationException("Password is required.");

            // 3 — create user
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                DepartmentId = dto.DepartmentId,
                ManagerId = dto.ManagerId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.Users.AddAsync(user);

            // 4 — initialise leave balances for this user
            await InitialiseLeaveBalancesAsync(user.Id);

            await _uow.SaveChangesAsync();

            return await GetByIdAsync(user.Id)
                ?? throw new Exception("Failed to retrieve created user.");
        }

        public async Task<UserResponseDto> UpdateAsync(Guid id, UserRequestDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user is null)
                throw new KeyNotFoundException("User not found.");

            user.FullName = dto.FullName;
            user.Role = dto.Role;
            user.DepartmentId = dto.DepartmentId;
            user.ManagerId = dto.ManagerId;

            if (dto.IsActive.HasValue)
                user.IsActive = dto.IsActive.Value;

            // only update password if provided
            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            return await GetByIdAsync(id)
                ?? throw new Exception("Failed to retrieve updated user.");
        }

        // ── private helpers ──────────────────────────────────────

        private async Task InitialiseLeaveBalancesAsync(Guid userId)
        {
            var leaveTypes = await _uow.LeaveTypes.GetActiveAsync();
            var year = DateTime.UtcNow.Year;

            foreach (var leaveType in leaveTypes)
            {
                await _uow.LeaveBalances.AddAsync(new LeaveBalance
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = userId,
                    LeaveTypeId = leaveType.Id,
                    Year = year,
                    TotalAllotted = leaveType.MaxDaysPerYear,
                    Used = 0,
                    Pending = 0
                });
            }
        }

        private static UserResponseDto MapToResponseDto(User user) => new()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString(),
            DepartmentId = user.DepartmentId,
            DepartmentName = user.Department?.Name ?? string.Empty,
            ManagerId = user.ManagerId,
            ManagerName = user.Manager?.FullName,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}

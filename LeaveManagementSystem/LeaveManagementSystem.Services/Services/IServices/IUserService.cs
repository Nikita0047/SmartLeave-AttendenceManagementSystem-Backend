using LeaveManagementSystem.Core.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services.IServices
{
     public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(Guid id);
        Task<UserResponseDto> CreateAsync(UserRequestDto dto);
        Task<UserResponseDto> UpdateAsync(Guid id, UserRequestDto dto);
        Task<UserResponseDto> GetCurrentUserAsync(Guid userId);
    }
}

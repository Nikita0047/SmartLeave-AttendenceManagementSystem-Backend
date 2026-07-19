using LeaveManagementSystem.Core.Dtos.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResponseDto>> GetAllAsync();
        Task<DepartmentResponseDto?> GetByIdAsync(Guid id);
        Task<DepartmentResponseDto> CreateAsync(DepartmentRequestDto dto);
        Task DeactivateAsync(Guid id);
    }
}

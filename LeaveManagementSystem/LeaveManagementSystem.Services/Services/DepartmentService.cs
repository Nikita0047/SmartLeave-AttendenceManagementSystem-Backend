using LeaveManagementSystem.Core.Dtos.Department;
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
    // Application/Services/DepartmentService.cs
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _uow;

        public DepartmentService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<DepartmentResponseDto>> GetAllAsync()
        {
            var departments = await _uow.Departments.GetAllAsync();
            return departments.Select(MapToResponseDto);
        }

        public async Task<DepartmentResponseDto?> GetByIdAsync(Guid id)
        {
            var dept = await _uow.Departments.GetByIdAsync(id);
            return dept is null ? null : MapToResponseDto(dept);
        }

        public async Task<DepartmentResponseDto> CreateAsync(DepartmentRequestDto dto)
        {
            if (await _uow.Departments.NameExistsAsync(dto.Name))
                throw new InvalidOperationException("Department name already exists.");

            var dept = new Department
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                IsActive = true
            };

            await _uow.Departments.AddAsync(dept);
            await _uow.SaveChangesAsync();

            return MapToResponseDto(dept);
        }

        public async Task<DepartmentResponseDto> UpdateAsync(Guid id, DepartmentRequestDto dto)
        {
            var dept = await _uow.Departments.GetByIdAsync(id);
            if (dept is null)
                throw new KeyNotFoundException("Department not found.");

            dept.Name = dto.Name;

            if (dto.IsActive.HasValue)
                dept.IsActive = dto.IsActive.Value;

            _uow.Departments.Update(dept);
            await _uow.SaveChangesAsync();

            return MapToResponseDto(dept);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var dept = await _uow.Departments.GetByIdAsync(id);
            if (dept is null)
                throw new KeyNotFoundException("Department not found.");

            dept.IsActive = false;
            _uow.Departments.Update(dept);
            await _uow.SaveChangesAsync();
        }

        private static DepartmentResponseDto MapToResponseDto(Department dept) => new()
        {
            Id = dept.Id,
            Name = dept.Name,
            IsActive = dept.IsActive,
            TotalEmployees = dept.Employees?.Count(u => u.IsActive) ?? 0
        };
    }
}

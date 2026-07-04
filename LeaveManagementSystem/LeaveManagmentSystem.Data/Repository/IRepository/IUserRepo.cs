using LeaveManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.IRepository
{
    public interface IUserRepo:IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdWithDepartmentAsync(Guid id);
        Task<IEnumerable<User>> GetByDepartmentAsync(Guid departmentId);
        Task<bool> EmailExistsAsync(string email);
    }
}

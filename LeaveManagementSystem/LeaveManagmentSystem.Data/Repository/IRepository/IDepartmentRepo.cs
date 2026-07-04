using LeaveManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.IRepository
{
    public interface IDepartmentRepo:IRepository<Department>
    {
        Task<bool> NameExistsAsync(string name);
    }
}

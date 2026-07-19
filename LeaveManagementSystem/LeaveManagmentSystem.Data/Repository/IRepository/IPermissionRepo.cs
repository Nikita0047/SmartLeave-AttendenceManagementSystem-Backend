using LeaveManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.IRepository
{
    public interface IPermissionRepo : IRepository<Permissions>
    {
        Task<Permissions?> GetByNameAsync(string name);
        Task<List<Permissions>> GetByNamesAsync(List<string> names);
    }
}

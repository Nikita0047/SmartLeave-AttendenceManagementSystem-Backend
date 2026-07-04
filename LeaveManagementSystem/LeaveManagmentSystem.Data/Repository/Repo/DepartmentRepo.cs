using LeaveManagementSystem.Core.Entities;
using LeaveManagmentSystem.Data.Data;
using LeaveManagmentSystem.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.Repo
{
   public class DepartmentRepo:Repository<Department>, IDepartmentRepo
    {
        public DepartmentRepo(AppDbContext context) : base(context) { }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.Departments
                .AnyAsync(d => d.Name.ToLower() == name.ToLower());
        }
    }
}

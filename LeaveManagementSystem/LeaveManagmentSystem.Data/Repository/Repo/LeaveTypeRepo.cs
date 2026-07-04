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
    public class LeaveTypeRepo:Repository<LeaveType>, ILeaveTypeRepo
    {

        public LeaveTypeRepo(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<LeaveType>> GetActiveAsync()
        {
            return await _context.LeaveTypes
                .Where(lt => lt.IsActive)
                .ToListAsync();
        }
    }
}

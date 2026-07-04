using LeaveManagmentSystem.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo Users { get; }
        IDepartmentRepo Departments { get; }
        ILeaveRequestRepo LeaveRequests { get; }
        ILeaveBalanceRepo LeaveBalances { get; }
        ILeaveTypeRepo LeaveTypes { get; }
        IAttendenceRecoRepo Attendance { get; }
        IRolePermissionRepo RolePermissions { get; }

        Task<int> SaveChangesAsync();
    }
}

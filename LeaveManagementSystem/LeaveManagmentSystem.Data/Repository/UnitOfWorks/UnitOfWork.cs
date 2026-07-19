using LeaveManagementSystem.Core.Entities;
using LeaveManagmentSystem.Data.Data;
using LeaveManagmentSystem.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository.UnitOfWorks
{
    // Infrastructure/Data/UnitOfWork.cs
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepo Users { get; }
        public IDepartmentRepo Departments { get; }
        public ILeaveRequestRepo LeaveRequests { get; }
        public ILeaveBalanceRepo LeaveBalances { get; }
        public IPermissionRepo Permissions { get; }
        public ILeaveTypeRepo LeaveTypes { get; }
        public IAttendenceRecoRepo Attendance { get; }
        public IRolePermissionRepo RolePermissions { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepo users,
            IDepartmentRepo departments,
            ILeaveRequestRepo leaveRequests,
            ILeaveBalanceRepo leaveBalances,
            ILeaveTypeRepo leaveTypes,
            IAttendenceRecoRepo attendance,
            IRolePermissionRepo rolePermissions,
            IPermissionRepo permission)
        {
            _context = context;
            Users = users;
            Departments = departments;
            LeaveRequests = leaveRequests;
            LeaveBalances = leaveBalances;
            LeaveTypes = leaveTypes;
            Attendance = attendance;
            RolePermissions = rolePermissions;
            Permissions = permission;
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}

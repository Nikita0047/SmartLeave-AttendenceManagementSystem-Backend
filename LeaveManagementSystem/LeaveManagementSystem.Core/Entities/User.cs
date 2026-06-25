using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public UserRole Role { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid? ManagerId { get; set; }
        public User Manager { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public ICollection<LeaveBalance> LeaveBalances { get; set; }
        public ICollection<AttendenceRecord> AttendenceRecords { get; set; }
    }
}

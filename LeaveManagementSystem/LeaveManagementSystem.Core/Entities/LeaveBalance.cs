using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Entities
{
    public class LeaveBalance
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public User Employee { get; set; }
        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public int Year { get; set; }
        public int TotalAllotted { get; set; }
        public int Used { get; set; }
        public int Pending { get; set; }             // submitted but not yet approved
        public int Remaining => TotalAllotted - Used - Pending;
    }
}

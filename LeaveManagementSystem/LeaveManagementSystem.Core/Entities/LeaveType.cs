using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Entities
{
    public class LeaveType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }             // "Sick Leave", "Casual Leave"
        public int MaxDaysPerYear { get; set; }
        public bool IsCarryForwardAllowed { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

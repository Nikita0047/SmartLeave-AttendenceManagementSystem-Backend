using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.LeaveType
{
    public class LeaveTypeResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MaxDaysPerYear { get; set; }
        public bool IsCarryForwardAllowed { get; set; }
        public bool IsActive { get; set; }
    }
}
